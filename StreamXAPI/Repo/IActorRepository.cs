using StreamXAPI.Models;

namespace StreamXAPI.Repo
{
    public interface IActorRepository
    {
        Task<List<Actor>> GetAllActorsAsync();
        Task<Actor?> GetActorByIdAsync(int id);
        Task<Actor?> GetActorByNameAsync(string name);
        Task AddActorAsync(Actor actor);
        Task UpdateActorAsync(Actor actor);
        Task DeleteActorAsync(Actor actor);
    }
}
