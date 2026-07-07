using StreamXAPI.CustomeExceptions;
using StreamXAPI.Models;
using StreamXAPI.Pagination;
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

        public async Task<PagedResult<Movie>> GetAllMoviesAsync(MovieQueryParams queryParams)
        {
            var pagedMovies = await _repo.GetPagedMoviesAsync(queryParams);

            var TotalPages = (int)Math.Ceiling((double)pagedMovies.TotalRecords / queryParams.PageSize);

            return await Task.FromResult(new PagedResult<Movie>
            {
                Items = pagedMovies.Items,
                TotalPages = TotalPages,
                TotalRecords = pagedMovies.TotalRecords,
                CurrentPage = queryParams.PageNumber,
                PageSize = queryParams.PageSize
            });
        }

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