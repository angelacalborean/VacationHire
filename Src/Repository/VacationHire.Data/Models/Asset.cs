namespace VacationHire.Data.Models
{
    /// <summary>
    ///     Represents an asset that can be rented out
    /// </summary>
    public class Asset
    {
        /// <summary>
        ///    The unique identifier for the asset
        /// </summary>
        public int Id { get; set; }

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

        public virtual ICollection<CabinAsset> CabinAssets { get; set; } = new List<CabinAsset>();

        public virtual ICollection<CarAsset> CarAssets { get; set; } = new List<CarAsset>();

        public virtual Category Category { get; set; }
    }
}