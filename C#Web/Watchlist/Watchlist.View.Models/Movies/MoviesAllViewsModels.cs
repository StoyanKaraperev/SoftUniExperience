namespace Watchlist.View.Models.Movies;

using System.ComponentModel.DataAnnotations;
using static Watchlist.Common.ValidationConstants.Constants; 

public class MoviesAllViewsModels
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(MovieTitleMinLength)]
    [MaxLength(MovieTitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(MovieDirectorMinLength)]
    [MaxLength(MovieDirectorMaxLength)]
    public string Director { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), "0.00", "10.00", ConvertValueInInvariantCulture = true)]
    public decimal Rating { get; set; }

    [Required]
    public string Genre { get; set; } = null!; 
}
