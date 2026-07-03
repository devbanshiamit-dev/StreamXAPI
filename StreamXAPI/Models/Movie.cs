namespace StreamXAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public string ThumbnailUrl { get; set; } = string.Empty;

        public string BannerUrl { get; set; } = string.Empty;

        public string VideoUrl { get; set; } = string.Empty;

        public int Duration { get; set; } // Minutes


        public double Rating { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }

    public class Genre
    {
        public int Id { get; set; }

        public string GenreName { get; set; } = string.Empty;

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
    public class MovieGenre
    {
        public int MovieId { get; set; }

        public Movie Movie { get; set; } = null!;

        public int GenreId { get; set; }

        public Genre Genre { get; set; } = null!;
    }

    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
    public class MovieActor
    {
        public int MovieId { get; set; }

        public Movie Movie { get; set; } = null!;

        public int ActorId { get; set; }

        public Actor Actor { get; set; } = null!;

        public string CharacterName { get; set; } = string.Empty;
    }
}
