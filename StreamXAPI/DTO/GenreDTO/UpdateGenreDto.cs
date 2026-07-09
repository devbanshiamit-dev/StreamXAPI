using System.ComponentModel.DataAnnotations;

namespace StreamXAPI.DTO.GenreDTO
{
    public class UpdateGenreDto
    {
        public int Id { get; set; }
        [Required]
        public string GenreName { get; set; } = string.Empty;
    }
}
