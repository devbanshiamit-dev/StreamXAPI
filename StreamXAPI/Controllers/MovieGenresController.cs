using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamXAPI.DTO.GenreDTO;
using StreamXAPI.Services;

namespace StreamXAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieGenresController : ControllerBase
    {
        private readonly IMovieService _service;

        public MovieGenresController(IMovieService service)
        {
            _service = service;
        }

        [HttpPost("{movieId}")]
        public async Task<IActionResult> AddGenres(int movieId, [FromBody] UpdateMovieGenresDTO dto)
        {
            await _service.AddGenresAsync(movieId, dto);
            return Ok("Genres added successfully.");
        }

        [HttpDelete("{movieId}/{genreId}")]
        public async Task<IActionResult> RemoveGenre(int movieId, int genreId)
        {
            await _service.RemoveGenreAsync(movieId, genreId);
            return Ok("Genre removed successfully.");
        }
    }
}
