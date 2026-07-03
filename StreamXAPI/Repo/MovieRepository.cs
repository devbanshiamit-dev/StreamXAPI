using StreamXAPI.Models;
using StreamXAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace StreamXAPI.Repo
{
    public class MovieRepository
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


        public async Task<IEnumerable<Movie>> GetByActorAsync(int actorId) =>
            await _con.Actors
                .Where(a => a.Id == actorId)
                .SelectMany(a => a.MovieActors.Select(ma => ma.Movie))
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

        public async Task RemoveAsync(Movie movie)
        {
            _con.Movies.Remove(movie);
            await _con.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _con.Movies.AnyAsync(m => m.Id == id);
    }
}
