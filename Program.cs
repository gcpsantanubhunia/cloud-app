using Microsoft.EntityFrameworkCore;
// using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
builder.Services.AddDbContext<BookDb>(opt => opt.UseInMemoryDatabase("BookList"));

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//    app.MapOpenApi();
//    app.MapScalarApiReference();
// }

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Ok(new { PodName = Environment.MachineName }));

app.RegisterEndpoints();

app.Run();

// var builder = WebApplication.CreateBuilder(args);

// var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
// var url = $"http://0.0.0.0:{port}";
// var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";

// var app = builder.Build();

// app.MapGet("/", () => $"Hello {target}!");

// app.Run(url);