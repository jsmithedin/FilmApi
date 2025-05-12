using Microsoft.EntityFrameworkCore;

namespace FilmApi.Models;

public class FilmContext : DbContext
{
    public FilmContext(DbContextOptions<FilmContext> options) : base(options)
    {
    }

    public DbSet<FilmItem> FilmItems { get; set; } = null!;
}