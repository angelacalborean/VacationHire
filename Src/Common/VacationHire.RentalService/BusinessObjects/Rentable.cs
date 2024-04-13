namespace VacationHire.RentalService.BusinessObjects
{
    /// <summary>
    ///     Assume some common properties exist for all rentable items
    /// </summary>
    /// <param name="id">Each item should be uniquely identifies</param>
    /// <param name="description">Description</param>
    /// <param name="assetId">Each item belongs to an asset group</param>
    /// <param name="state">State of the item  <see cref="AssetState"/></param>
    public class Rentable(int id, string description, int assetId, string state)
    {
        /// <summary>
        ///    The unique identifier for the asset that can be rented out
        /// </summary>
        public int Id { get; set; } = id;

        /// <summary>
        ///     Additional description for the rentable item
        /// </summary>
        public string Description { get; set; } = description;

        /// <summary>
        ///     The identifier for the rentable item asset
        /// </summary>
        public int AssetId { get; set; } = assetId;

        /// <summary>
        ///    The current state of the rentable item
        /// </summary>
        public string State { get; set; } = state;
    }
}