using System.ComponentModel.DataAnnotations;

namespace StreamXAPI.DTO.ActorDTO
{
    public class MovieActorDTO
    {
        [Required]
        public int ActorId { get; set; }
        [Required]
        public string CharacterName { get; set; } = string.Empty;
    }
}
