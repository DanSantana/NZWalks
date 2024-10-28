using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        // o valor retornado poderia ser null
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region> UpdateAsync(Guid Id, Region region);
        Task<Region> DeleteAsync(Guid Id);


    }
}
