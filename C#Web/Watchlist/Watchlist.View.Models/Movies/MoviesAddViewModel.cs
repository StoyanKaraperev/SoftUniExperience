namespace Watchlist.View.Models.Movies;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Watchlist.Data.Models;
using static Watchlist.Common.ValidationConstants.Constants;

public class MoviesAddViewModel
{
    public MoviesAddViewModel()
    {
        this.Genres = new List<Genre>();
    }

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
    public int GenreId { get; set; }

    public IEnumerable<Genre> Genres { get; set; } = null!;
}
