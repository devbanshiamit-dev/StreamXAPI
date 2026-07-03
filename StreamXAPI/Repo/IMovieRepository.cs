using StreamXAPI.Models;

namespace StreamXAPI.Repo
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task<IEnumerable<Movie>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Movie>> SearchAsync(string searchTerm);
        Task<IEnumerable<Movie>> GetByActorAsync(int actorId);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task RemoveAsync(Movie movie);
        Task<bool> ExistsAsync(int id);
    }
}
