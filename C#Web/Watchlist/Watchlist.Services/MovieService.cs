namespace Watchlist.Services;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Services.Interfaces;
using Watchlist.View.Models.Movies;

public class MovieService : IMovieService
{
    private readonly WatchlistDbContext context;

    public MovieService(WatchlistDbContext _context)
    {
        this.context = _context;
    }

    public async Task<IEnumerable<MoviesAllViewsModels>> GetAllAsync()
    {
        return await context.Movies
            .Select(m => new MoviesAllViewsModels()
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                ImageUrl = m.ImageUrl,
                Rating = m.Rating,
                Genre = m.Genre.Name, 
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<Genre>> GetGenresAsync()
    {
        return await context.Genres.ToListAsync();
    }

    public async Task AddMovieAsync(MoviesAddViewModel model)
    {
        var entity = new Movie()
        {
            Director = model.Director,
            GenreId = model.GenreId,
            ImageUrl = model.ImageUrl,
            Rating = model.Rating,
            Title = model.Title
        };

        await context.Movies.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task AddMovieToCollectionAsync(int movieId, string userId)
    {
        var user = await context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.UsersMovies)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new ArgumentException("Invalid User ID");
        }

        var movie = await context.Movies.FirstOrDefaultAsync(u => u.Id == movieId); 

        if (movie == null)
        {
            throw new ArgumentException("Invalid Movie ID");
        }

        if (!user.UsersMovies.Any(m => m.MovieId == movieId))
        {
            user.UsersMovies.Add(new UserMovie()
            {
                MovieId = movieId,
                UserId = userId,
                Movie = movie,
                User = user
            });
        }

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<MoviesAllViewsModels>> GetWatchedAsync(string userId)
    {
        var user = await context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.UsersMovies)
            .ThenInclude(um => um.Movie)
            .ThenInclude(m => m.Genre)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new ArgumentException("Invalid User ID");
        }

        return user.UsersMovies
            .Select(m => new MoviesAllViewsModels()
            {
                Director = m.Movie.Director,
                Genre = m.Movie.Genre.Name,
                Id = m.Movie.Id,
                Rating = m.Movie.Rating,
                ImageUrl = m.Movie.ImageUrl,
                Title = m.Movie.Title
            });
    }

    public async Task RemoveMovieFromCollectionAsync(int movieId, string userId)
    {
        var user = await context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.UsersMovies)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new ArgumentException("Invalid User ID");
        }

        var movie = user.UsersMovies.FirstOrDefault(m => m.MovieId == movieId);

        if (movie != null)
        {
            user.UsersMovies.Remove(movie);

            await context.SaveChangesAsync();
        }
    }
}
