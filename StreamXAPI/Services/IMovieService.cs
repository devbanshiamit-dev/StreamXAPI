using StreamXAPI.Models;
using StreamXAPI.Pagination;

namespace StreamXAPI.Services
{
    public interface IMovieService
    {
        Task<PagedResult<Movie>> GetAllMoviesAsync(MovieQueryParams queryParams);
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);
        Task<Movie?> GetMovieByIdAsync(int id);
    }
}