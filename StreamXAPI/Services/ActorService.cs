using StreamXAPI.DTO.ActorDTO;
using StreamXAPI.CustomeExceptions;
using StreamXAPI.Models;
using StreamXAPI.Repo;

namespace StreamXAPI.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actor;

        public ActorService(IActorRepository actor)
        {
            _actor = actor;
        }

        public async Task<List<Actor>> GetAllAsync()
        {
            return await _actor.GetAllActorsAsync();
        }
        public async Task<Actor> GetByIdAsync(int id)
        {
            var actor = await _actor.GetActorByIdAsync(id);
            if (actor == null)
            {
                throw new NotFoundException("Actor not found.");
            }
            return actor;
        }
        public async Task AddAsync(CreateActorDTO ActorDTO)
        {
            var existingActor = await _actor.GetActorByNameAsync(ActorDTO.Name);
            if (existingActor != null)
            {
                throw new DuplicateException("Actor with the same name already exists.");
            }

            if (string.IsNullOrWhiteSpace(ActorDTO.Name))
            {
                throw new ArgumentException("Actor name cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(ActorDTO.ImageUrl))
            {
                throw new ArgumentException("Actor image URL cannot be null or empty.");
            }

            IsValidDateOfBirth(ActorDTO.DateOfBirth);

            // Create a new Actor instance from the DTO
            var actor = new Actor
            {
                Name = ActorDTO.Name,
                DateOfBirth = ActorDTO.DateOfBirth,
                ImageUrl = ActorDTO.ImageUrl
            };

            await _actor.AddActorAsync(actor);
        }
        public async Task UpdateAsync(int id, UpdateActorDTO ActorDTO)
        {
            var existingActor = await _actor.GetActorByIdAsync(id);
            if (existingActor == null)
            {
                throw new NotFoundException("Actor not found.");
            }
            if (string.IsNullOrWhiteSpace(ActorDTO.Name))
            {
                throw new ArgumentException("Actor name cannot be null or empty.");
            }
            if (string.IsNullOrWhiteSpace(ActorDTO.Url))
            {
                throw new ArgumentException("Actor image URL cannot be null or empty.");
            }
            // Update the existing actor's properties

            existingActor.Name = ActorDTO.Name;
            existingActor.ImageUrl = ActorDTO.Url;
            await _actor.UpdateActorAsync(existingActor);
        }
        public async Task DeleteAsync(int id)
        {
            var existingActor = await _actor.GetActorByIdAsync(id);
            if (existingActor == null)
            {
                throw new NotFoundException("Actor not found.");
            }
            await _actor.DeleteActorAsync(existingActor);
        }


        private static void IsValidDateOfBirth(DateTime dob)
        {
            // 1. Check if the date was never set (is MinValue)
            if (dob == DateTime.MinValue)
            {
                throw new ValidationException("Date of birth is required.");
            }

            // 2. Check if the date is in the future
            if (dob > DateTime.Today)
            {
                throw new ValidationException("Date of birth cannot be in the future.");
            }

            // 3. Check for reasonable age limit (e.g., maximum 120 years old)
            const int maxAge = 120;
            if (dob < DateTime.Today.AddYears(-maxAge))
            {
                throw new ValidationException($"Date of birth cannot be more than {maxAge} years ago.");
            }
        }
    }
}
