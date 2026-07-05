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

            if (await _repo.ExistsByTitleAsync(movie.Title)) { throw new ArgumentException("Movie Already Exists"); }

            await _repo.AddAsync(movie);
        }
        public async Task UpdateMovieAsync(Movie movie)
        {
            ValidateMovie(movie);

            if (!await _repo.ExistsByIdAsync(movie.Id)) { throw new ArgumentException("Movie Not Found"); }

            if (! await _repo.ExistsByTitleAsync(movie.Title)) { throw new ArgumentException("Movie Title Already Exists"); }

            await _repo.UpdateAsync(movie);
        }
        public async Task DeleteMovieAsync(int id)
        {
            if (!await _repo.ExistsByIdAsync(id)) { throw new ArgumentException("Movie Not Found"); }
            await _repo.RemoveAsync(id);
        }
        public async Task<IEnumerable<Movie>> GetMoviesByActorAsync(string actorName)
        {
            if (string.IsNullOrWhiteSpace(actorName))
                throw new ArgumentException("Actor name cannot be null or empty.");
            
            return await _repo.GetByActorAsync(actorName);
        }
        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be null or empty.");
            
            return await _repo.SearchAsync(searchTerm);
        }
        public async Task<IEnumerable<Movie>> GetMoviesByCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Category ID must be greater than zero.");
      
            return await _repo.GetByCategoryAsync(categoryId);
        }


        private static void ValidateMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            if (movie.Duration <= 0)
                throw new ArgumentException("Invalid duration.");

            if (string.IsNullOrWhiteSpace(movie.Title))
                throw new ArgumentException("Title is required.");

            if (movie.ReleaseDate > DateTime.UtcNow)
                throw new ArgumentException("Release date is required.");
        }
    }
}