namespace VacationHire.Data.Models
{
    /// <summary>
    ///     Represents a category for different items: Vehicles, Cabins, Drones, Costumes, Bikes...  
    /// </summary>
    public class Category
    {
        /// <summary>
        ///    The unique identifier for the category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The name of the category: mapped to nvarchar[N] column in the database
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        ///     Additional description for the category: mapped to nvarchar[N] column in the database
        /// </summary>
        public string Description { get; set; }

        public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}