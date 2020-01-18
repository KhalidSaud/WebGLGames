using System.ComponentModel.DataAnnotations;

namespace WebGLGames.Models
{
    public class Game
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ReleaseName { get; set; }
        public string ReleaseNameDotJson { get; set; }

    }
}
