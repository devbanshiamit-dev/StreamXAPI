using StreamXAPI.Models;

namespace StreamXAPI.Repo
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre?> GetGenreByIdAsync(int id);
        Task<Genre?> GetGenreByNameAsync(string name);
        Task AddGenreAsync(Genre genre);
        Task UpdateGenreAsync(Genre genre);
        Task DeleteGenreAsync(int id);
    }
}
