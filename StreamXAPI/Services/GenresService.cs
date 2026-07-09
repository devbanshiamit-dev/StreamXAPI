using StreamXAPI.Models;
using StreamXAPI.Repo;
using StreamXAPI.CustomeExceptions;
using StreamXAPI.DTO.GenreDTO;

namespace StreamXAPI.Services
{
    public class GenresService : IGenresService
    {
        private readonly IGenreRepository _repository;
        public GenresService(IGenreRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Genre>> GetAllGenresAsync()
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
        public async Task AddGenreAsync(CreateGenreDto genre)
        {
            if (string.IsNullOrWhiteSpace(genre.GenreName))
            {
                throw new ValidationException("Genre name cannot be empty.");
            }

            var existingGenre = await _repository.GetGenreByNameAsync(genre.GenreName);
            if (existingGenre != null)
            {
                throw new DuplicateException($"Genre with Name {genre.GenreName} already exists.");
            }

            var genreEntity = new Genre
            {
                GenreName = genre.GenreName
            };

            await _repository.AddGenreAsync(genreEntity);
        }
        public async Task UpdateGenreAsync(UpdateGenreDto genre)
        {
            var existingById = await _repository.GetGenreByIdAsync(genre.Id);

            if (existingById == null)
                throw new NotFoundException($"Genre Not Found");

            existingById.GenreName = genre.GenreName;

            await _repository.UpdateGenreAsync(existingById);
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
    }
}
