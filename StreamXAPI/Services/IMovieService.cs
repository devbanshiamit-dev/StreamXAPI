using StreamXAPI.Models;

namespace StreamXAPI.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);
        Task<Movie?> GetMovieByIdAsync(int id);
        Task<IEnumerable<Movie>> GetMoviesByActorAsync(string actorName);
        Task<IEnumerable<Movie>> SearchMoviesAsync(string searchTerm);
        Task<IEnumerable<Movie>> GetMoviesByCategoryAsync(int categoryId);
    }
}