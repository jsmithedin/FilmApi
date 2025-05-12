using System.ComponentModel.DataAnnotations;

namespace FilmApi.Models;

public enum FilmType
{
    [Display(Name = "Color Film")]
    Color,

    [Display(Name = "Black and White")]
    BlackAndWhite,

    [Display(Name = "Slide Film")]
    Slide,

    [Display(Name = "Negative Film")]
    Negative,

    [Display(Name = "Instant Film")]
    Instant,

    [Display(Name = "Infrared Film")]
    Infrared
}

public enum FilmFormat
{
    [Display(Name = "35mm")]
    Format35mm,

    [Display(Name = "120 Medium Format")]
    Format120,

    [Display(Name = "4x5")]
    LargeFormat4x5,

    [Display(Name = "8x10")]
    LargeFormat8x10,

    [Display(Name = "110")]
    Format110,

    [Display(Name = "220")]
    Format220,

    [Display(Name = "Instant Pack")]
    InstantPack,

    [Display(Name = "Sheet Film")]
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
