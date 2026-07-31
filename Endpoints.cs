using Microsoft.EntityFrameworkCore;
public static class Endpoints
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        RouteGroupBuilder books = app.MapGroup("/books");

        books.MapGet("/", GetAllTodos);
        books.MapGet("/{id}", GetTodo);
        books.MapPost("/", CreateTodo);
        books.MapPut("/{id}", UpdateTodo);
        books.MapDelete("/{id}", DeleteTodo);


        static async Task<IResult> GetAllTodos(BookDb db)
        {
            return TypedResults.Ok(await db.Books.ToListAsync());
        }       

        static async Task<IResult> GetTodo(int id, BookDb db)
        {
            return await db.Books.FindAsync(id)
                is Book book
                    ? TypedResults.Ok(book)
                    : TypedResults.NotFound();
        }

        static async Task<IResult> CreateTodo(Book newBook, BookDb db)
        {          
            db.Books.Add(newBook);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/books/{newBook.Id}", newBook);
        }

        static async Task<IResult> UpdateTodo(int id, Book updatedBook, BookDb db)
        {
            var book = await db.Books.FindAsync(id);

            if (book is null) return TypedResults.NotFound();

            book.Id = id;
            book.BookName = updatedBook.BookName;
            book.Price = updatedBook.Price;
            book.Category = updatedBook.Category;
            book.Author = updatedBook.Author;

            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }
        

        static async Task<IResult> DeleteTodo(int id, BookDb db)
        {
            if (await db.Books.FindAsync(id) is Book book)
            {
                db.Books.Remove(book);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}