using VacationHire.Data.Enum;
using VacationHire.RentalService.BusinessObjects;
using VacationHire.RentalService.Interfaces;
using VacationHire.RentalService.Responses;
using VacationHire.Repository.Interfaces;

namespace VacationHire.RentalService.Implementations
{
    public class CabinAssetsRentableService : IRentableService
    {
        private readonly ICabinAssetRepository _cabinAssetRepository;
        public CabinAssetsRentableService(ICabinAssetRepository cabinAssetRepository)
        {
            _cabinAssetRepository = cabinAssetRepository;
        }

        /// <summary>
        ///     Improvements: the method displays all the rentable items
        /// - it should have some checks and return only the items that are available for rent
        /// - performance wise: it should be paginated when the number of items is large 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Rentable>> GetRentableItems()
        {
            var cabins = await _cabinAssetRepository.GetAll();
            return cabins.Select(car => new CabinRentable(car.Id, car.Description, car.AssetId, AssetState.Available.ToString())
            {
                Address = car.Address,
                NoOfRooms = car.NoOfRooms,
            }).Cast<Rentable>().ToList();
        }

        public async Task<Rentable> GetItemById(int id)
        {
            var item = await _cabinAssetRepository.GetById(id);

            if (item == null)
                throw new ArgumentException("Invalid cabin identifier");

            return new CabinRentable(item.Id, item.Description, item.AssetId, AssetState.Available.ToString())
            {
                Address = item.Address,
                NoOfRooms = item.NoOfRooms,
            };
        }

        public async Task<RentRequestResult> RentAnItem(Rentable? rentableItem)
        {
            if (rentableItem is not CabinRentable cabinRentable)
                throw new ArgumentException("Invalid rentable item");

            // More check should be done here to ensure the item is still available for rent, check if the update succeeded
            await _cabinAssetRepository.Rent(cabinRentable.Id);
            return new RentRequestResult
            {
                RentRequestId = 42,
                RentableAssetId = cabinRentable.Id,
            };
        }

        public async Task<ReturnRequestResult> ReturnAnItem(Rentable rentableItem)
        {
            if (rentableItem is not CabinRentable cabinRentable)
                throw new ArgumentException("Invalid rentable item");

            // More check should be done here before or after rental allowed period, check if the update succeeded
            await _cabinAssetRepository.Rent(cabinRentable.Id);
            return new ReturnRequestResult
            {
                ReturnRequestId = 12,
                RentableAssetId = cabinRentable.Id,
            };
        }
    }
}