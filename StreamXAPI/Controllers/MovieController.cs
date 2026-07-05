using Microsoft.AspNetCore.Mvc;
using StreamXAPI.CustomeExceptions;
using StreamXAPI.Models;
using StreamXAPI.Pagination;
using StreamXAPI.Services;

namespace StreamXAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _mov;

        public MovieController(IMovieService mov)
        {
            _mov = mov;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] Movie movie)
        {
            await _mov.AddMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie)
        {
            if (id != movie.Id)
                return BadRequest();

            await _mov.UpdateMovieAsync(movie);
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _mov.DeleteMovieAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _mov.GetAllMoviesAsync(paginationParams));
        }

        [HttpGet("actor/{actorName}")]
        public async Task<IActionResult> GetMoviesByActor(string actorName)
        {
            return Ok(await _mov.GetMoviesByActorAsync(actorName));
        }

        [HttpGet("Search/{searchTerm}")]
        public async Task<IActionResult> SearchMovies(string searchTerm)
        {
            return Ok(await _mov.SearchMoviesAsync(searchTerm));
        }
        

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetMoviesByCategory(int categoryId)
        {
            return Ok(await _mov.GetMoviesByCategoryAsync(categoryId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            return Ok(await _mov.GetMovieByIdAsync(id));
        }
    }
}
