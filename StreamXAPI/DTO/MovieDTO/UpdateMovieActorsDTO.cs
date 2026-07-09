using StreamXAPI.DTO.ActorDTO;

namespace StreamXAPI.DTO.MovieDTO
{
    public class UpdateMovieActorsDTO
    {
        public List<MovieActorDTO> Actors { get; set; } = new List<MovieActorDTO>();
    }
}
