using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamXAPI.DTO.ActorDTO;
using StreamXAPI.Services;

namespace StreamXAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _ser;

        public ActorController(IActorService ser)
        {
            _ser = ser;
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
    }
}
