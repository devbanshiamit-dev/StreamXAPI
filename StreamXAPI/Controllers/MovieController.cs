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


        // Movie Genre Endpoints

        [HttpPost("{movieId}/genres")]
        public async Task<IActionResult> AddGenres(int movieId, [FromBody] UpdateMovieGenresDTO dto)
        {
            await _mov.AddGenresAsync(movieId, dto);
            return Ok("Genres added successfully.");
        }

        [HttpDelete("{movieId}/genres/{genreId}")]
        public async Task<IActionResult> RemoveGenre(int movieId, int genreId)
        {
            await _mov.RemoveGenreAsync(movieId, genreId);
            return Ok("Genre removed successfully.");
        }


        // Movie Actor Endpoints

        [HttpPost("{movieId}/actors")]
        public async Task<IActionResult> AddActors(int movieId, [FromBody] UpdateMovieActorsDTO dto)
        {
            await _mov.AddActorsAsync(movieId, dto);
            return Ok("Actors added successfully.");
        }

        [HttpDelete("{movieId}/actors/{actorId}")]
        public async Task<IActionResult> RemoveActor(int movieId, int actorId)
        {
            await _mov.RemoveActorAsync(movieId, actorId);
            return Ok("Actor removed successfully.");
        }

        // Temp EndPoint Just For Testing.
        [HttpPost("seed")]
        public async Task<IActionResult> SeedMovies([FromBody] List<CreateMovieDTO> movies)
        {
            await _mov.AddMoviesAsync(movies);
            return Ok("Movies seeded successfully.");
        }
    }
}