using StreamXAPI.DTO.ActorDTO;

namespace StreamXAPI.DTO.MovieDTO
{
    public class UpdateMovieDTO
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public string ThumbnailUrl { get; set; } = string.Empty;

        public string BannerUrl { get; set; } = string.Empty;

        public string VideoUrl { get; set; } = string.Empty;

        public int Duration { get; set; }

        public double Rating { get; set; }
    }
}
