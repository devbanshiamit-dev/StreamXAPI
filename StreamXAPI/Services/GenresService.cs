using StreamXAPI.Models;
using StreamXAPI.Repo;
using StreamXAPI.CustomeExceptions;

namespace StreamXAPI.Services
{
    public class GenresService : IGenresService
    {
        private readonly IGenreRepository _repository;
        public GenresService(IGenreRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _repository.GetAllGenresAsync();
        }
        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            var result = await _repository.GetGenreByIdAsync(id);
            if (result == null)
            {
                throw new NotFoundException($"Genre with ID {id} not found.");
            }
            return result;
        }
        public async Task AddGenreAsync(Genre genre)
        {
            ValidateGenreProperty(genre);
            var existingGenre = await _repository.GetGenreByNameAsync(genre.GenreName);
            if (existingGenre != null)
            {
                throw new DuplicateException($"Genre with Name {genre.GenreName} already exists.");
            }
            await _repository.AddGenreAsync(genre);
        }
        public async Task UpdateGenreAsync(Genre genre)
        {
            ValidateGenreProperty(genre);
            var existingById = await _repository.GetGenreByIdAsync(genre.Id);
            if (existingById == null)
            {
                throw new NotFoundException($"Genre with ID {genre.Id} not found.");
            }
            var existingGenre = await _repository.GetGenreByNameAsync(genre.GenreName);
            if (existingGenre != null && existingGenre.Id != genre.Id)
            {
                throw new DuplicateException($"Genre with Name {genre.GenreName} already exists.");
            }
            await _repository.UpdateGenreAsync(genre);
        }
        public async Task DeleteGenreAsync(int id)
        {
            var existingGenre = await _repository.GetGenreByIdAsync(id);
            if (existingGenre == null)
            {
                throw new NotFoundException($"Genre with ID {id} not found.");
            }
            await _repository.DeleteGenreAsync(id);
        }

        private static void ValidateGenreProperty(Genre genre)
        {
            if (string.IsNullOrWhiteSpace(genre.GenreName))
            {
                throw new ValidationException("Genre name cannot be empty.");
            }
        }
    }
}
