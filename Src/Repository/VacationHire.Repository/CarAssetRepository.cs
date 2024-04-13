using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VacationHire.Data.Data;
using VacationHire.Data.Enum;
using VacationHire.Data.Models;
using VacationHire.Repository.Interfaces;

namespace VacationHire.Repository
{
    public class CarAssetRepository : RepositoryBase, ICarAssetRepository
    {
        private readonly ILogger<CarAssetRepository> _logger;

        public CarAssetRepository(VacationHireDbContext dbContext, ILogger<CarAssetRepository> logger) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<IList<CarAsset>> GetAll()
        {
            return await Context.CarAssets.ToListAsync();
        }

        public Task<CarAsset?> GetById(int id)
        {
            return Context.CarAssets.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IList<CarAsset>> GetByAssetId(int assetId)
        {
            return await Context.CarAssets.Where(e => e.AssetId == assetId).ToListAsync();
        }

        public async Task<CarAsset> Add(CarAsset carAsset)
        {
            try
            {
                var result = await Context.CarAssets.AddAsync(carAsset);
                await Context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                // String interpolation not used since we want to log this as a custom properties in Application Insights
                _logger.LogError(ex, "Unable to add car assert {CarAssetId}", carAsset.Id);
                return null;
            }
        }

        public Task Rent(int id)
        {
            return UpdateItem(id, AssetState.RentalPending);
        }

        public Task Return(int id)
        {
            return UpdateItem(id, AssetState.ReturnPending);
        }

        private async Task UpdateItem(int id, AssetState newState)
        {
            var item = await Context.CarAssets.FirstOrDefaultAsync(e => e.Id == id);
            if (item == null)
                throw new ArgumentException("Invalid car identifier");

            item.State = (short)newState;
            await Context.SaveChangesAsync();
        }
    }
}