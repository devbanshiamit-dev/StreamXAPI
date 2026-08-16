using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamXAPI.DTO.ActorDTO;
using StreamXAPI.Models;
using StreamXAPI.Repo;
using StreamXAPI.Services;

namespace StreamXAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _ser;
        private readonly IActorRepository _repo;

        public ActorController(IActorService ser , IActorRepository repository)
        {
            _ser = ser;
            _repo = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActors()
        {
            var actors = await _ser.GetAllAsync();
            return Ok(actors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActorById(int id)
        {
            var actor = await _ser.GetByIdAsync(id);
            return Ok(actor);
        }
        [HttpPost]
        public async Task<IActionResult> AddActor([FromBody] CreateActorDTO ActorDTO)
        {
            await _ser.AddAsync(ActorDTO);
            return CreatedAtAction(nameof(GetActorById), new { id = ActorDTO.Id }, ActorDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActor(int id, [FromBody] UpdateActorDTO ActorDTO)
        {
            await _ser.UpdateAsync(id, ActorDTO);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            await _ser.DeleteAsync(id);
            return NoContent();
        }

        //temp method
        [HttpPost]
        public async Task<IActionResult> AddActorsAsync(List<Actor> Act)
        {
            await _repo.AddInBulkAsync(Act);
            return Ok(new
            {
                message = "Actors Added",
                count =Act.Count,
            });
        }
    }
}
