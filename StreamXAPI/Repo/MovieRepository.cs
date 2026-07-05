using StreamXAPI.Models;
using StreamXAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace StreamXAPI.Repo
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _con;
        public MovieRepository(AppDbContext context)
        {
            _con = context;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync() =>
        await _con.Movies
               .AsNoTracking()
               .ToListAsync();

        public async Task<Movie?> GetByIdAsync(int id) =>
            await _con.Movies.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<IEnumerable<Movie>> GetByCategoryAsync(int categoryId) =>
            await _con.Genres
                .Where(g => g.Id == categoryId)
                .SelectMany(g => g.MovieGenres.Select(mg => mg.Movie))
                .ToListAsync();

        public async Task<IEnumerable<Movie>> SearchAsync(string searchTerm) =>
            await _con.Movies
                .Where(m =>
                m.Title.Contains(searchTerm) || (m.Description != null &&
                m.Description.Contains(searchTerm)))
                .ToListAsync();

        public async Task<IEnumerable<Movie>> GetByActorAsync(string actorName) =>
            await _con.Movies
                .Where(m => m.MovieActors.Any(ma => ma.Actor.Name == actorName))
                .ToListAsync();


        public  async Task AddAsync(Movie movie)
        {
            _con.Movies.Add(movie);
            await _con.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _con.Movies.Update(movie);
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
    }
}