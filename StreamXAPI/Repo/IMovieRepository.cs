using StreamXAPI.DTO.GenreDTO;
using StreamXAPI.DTO.MovieDTO;
using StreamXAPI.Models;
using StreamXAPI.Pagination;

namespace StreamXAPI.Repo
{
    public interface IMovieRepository
    {
        Task<PagedResult<Movie>> GetPagedMoviesAsync(MovieQueryParams queryParams);
        Task<Movie?> GetByIdAsync(int id);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task RemoveAsync(int id);
        Task<bool> ExistsByIdAsync(int id);
        Task<bool> ExistsByTitleAsync(string title);
        Task<bool> ExistsByTitleExceptIdAsync(string title, int id);
        Task AddActorsAsync(Movie movie, UpdateMovieActorsDTO actors);
        Task RemoveActorAsync(MovieActor actor);
        Task AddGenresAsync(Movie movie, UpdateMovieGenresDTO dto);
        Task RemoveGenreAsync(MovieGenre genre);
        Task AddRangeAsync(List<Movie> movies);
    }
}
