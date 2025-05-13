using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmApi.Models;

[JsonConverter(typeof(JsonStringEnumConverter<FilmType>))]
public enum FilmType
{
    [JsonStringEnumMemberName("Colour Film")]
    Colour,

    [JsonStringEnumMemberName("Black and White")]
    BlackAndWhite,

    [JsonStringEnumMemberName("Slide Film")]
    Slide,

    [JsonStringEnumMemberName("Negative Film")]
    Negative,

    [JsonStringEnumMemberName("Instant Film")]
    Instant,

    [JsonStringEnumMemberName("Infrared Film")]
    Infrared
}

[JsonConverter(typeof(JsonStringEnumConverter<FilmFormat>))]
public enum FilmFormat
{
    [JsonStringEnumMemberName("35mm")]
    Format35mm,

    [JsonStringEnumMemberName("120 Medium Format")]
    Format120,

    [JsonStringEnumMemberName("4x5")]
    LargeFormat4x5,

    [JsonStringEnumMemberName("8x10")]
    LargeFormat8x10,

    [JsonStringEnumMemberName("110")]
    Format110,

    [JsonStringEnumMemberName("220")]
    Format220,

    [JsonStringEnumMemberName("Instant Pack")]
    InstantPack,

    [JsonStringEnumMemberName("Sheet Film")]
    SheetFilm
}

public class FilmItem
{
    public int Id { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Manufacturer must be between 2 and 100 characters")]
    public string Manufacturer { get; set; } = "";
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name { get; set; } = "";
    [Required]
    [EnumDataType(typeof(FilmType))]
    public FilmType Type { get; set; }
    [Required]
    [EnumDataType(typeof(FilmFormat))]
    public FilmFormat Format { get; set; }
    [Required]
    [Range(0, 1000, ErrorMessage = "Quantity must be between 0 and 1000")]
    public int Quantity { get; set; } = 0;
}
