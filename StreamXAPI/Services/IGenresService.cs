using StreamXAPI.DTO.GenreDTO;
using StreamXAPI.Models;

namespace StreamXAPI.Services
{
    public interface IGenresService
    {
        Task<List<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(int id);
        Task AddGenreAsync(CreateGenreDto genre);
        Task UpdateGenreAsync(UpdateGenreDto genre);
        Task DeleteGenreAsync(int id);
    }
}
