namespace VacationHire.Data.Models
{
    /// <summary>
    ///     Each type of asset has itw own set of properties:
    ///         - a vehicle will have a mark, model, year, and mileage
    ///         - a cabin will have a location, number of rooms, number of bathrooms
    /// All these specific properties will be stored in the corresponding table: each time a new type of asset is added, a new table will be created
    /// </summary>
    public class CarAsset
    {
        /// <summary>
        ///    The unique identifier for the car asset
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///    The asset under which the car is listed (linked to the Asset table using a foreign key)
        /// </summary>
        public int AssetId { get; set; }

        /// <summary>
        ///    The state of the asset in the rental process (smallint column in the database; mapped to the AssetState enum in repository methods)
        /// </summary>
        public short State { get; set; }

        /// <summary>
        ///     Additional description for the asset: mapped to nvarchar[N] column in the database
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The mark of the vehicle: mapped to nvarchar[N] column in the database
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        ///     The model of the vehicle: mapped to nvarchar[N] column in the database
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        ///     The year the vehicle was manufactured: mapped to smallint column in the database
        /// </summary>
        public short? Year { get; set; }

        /// <summary>
        ///     The mileage of the vehicle: mapped to int column in the database
        /// </summary>
        public int? Mileage { get; set; }

        public virtual Asset Asset { get; set; }
    }
}