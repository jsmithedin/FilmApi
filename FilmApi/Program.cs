using Microsoft.EntityFrameworkCore;
using FilmApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FilmContext>(opt => opt.UseInMemoryDatabase("FilmList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "FilmApi";
    config.Title = "FilmAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "FilmAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

var todoItems = app.MapGroup("/filmitems");

todoItems.MapGet("/", GetAllTodos);
todoItems.MapGet("/complete", GetCompleteTodos);
todoItems.MapGet("/{id}", GetTodo);
todoItems.MapPost("/", CreateTodo);
todoItems.MapPut("/", UpdateTodo);
todoItems.MapDelete("/{id}", DeleteTodo);

app.MapControllers();

app.Run();

static async Task<IResult> GetAllTodos(FilmContext db)
{
    return TypedResults.Ok(await db.TodoItems.Select(x => new FilmContext(x)).ToListAsync());
}

static async Task<IResult> GetCompleteTodos(FilmContext db)
{
    return TypedResults.Ok(await db.Todos.Where(t => t.IsComplete).Select(x => new FilmItemDTO(x)).ToListAsync());
}

static async Task<IResult> GetTodo(int id, FilmContext db)
{
    return await db.Todos.FindAsync(id)
        is FilmItem todo
        ? TypedResults.Ok(new FilmItemDTO(todo))
        : TypedResults.NotFound();
}

static async Task<IResult> CreateTodo(FilmItemDTO filmItemDto, FilmContext db)
{
    var todoItem = new FilmItem
    {
        IsComplete = filmItemDto.IsComplete,
        Name = filmItemDto.Name
    };
    
    db.Todos.Add(todoItem);
    await db.SaveChangesAsync();
    
    filmItemDto = new FilmItemDTO(todoItem);

    return TypedResults.Created($"/todoitems/{filmItemDto.Id}", filmItemDto);
}

static async Task<IResult> UpdateTodo(int id, FilmItemDTO filmItemDto, FilmContext db)
{
    var todo = await db.Todos.FindAsync(id);
    
    if (todo is null) return TypedResults.NotFound();

    todo.Name = filmItemDto.Name;
    todo.IsComplete = filmItemDto.IsComplete;
    
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteTodo(int id, FilmContext db)
{
    if (await db.Todos.FindAsync(id) is FilmItem todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    
    return TypedResults.NotFound();
}

    