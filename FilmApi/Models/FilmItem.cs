namespace FilmApi.Models;

public class FilmItem
{
    public int Id { get; set; }
    public string Manufacturer { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Format { get; set; }
    public int Quantity { get; set; }
}