namespace Watchlist.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Watchlist.Common.ValidationConstants.Constants;

public class Movie
{
    public Movie()
    {
        this.UsersMovies = new List<UserMovie>();
    }

    [Required]
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(MovieTitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(MovieDirectorMaxLength)]
    public string Director { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public decimal Rating { get; set; }

    [Required]
    [ForeignKey(nameof(Genre))]
    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    [Required]
    public virtual ICollection<UserMovie> UsersMovies { get; set; } = null!; 
}