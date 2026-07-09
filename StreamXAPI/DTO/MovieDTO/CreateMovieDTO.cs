using StreamXAPI.DTO.ActorDTO;
using System.ComponentModel.DataAnnotations;

namespace StreamXAPI.DTO.MovieDTO
{
    public class CreateMovieDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string ThumbnailUrl { get; set; } = string.Empty;
        [Required]
        public string BannerUrl { get; set; } = string.Empty;
        [Required]
        public string VideoUrl { get; set; } = string.Empty;
        [Required]
        public int Duration { get; set; }

        public double Rating { get; set; }

        public List<int> GenreIds { get; set; } = new();

        public List<MovieActorDTO> ActorIds { get; set; } = new();
    }
}
