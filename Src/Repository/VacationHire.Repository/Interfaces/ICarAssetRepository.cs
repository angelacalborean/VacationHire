using VacationHire.Data.Models;

namespace VacationHire.Repository.Interfaces
{
    /// <summary>
    /// Points for improvement:
    ///     - This repository works with the CarAsset entity that is mapped to a specific table in the database
    ///     - The methods in this repository are used in different parts of the application that are decoupled from each other
    ///     - Improvements:
    ///             - the repository could be split into smaller parts that perform specific operations
    ///             - display the available items: read from replica, called from a "read only repository" 
    ///             - only admin are allowed to add items: extract admin operations in a separate repository and grant only to that one the INSERT permission
    /// </summary>
    public interface ICarAssetRepository
    {
        /// <summary>
        ///     List all cars
        /// </summary>
        /// <returns>Returns all available cars</returns>
        Task<IList<CarAsset>> GetAll();

        /// <summary>
        ///     Get a car by its unique identifier
        /// </summary>
        /// <param name="id">Car identifier</param>
        /// <returns>Car that matches the id, null otherwise</returns>
        Task<CarAsset?> GetById(int id);

        /// <summary>
        ///     List cars from a gives asset
        /// </summary>
        /// <param name="assetId">Asset identifier</param>
        /// <returns>Cars that matches the asset </returns>
        Task<IList<CarAsset>> GetByAssetId(int assetId);

        /// <summary>
        ///    Add a new car to the database
        /// </summary>
        /// <param name="carAsset">Item to be added</param>
        /// <returns>Newly added item</returns>
        Task<CarAsset> Add(CarAsset carAsset);

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