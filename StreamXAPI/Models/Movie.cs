using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StreamXAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Url]
        public string ThumbnailUrl { get; set; } = string.Empty;

        [Url]
        public string BannerUrl { get; set; } = string.Empty;

        [Url]
        public string VideoUrl { get; set; } = string.Empty;

        [Range(1, 600)]
        public int Duration { get; set; } // Minutes


        [Range(0, 10)]
        public double Rating { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }

    public class Genre
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        [Column("Name")]
        public string GenreName { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
    public class MovieGenre
    {
        public int MovieId { get; set; }

        [JsonIgnore]
        public Movie Movie { get; set; } = null!;

        public int GenreId { get; set; }

        public Genre Genre { get; set; } = null!;
    }

    public class Actor
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        [Url]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
    public class MovieActor
    {
        public int MovieId { get; set; }

        [JsonIgnore]
        public Movie Movie { get; set; } = null!;

        public int ActorId { get; set; }

        public Actor Actor { get; set; } = null!;

        public string CharacterName { get; set; } = string.Empty;
    }
}
