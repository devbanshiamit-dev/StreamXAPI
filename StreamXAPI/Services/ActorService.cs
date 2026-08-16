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


        private static void IsValidDateOfBirth(DateOnly dob)
        {
            // 1. Optional: Reject default value if needed
            // if (dob == default)
            // {
            //     throw new ValidationException("Date of birth is required.");
            // }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            // 2. Cannot be in the future
            if (dob > today)
            {
                throw new ValidationException("Date of birth cannot be in the future.");
            }

            // 3. Maximum age check (e.g. 120 years)
            const int maxAge = 120;
            var minAllowedDate = today.AddYears(-maxAge);

            if (dob < minAllowedDate)
            {
                throw new ValidationException($"Date of birth cannot be more than {maxAge} years ago.");
            }
        }
    }
}
