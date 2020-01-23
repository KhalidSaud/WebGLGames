//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGLGames.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Remote(action: "CheckName2", controller: "Games")]
        public string Name { get; set; }
        [MinLength(3, ErrorMessage = "Minimum length is 3 characters")]
        public string ReleaseName { get; set; }
        public string ReleaseNameDotJson { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
