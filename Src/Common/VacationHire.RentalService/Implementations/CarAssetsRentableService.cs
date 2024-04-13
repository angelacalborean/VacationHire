using VacationHire.Data.Enum;
using VacationHire.RentalService.BusinessObjects;
using VacationHire.RentalService.Interfaces;
using VacationHire.RentalService.Responses;
using VacationHire.Repository.Interfaces;

namespace VacationHire.RentalService.Implementations
{
    public class CarAssetsRentableService : IRentableService
    {
        private readonly ICarAssetRepository _carAssetRepository;
        public CarAssetsRentableService(ICarAssetRepository carAssetRepository)
        {
            _carAssetRepository = carAssetRepository;
        }

        /// <summary>
        ///     The call to the repository is not entirely correct: there is no filter o the available items:
        /// in a real scenario, the available items should be filtered, otherwise the user will see them and be able to rent them
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Rentable>> GetRentableItems()
        {
            var cars = await _carAssetRepository.GetAll();
            return cars.Select(car => new CarRentable(car.Id, car.Description, car.AssetId, AssetState.Available.ToString())
            {
                Model = car.Model,
                Year = car.Year,
            }).Cast<Rentable>().ToList();
        }

        /// <summary>
        ///     Retrieve a rentable item by its identifier
        /// </summary>
        /// <param name="id">Unique identifier</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Rentable> GetItemById(int id)
        {
            var item = await _carAssetRepository.GetById(id);

            if (item == null)
                throw new ArgumentException("Invalid car identifier");

            return new CarRentable(item.Id, item.Description, item.AssetId, AssetState.Available.ToString())
            {
                Model = item.Model,
                Year = item.Year,
            };
        }

        public async Task<RentRequestResult> RentAnItem(Rentable? rentableItem)
        {
            if (rentableItem is not CarRentable carRentable)
                throw new ArgumentException("Invalid rentable item");

            // More check should be done here to ensure the item is still available for rent, check if the update succeeded
            await _carAssetRepository.Rent(carRentable.Id);
            return new RentRequestResult
            {
                RentRequestId = 42,
                RentableAssetId = carRentable.Id,
            };
        }

        public async Task<ReturnRequestResult> ReturnAnItem(Rentable rentableItem)
        {
            if (rentableItem is not CabinRentable cabinRentable)
                throw new ArgumentException("Invalid rentable item");

            // More check should be done here before or after rental allowed period, check if the update succeeded
            await _carAssetRepository.Rent(cabinRentable.Id);
            return new ReturnRequestResult
            {
                ReturnRequestId = 12,
                RentableAssetId = cabinRentable.Id,
            };
        }
    }
}