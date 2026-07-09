using Microsoft.EntityFrameworkCore;
using StreamXAPI.Data;
using StreamXAPI.Models;

namespace StreamXAPI.Repo
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _context;
        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.AsNoTracking().ToListAsync();
        }
        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }
        public async Task<Genre?> GetGenreByNameAsync(string GenreName)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.GenreName == GenreName);
        }
        public async Task AddGenreAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateGenreAsync(Genre genre)
        {
            await _context.SaveChangesAsync();
        }
        public async Task DeleteGenreAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
}