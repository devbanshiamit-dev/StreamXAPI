using StreamXAPI.DTO.GenreDTO;
using StreamXAPI.DTO.MovieDTO;
using StreamXAPI.Models;
using StreamXAPI.Pagination;

namespace StreamXAPI.Services
{
    public interface IMovieService
    {
        Task<PagedResult<Movie>> GetAllMoviesAsync(MovieQueryParams queryParams);
        Task<Movie> AddMovieAsync(CreateMovieDTO movie);
        Task UpdateMovieAsync(int id, UpdateMovieDTO movie);
        Task DeleteMovieAsync(int id);
        Task<Movie?> GetMovieByIdAsync(int id);
        Task AddActorsAsync(int id, UpdateMovieActorsDTO Dto);
        Task RemoveActorAsync(int movieId, int actorId);
        Task AddGenresAsync(int movieId, UpdateMovieGenresDTO dto);
        Task RemoveGenreAsync(int movieId, int genreId);
    }
}