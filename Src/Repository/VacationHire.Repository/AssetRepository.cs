using Microsoft.EntityFrameworkCore;
using VacationHire.Data.Data;
using VacationHire.Data.Models;
using VacationHire.Repository.Interfaces;

namespace VacationHire.Repository
{
    public class AssetRepository(VacationHireDbContext dbContext) : RepositoryBase(dbContext), IAssetRepository
    {
        public async Task<IEnumerable<Asset>> GetAssets()
        {
            return await Context.Assets.ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssetsByCategory(int categoryId)
        {
            return await Context.Assets.Where(e => e.CategoryId == categoryId).ToListAsync();
        }

        public Task<Asset?> GetById(int id)
        {
            return Context.Assets.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Asset> AddAsset(Asset asset)
        {
            try
            {
                var result = await Context.Assets.AddAsync(asset);
                await Context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}