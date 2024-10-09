using Microsoft.EntityFrameworkCore;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOrions):base(dbContextOrions)
        {
            
        }
        DbSet<Difficulty> Difficulties { get; set; }

        DbSet<Region> Regions { get; set; }

        DbSet<Walk> Walks { get; set; }

    }
}
