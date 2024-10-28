using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public Task<Region> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Region>> GetAllAsync()
        {
            return dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)=>
            await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

        public async Task<Region?> UpdateAsync(Guid Id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x=> x.Id == Id);
            if (region == null)
            {
                return null;
            }
            existingRegion.Name = region.Name.ToString();
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
             return existingRegion;
        }
      

        
    }
}
