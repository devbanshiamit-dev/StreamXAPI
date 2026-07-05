using Microsoft.AspNetCore.Mvc;
using StreamXAPI.Models;
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

        [HttpPost("Add")]
        public async Task<IActionResult> AddMovie(Movie movie)
        {
            await _mov.AddMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovies), new { id = movie.Id }, movie);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateMovie(Movie movie)
        {
            await _mov.UpdateMovieAsync(movie);
            return Ok(movie);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _mov.DeleteMovieAsync(id);
            return NoContent();
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _mov.GetAllMoviesAsync());
        }

        [HttpGet("GetByActor/{actorName}")]
        public async Task<IActionResult> GetMoviesByActor(string actorName)
        {
            return Ok(await _mov.GetMoviesByActorAsync(actorName));
        }

        [HttpGet("Search/{searchTerm}")]
        public async Task<IActionResult> SearchMovies(string searchTerm)
        {
            return Ok(await _mov.SearchMoviesAsync(searchTerm));
        }
        

        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<IActionResult> GetMoviesByCategory(int categoryId)
        {
            return Ok(await _mov.GetMoviesByCategoryAsync(categoryId));
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            return Ok(await _mov.GetMovieByIdAsync(id));
        }
    }
}
