namespace FilmApi.Models;

public class FilmItemDTO
{
    public int Id { get; set; }
    public string Manufacturer { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Format { get; set; }
    public int Quantity { get; set; }

    public FilmItemDTO()
    {
    }
    public FilmItemDTO(FilmItem filmItemItem) =>
    (Id, Manufacturer, Name, Type, Format, Quantity) = (filmItemItem.Id, filmItemItem.Manufacturer, filmItemItem.Name, filmItemItem.Type, filmItemItem.Format, filmItemItem.Quantity);
}