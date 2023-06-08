namespace Watchlist.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Watchlist.Common.ValidationConstants.Constants;

public class Genre
{
    public Genre()
    {
        this.Movies = new List<Movie>();
    }

    [Required]
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(GenreNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    public virtual ICollection<Movie> Movies { get; set; } = null!; 
}