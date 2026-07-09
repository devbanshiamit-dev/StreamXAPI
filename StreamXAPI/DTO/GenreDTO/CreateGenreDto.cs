using System.ComponentModel.DataAnnotations;

namespace StreamXAPI.DTO.GenreDTO
{
    public class CreateGenreDto
    {
        public int Id { get; set; }
        [Required]
        public string GenreName { get; set; } = string.Empty;
    }
}
