using StreamXAPI.DTO.ActorDTO;
using StreamXAPI.Models;

namespace StreamXAPI.Services
{
    public interface IActorService
    {
        Task<List<Actor>> GetAllAsync();
        Task<Actor> GetByIdAsync(int id);
        Task AddAsync(CreateActorDTO ActorDTO);
        Task UpdateAsync(int id, UpdateActorDTO ActorDTO);
        Task DeleteAsync(int id);
    }
}
