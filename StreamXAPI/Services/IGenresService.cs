using StreamXAPI.Models;

namespace StreamXAPI.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(int id);
        Task AddGenreAsync(Genre genre);
        Task UpdateGenreAsync(Genre genre);
        Task DeleteGenreAsync(int id);
    }
}
