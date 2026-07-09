using Microsoft.EntityFrameworkCore;
using StreamXAPI.Data;
using StreamXAPI.Models;

namespace StreamXAPI.Repo
{
    public class ActorRepository : IActorRepository
    {
        private readonly AppDbContext _con;

        public ActorRepository(AppDbContext con)
        {
            _con = con;
        }
        //Get Methods To retrieve all actors, get actor by id, and get actor by name
        public async Task<List<Actor>> GetAllActorsAsync()
        {
            return await _con.Actors.AsNoTracking().ToListAsync();
        }
        public async Task<Actor?> GetActorByIdAsync(int id)
        {
            return await _con.Actors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Actor?> GetActorByNameAsync(string name)
        {
            return await _con.Actors.FirstOrDefaultAsync(a => a.Name == name);
        }

        // Add Method to add a new actor to the database
        public async Task AddActorAsync(Actor actor)
        {
            _con.Actors.Add(actor);
            await _con.SaveChangesAsync();
        }

        // Update Method to update an existing actor in the database
        public async Task UpdateActorAsync(Actor actor)
        {
            _con.Actors.Update(actor);
            await _con.SaveChangesAsync();
        }

        // Delete Method to delete an existing actor from the database

        public async Task DeleteActorAsync(Actor actor)
        {
            _con.Actors.Remove(actor);
            await _con.SaveChangesAsync();
        }
    }
}
