using StreamXAPI.CustomeExceptions;
using StreamXAPI.Models;
using StreamXAPI.Repo;

namespace StreamXAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;
        public MovieService(IMovieRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Movie>> GetAllMoviesAsync() => _repo.GetAllAsync();

        public Task<Movie?> GetMovieByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task AddMovieAsync(Movie movie)
        {
            ValidateMovie(movie);

            if (await _repo.ExistsByTitleExceptIdAsync(movie.Title, movie.Id)) { throw new DuplicateException("Movie Title Already Exists"); }

            await _repo.AddAsync(movie);
        }
        public async Task UpdateMovieAsync(Movie movie)
        {
            ValidateMovie(movie);

            if (!await _repo.ExistsByIdAsync(movie.Id))
                throw new NotFoundException("Movie not found.");

            if (await _repo.ExistsByTitleExceptIdAsync(movie.Title, movie.Id))
                throw new DuplicateException("Movie title already exists.");

            await _repo.UpdateAsync(movie);
        }
        public async Task DeleteMovieAsync(int id)
        {
            if (!await _repo.ExistsByIdAsync(id)) { throw new NotFoundException("Movie Not Found"); }
            await _repo.RemoveAsync(id);
        }
        public async Task<IEnumerable<Movie>> GetMoviesByActorAsync(string actorName)
        {
            if (string.IsNullOrWhiteSpace(actorName))
                throw new ValidationException("Actor name cannot be null or empty.");
            
            return await _repo.GetByActorAsync(actorName);
        }
        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ValidationException("Search term cannot be null or empty.");
            
            return await _repo.SearchAsync(searchTerm);
        }
        public async Task<IEnumerable<Movie>> GetMoviesByCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
                throw new ValidationException("Category ID must be greater than zero.");
      
            return await _repo.GetByCategoryAsync(categoryId);
        }


        private static void ValidateMovie(Movie movie)
        {
            if (movie == null)
                throw new ValidationException("Movie cannot be null.");

            if (movie.Duration <= 0)
                throw new ValidationException("Invalid duration.");

            if (string.IsNullOrWhiteSpace(movie.Title))
                throw new ValidationException("Title is required.");

            if (movie.ReleaseDate > DateTime.UtcNow)
                throw new ValidationException("Release date cannot be in the future.");
        }
    }
}