using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VacationHire.Data.Data;
using VacationHire.Data.Enum;
using VacationHire.Data.Models;
using VacationHire.Repository.Interfaces;

namespace VacationHire.Repository
{
    public class CabinAssetRepository : RepositoryBase, ICabinAssetRepository
    {
        private readonly ILogger<CabinAssetRepository> _logger;

        public CabinAssetRepository(VacationHireDbContext dbContext, ILogger<CabinAssetRepository> logger) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<IList<CabinAsset>> GetAll()
        {
            return await Context.CabinAssets.ToListAsync();
        }

        public Task<CabinAsset?> GetById(int id)
        {
            return Context.CabinAssets.FirstOrDefaultAsync(e => e.Id == id);
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
            var item = await Context.CabinAssets.FirstOrDefaultAsync(e => e.Id == id);
            if (item == null)
                throw new ArgumentException("Invalid car identifier");

            item.State = (short)newState;
            await Context.SaveChangesAsync();
        }
    }
}