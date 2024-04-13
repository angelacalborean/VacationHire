using VacationHire.Data.Enum;
using VacationHire.RentalService.Interfaces;
using VacationHire.Repository.Interfaces;

namespace VacationHire.RentalService.Implementations
{
    /// <summary>
    ///     Extensions points for creating rentable services for new items to be rented out
    /// If you want to start renting books:
    ///     - assume you have the persistence layer in place
    ///     - add a new implementation of IRentableService for books
    ///     - based on the new asset type, return the concrete implementation of the IRentableService interface
    /// </summary>
    public class RentableServiceFactory : IRentableServiceFactory
    {
        private readonly ICabinAssetRepository _cabinAssetRepository;
        private readonly ICarAssetRepository _carAssetRepository;

        public RentableServiceFactory(ICabinAssetRepository cabinAssetRepository, ICarAssetRepository carAssetRepository)
        {
            _cabinAssetRepository = cabinAssetRepository;
            _carAssetRepository = carAssetRepository;
        }

        public IRentableService CreateRentableService(AssetType assetType)
        {
            switch (assetType)
            {
                case AssetType.Car:
                    return new CarAssetsRentableService(_carAssetRepository);

                case AssetType.Cabin:
                    return new CabinAssetsRentableService(_cabinAssetRepository);

                // Assume you will start renting books
                //case AssetType.Book:
                //    return new BookAssetsRentableService(_bookAssetRepository);

                default:
                    throw new ArgumentOutOfRangeException(nameof(assetType), assetType, null);
            }
        }
    }
}