using VacationHire.Data.Models;

namespace VacationHire.Repository.Interfaces
{
    /// <summary>
    ///     Will be added when the new feature of renting cabins is implemented
    /// Same improvements as in the CarAssetRepository
    /// </summary>
    public interface ICabinAssetRepository
    {
        /// <summary>
        ///     List all cars
        /// </summary>
        /// <returns>Returns all available cabins</returns>
        Task<IList<CabinAsset>> GetAll();

        /// <summary>
        ///     Get a cabin by its unique identifier
        /// </summary>
        /// <param name="id">Cabin identifier</param>
        /// <returns>Cabin that matches the id, null otherwise</returns>
        Task<CabinAsset?> GetById(int id);

        /// <summary>
        ///     Changes the status of the item: RentalPending
        /// </summary>
        /// <param name="id">Item identifier</param>
        /// <returns></returns>
        Task Rent(int id);

        /// <summary>
        ///     Changes the status of the item: ReturnPending
        /// </summary>
        /// <param name="id">Item identifier</param>
        /// <returns></returns>
        Task Return(int id);
    }
}