using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamXAPI.DTO.MovieDTO;
using StreamXAPI.Services;

namespace StreamXAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieActorsController : ControllerBase
    {
        private readonly IMovieService _service;

        public MovieActorsController(IMovieService service)
        {
            _service = service;
        }

        [HttpPost("{movieId}")]
        public async Task<IActionResult> AddActors(int movieId, [FromBody] UpdateMovieActorsDTO dto)
        {
            await _service.AddActorsAsync(movieId, dto);
            return Ok("Actors added successfully.");
        }

        [HttpDelete("{movieId}/{actorId}")]
        public async Task<IActionResult> RemoveActor(int movieId, int actorId)
        {
            await _service.RemoveActorAsync(movieId, actorId);
            return Ok("Actor removed successfully.");
        }
    }
}
