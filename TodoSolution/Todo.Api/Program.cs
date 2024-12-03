using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Todo.Data;
using Todo.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add DbContext, UnitOfWork, and AutoMapper to the Service container <-------- here we are adding our services
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("Todo"))
);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(); // scalar/v1 <-------- this adds Scalar.AspNetCore to the HTTP Request pipeline
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();