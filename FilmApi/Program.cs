using Microsoft.EntityFrameworkCore;
using FilmApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FilmContext>((sp, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("Postgres");
    options.UseNpgsql(connectionString)
        .UseSnakeCaseNamingConvention();
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();
