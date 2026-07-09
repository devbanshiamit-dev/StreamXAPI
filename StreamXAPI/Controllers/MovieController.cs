using Microsoft.AspNetCore.Mvc;
using StreamXAPI.DTO.MovieDTO;
using StreamXAPI.DTO.ActorDTO;
using StreamXAPI.DTO.GenreDTO;
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
        public async Task<IActionResult> AddMovie([FromBody] CreateMovieDTO movie)
        {
            var createdMovie = await _mov.AddMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.Id }, createdMovie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] UpdateMovieDTO movie)
        {
            await _mov.UpdateMovieAsync(id ,movie);
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _mov.DeleteMovieAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] MovieQueryParams queryParams)
        {
            return Ok(await _mov.GetAllMoviesAsync(queryParams));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            return Ok(await _mov.GetMovieByIdAsync(id));
        }
    }
}
