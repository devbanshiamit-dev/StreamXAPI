using Microsoft.EntityFrameworkCore;
using StreamXAPI.Data;
using StreamXAPI.DTO.GenreDTO;
using StreamXAPI.DTO.MovieDTO;
using StreamXAPI.Models;
using StreamXAPI.Pagination;

namespace StreamXAPI.Repo
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _con;
        public MovieRepository(AppDbContext context)
        {
            _con = context;
        }

        public async Task<PagedResult<Movie>> GetPagedMoviesAsync(MovieQueryParams queryParams)
        {
            var query = _con.Movies
              .Include(m => m.MovieGenres)
              .ThenInclude(mg => mg.Genre)
              .Include(m => m.MovieActors)
              .ThenInclude(ma => ma.Actor)
              .AsNoTracking();

            if (!string.IsNullOrEmpty(queryParams.Genre))
            {
                query = query.Where(m =>
                    m.MovieGenres.Any(mg =>
                    mg.Genre.GenreName == queryParams.Genre));
            }

            if (!string.IsNullOrWhiteSpace(queryParams.Actor))
            {
                query = query.Where(m => m.MovieActors.Any(ma =>
                      ma.Actor.Name == queryParams.Actor));
            }

            if (!string.IsNullOrWhiteSpace(queryParams.Search))
            {
                query = query.Where(m =>
                      m.Title.Contains(queryParams.Search) ||

                      m.Description.Contains(queryParams.Search) ||

                      m.MovieGenres.Any(mg =>
                          mg.Genre.GenreName.Contains(queryParams.Search)) ||

                      m.MovieActors.Any(ma =>
                          ma.Actor.Name.Contains(queryParams.Search))
                );
            }


            var totalCount = await query.CountAsync();

            query = queryParams.SortBy?.ToLower() switch
            {
                "title" => query.OrderBy(m => m.Title),

                "title_desc" => query.OrderByDescending(m => m.Title),

                "rating" => query.OrderByDescending(m => m.Rating),

                "rating_asc" => query.OrderBy(m => m.Rating),

                "releasedate" => query.OrderByDescending(m => m.ReleaseDate),

                "releasedate_asc" => query.OrderBy(m => m.ReleaseDate),

                "duration" => query.OrderBy(m => m.Duration),

                "duration_desc" => query.OrderByDescending(m => m.Duration),

                _ => query.OrderBy(m => m.Id)
            };

            var items = await query
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return new PagedResult<Movie>
            {
                TotalRecords = totalCount,
                Items = items
            };
        }

        public async Task<Movie?> GetByIdAsync(int id) =>
            await _con.Movies
            .Include(m => m.MovieGenres)
            .Include(m => m.MovieActors)
            .FirstOrDefaultAsync(m => m.Id == id);

        public async Task<bool> ExistsByTitleExceptIdAsync(string title, int id) =>
            await _con.Movies.AnyAsync(m => m.Title == title && m.Id != id);

        public async Task AddAsync(Movie movie)
        {
            _con.Movies.Add(movie);
            await _con.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            await _con.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var existingMovie = await _con.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (existingMovie != null)
            {
                _con.Movies.Remove(existingMovie);
                await _con.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByIdAsync(int id) =>
            await _con.Movies.AnyAsync(m => m.Id == id);

        public async Task<bool> ExistsByTitleAsync(string title) =>
            await _con.Movies.AnyAsync(m => m.Title == title);

        public async Task AddActorsAsync(Movie movie, UpdateMovieActorsDTO actors)
        {
            foreach (var actor in actors.Actors)
            {
                movie.MovieActors.Add(new MovieActor
                {
                    ActorId = actor.ActorId,
                    CharacterName = actor.CharacterName,
                });
            }
            await _con.SaveChangesAsync();
        }
        public async Task RemoveActorAsync(MovieActor actor)
        {
            _con.MovieActors.Remove(actor);
            await _con.SaveChangesAsync();
        }

        public async Task AddGenresAsync(Movie movie, UpdateMovieGenresDTO dto)
        {
            foreach (var genreId in dto.GenreIds)
            {
                movie.MovieGenres.Add(new MovieGenre
                {
                    GenreId = genreId
                });
            }

            await _con.SaveChangesAsync();
        }
        public async Task RemoveGenreAsync(MovieGenre genre)
        {
            _con.MovieGenres.Remove(genre);
            await _con.SaveChangesAsync();
        }
    }
}