using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiplayerGame.Models;

namespace MultiplayerGame.Data
{
    public class GameContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; }

        public GameContext(DbContextOptions<GameContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configuration
        }
    }
}
