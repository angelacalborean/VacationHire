using VacationHire.Data.Enum;

namespace VacationHire.RentalService.Interfaces
{
    /// <summary>
    ///     Retrieve a concrete implementation of the IRentableService interface
    /// </summary>
    public interface IRentableServiceFactory
    {
        /// <summary>
        ///     Based on the asset type, return the concrete implementation of the IRentableService interface
        /// </summary>
        /// <param name="assetType">Asset type</param>
        /// <returns></returns>
        IRentableService CreateRentableService(AssetType assetType);
    }
}