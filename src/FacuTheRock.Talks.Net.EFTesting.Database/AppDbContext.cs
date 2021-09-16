using Microsoft.EntityFrameworkCore;
using FacuTheRock.Talks.Net.EFTesting.Database.Models;

namespace FacuTheRock.Talks.Net.EFTesting.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Player> Players { get; set; }
    }
}
