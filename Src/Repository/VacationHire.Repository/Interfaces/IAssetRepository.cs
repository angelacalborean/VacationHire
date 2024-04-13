using VacationHire.Data.Models;

namespace VacationHire.Repository.Interfaces
{
    /// <summary>
    ///     Represents the repository for the Asset management
    /// </summary>
    public interface IAssetRepository
    {
        /// <summary>
        ///     List all available assets
        /// </summary>
        /// <returns>Returns all available assets</returns>
        Task<IEnumerable<Asset>> GetAssets();

        /// <summary>
        ///     List all available assets from a given category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns></returns>
        Task<IEnumerable<Asset>> GetAssetsByCategory(int categoryId);

        /// <summary>
        ///     Get an asset by its unique identifier
        /// </summary>
        /// <param name="id">Asset identifier</param>
        /// <returns>Asset that matches the id, null otherwise</returns>
        Task<Asset?> GetById(int id);

        /// <summary>
        ///    Add a new asset to the repository
        /// </summary>
        /// <param name="asset">Asset to be added</param>
        /// <returns>True, if operation succeeded</returns>
        Task<Asset> AddAsset(Asset asset);
    }
}