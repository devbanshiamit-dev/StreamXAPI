using StreamXAPI.Models;
using StreamXAPI.Pagination;

namespace StreamXAPI.Repo
{
    public interface IMovieRepository
    {
        Task<PagedResult<Movie>> GetPagedMoviesAsync(PaginationParams paginationParams);
        Task<Movie?> GetByIdAsync(int id);
        Task<IEnumerable<Movie>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Movie>> SearchAsync(string searchTerm);
        Task<IEnumerable<Movie>> GetByActorAsync(string actorName);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task RemoveAsync(int id);
        Task<bool> ExistsByIdAsync(int id);
        Task<bool> ExistsByTitleAsync(string title);
        Task<bool> ExistsByTitleExceptIdAsync(string title, int id);
    }
}
