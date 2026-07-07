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
    }
}
