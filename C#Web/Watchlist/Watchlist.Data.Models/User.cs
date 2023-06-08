namespace Watchlist.Data.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public User()
    {
        this.UsersMovies = new List<UserMovie>();
    }

    public ICollection<UserMovie> UsersMovies { get; set; } = null!;
}
