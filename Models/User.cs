using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebGLGames.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
