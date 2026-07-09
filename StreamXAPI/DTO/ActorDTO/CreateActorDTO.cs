using System.ComponentModel.DataAnnotations;

namespace StreamXAPI.DTO.ActorDTO
{
    public class CreateActorDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Url]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
