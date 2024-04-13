namespace VacationHire.Repository.Interfaces
{
    /// <summary>
    ///     Interface that could be used to handle different types of assets if they have common properties and can use that same table for storage
    /// </summary>
    public interface IGenericAsset
    {
        /// <summary>
        ///    The unique identifier for the asset
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     The name of the asset: mapped to nvarchar[N] column in the database
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        ///     Additional description for the asset: mapped to nvarchar[N] column in the database
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The identifier for the category, under which the item is listed (linked to the Category table using a foreign key)
        /// </summary>
        public int CategoryId { get; set; }
    }

    /// <summary>
    ///     Not used in the current implementation due to the following:
    ///         - I consider that each new other type of rental assets will have a different set of properties
    ///         - There are no common properties between the different types of assets
    ///         - If we run on the assumption that there are common properties, and they comply to a fixed structure, then a generic interface could be used
    /// and genetic classes could be created to handle the different types of assets from which all assets objects derive 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericAssetRepository<T> where T : IGenericAsset
    {
        /// <summary>
        ///     List all cars
        /// </summary>
        /// <returns>Returns all available cars</returns>
        Task<IEnumerable<T>> GetCarAssets();

        /// <summary>
        ///     Get a car by its unique identifier
        /// </summary>
        /// <param name="id">Car identifier</param>
        /// <returns>Car that matches the id, null otherwise</returns>
        Task<T?> GetById(int id);

        /// <summary>
        ///     List cars from a gives asset
        /// </summary>
        /// <param name="assetId">Asset identifier</param>
        /// <returns>Cars that matches the asset </returns>
        Task<IEnumerable<T>> GetByAssetId(int assetId);

        /// <summary>
        ///    Add a new car to the database
        /// </summary>
        /// <param name="carAsset">Item to be added</param>
        /// <returns>Newly added item</returns>
        Task<T> Add(T carAsset);
    }
}