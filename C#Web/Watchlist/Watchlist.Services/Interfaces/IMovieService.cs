namespace Watchlist.Services.Interfaces;

using Watchlist.Data.Models;
using Watchlist.View.Models.Movies;

public interface IMovieService
{
    Task<IEnumerable<MoviesAllViewsModels>> GetAllAsync();

    Task<IEnumerable<Genre>> GetGenresAsync();

    Task AddMovieAsync(MoviesAddViewModel model);

    Task AddMovieToCollectionAsync(int movieId, string userId);

    Task<IEnumerable<MoviesAllViewsModels>> GetWatchedAsync(string userId);

    Task RemoveMovieFromCollectionAsync(int movieId, string userId);
}
