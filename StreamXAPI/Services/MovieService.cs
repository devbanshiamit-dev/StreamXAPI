using StreamXAPI.CustomeExceptions;
using StreamXAPI.DTO.GenreDTO;
using StreamXAPI.DTO.MovieDTO;
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

        public async Task<Movie> AddMovieAsync(CreateMovieDTO movie)
        {
            if (movie == null)
                throw new ValidationException("Movie cannot be null.");

            if (movie.Duration <= 0)
                throw new ValidationException("Invalid duration.");

            if (string.IsNullOrWhiteSpace(movie.Title))
                throw new ValidationException("Title is required.");

            if (movie.ReleaseDate > DateTime.UtcNow)
                throw new ValidationException("Release date cannot be in the future.");

            if (movie.GenreIds == null || !movie.GenreIds.Any())
                throw new ValidationException("At least one genre is required.");

            if (movie.ActorIds == null || !movie.ActorIds.Any())
                throw new ValidationException("At least one actor is required.");

            if (await _repo.ExistsByTitleAsync(movie.Title)) { throw new DuplicateException("Movie Title Already Exists"); }

            var newMovie = new Movie
            {
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                ThumbnailUrl = movie.ThumbnailUrl,
                BannerUrl = movie.BannerUrl,
                VideoUrl = movie.VideoUrl,
                Duration = movie.Duration,
                Rating = movie.Rating,
                MovieGenres = movie.GenreIds.Select(genreId => new MovieGenre { GenreId = genreId }).ToList(),
                MovieActors = movie.ActorIds.Select(actor => new MovieActor { ActorId = actor.ActorId, CharacterName = actor.CharacterName }).ToList()
            };
            await _repo.AddAsync(newMovie);
            
            return newMovie;
        }
        public async Task UpdateMovieAsync(int id, UpdateMovieDTO movie)
        {
            if (movie == null)
                throw new ValidationException("Movie cannot be null.");

            if (movie.Duration <= 0)
                throw new ValidationException("Invalid duration.");

            if (string.IsNullOrWhiteSpace(movie.Title))
                throw new ValidationException("Title is required.");

            if (movie.ReleaseDate > DateTime.UtcNow)
                throw new ValidationException("Release date cannot be in the future.");

            var existingMovie = await _repo.GetByIdAsync(id);

            if (existingMovie == null)
                throw new NotFoundException("Movie not found.");

            if (!await _repo.ExistsByTitleExceptIdAsync(movie.Title, id))
                throw new DuplicateException("Movie Title Already Exists");

            existingMovie.Title = movie.Title;
            existingMovie.Description = movie.Description;
            existingMovie.ReleaseDate = movie.ReleaseDate;
            existingMovie.ThumbnailUrl = movie.ThumbnailUrl;
            existingMovie.BannerUrl = movie.BannerUrl;
            existingMovie.VideoUrl = movie.VideoUrl;
            existingMovie.Duration = movie.Duration;
            existingMovie.Rating = movie.Rating;

            await _repo.UpdateAsync(existingMovie);
        }
        public async Task DeleteMovieAsync(int id)
        {
            if (!await _repo.ExistsByIdAsync(id)) { throw new NotFoundException("Movie Not Found"); }
            await _repo.RemoveAsync(id);
        }

        public async Task AddActorsAsync(int id, UpdateMovieActorsDTO dto)
        {
            var movie = await _repo.GetByIdAsync(id);

            if (movie == null)
                throw new NotFoundException("Movie not found.");

            foreach(var actor in dto.Actors)
            {
                if(movie.MovieActors.Any(a => a.ActorId == actor.ActorId))
                {
                    throw new DuplicateException($"Actor with Id {actor.ActorId} is already in this movie.");
                }
            }
            await _repo.AddActorsAsync(movie, dto);
        }
        public async Task RemoveActorAsync(int movieId, int actorId)
        {
            var movie = await _repo.GetByIdAsync(movieId);

            if (movie == null)
                throw new NotFoundException("Movie not found.");

            var actor = movie.MovieActors.FirstOrDefault(x => x.ActorId == actorId);

            if (actor == null)
                throw new NotFoundException("Actor not found in this movie.");

            await _repo.RemoveActorAsync(actor);
        }

        public async Task AddGenresAsync(int movieId, UpdateMovieGenresDTO dto)
        {
            var movie = await _repo.GetByIdAsync(movieId);

            if (movie == null)
                throw new NotFoundException("Movie not found.");

            foreach (var genreId in dto.GenreIds)
            {
                if (movie.MovieGenres.Any(x => x.GenreId == genreId))
                    throw new DuplicateException($"Genre {genreId} already exists in this movie.");
            }

            await _repo.AddGenresAsync(movie, dto);
        }

        public async Task RemoveGenreAsync(int movieId, int genreId)
        {
            var movie = await _repo.GetByIdAsync(movieId);

            if (movie == null)
                throw new NotFoundException("Movie not found.");

            var genre = movie.MovieGenres.FirstOrDefault(x => x.GenreId == genreId);

            if (genre == null)
                throw new NotFoundException("Genre not found in this movie.");

            await _repo.RemoveGenreAsync(genre);
        }
    }
}