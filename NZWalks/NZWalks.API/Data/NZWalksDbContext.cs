using Microsoft.EntityFrameworkCore;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOrions) : base(dbContextOrions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

    }
}
