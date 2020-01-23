using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebGLGames.Models;

namespace WebGLGames.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Game>()
                .HasIndex(c => c.Name)
                .IsUnique(true);
            modelBuilder.Entity<IdentityUser>()
                .HasIndex(c => c.Email)
                .IsUnique(true);
        }


        public DbSet<User> IdentityUsers { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
