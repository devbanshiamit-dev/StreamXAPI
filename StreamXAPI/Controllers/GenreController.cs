using Microsoft.AspNetCore.Mvc;
using StreamXAPI.DTO.GenreDTO;
using StreamXAPI.Repo;
using StreamXAPI.Services;

namespace StreamXAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenresService _ser;
        private readonly IGenreRepository repository; //temp
        public GenreController(IGenresService ser, IGenreRepository repository)
        {
            _ser = ser;
            this.repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            return Ok(await _ser.GetAllGenresAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre(int id)
        {
            return Ok(await _ser.GetGenreByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] CreateGenreDto genre)
        {
            await _ser.AddGenreAsync(genre);
            return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] UpdateGenreDto genre)
        {
            await _ser.UpdateGenreAsync(genre);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _ser.DeleteGenreAsync(id);
            return NoContent();
        }

        //temp
        [HttpPost("bulk-genres")]
        public async Task<IActionResult> AddBulkGenres([FromBody] List<CreateGenreDto> genres)
        {
            await repository.AddGenresBulkAsync(genres);

            return Ok(new
            {
                message = "Genres added successfully",
                count = genres.Count
            });
        }
    }
}
