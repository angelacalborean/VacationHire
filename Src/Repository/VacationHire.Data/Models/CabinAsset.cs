namespace VacationHire.Data.Models
{
    /// <summary>
    ///     Cabin assets have their own set of properties: a location, number of rooms, number of bathrooms
    /// </summary>
    public class CabinAsset
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
        public string Address { get; set; }

        /// <summary>
        ///     Mapped to smallint column in the database
        /// </summary>
        public short? NoOfRooms { get; set; }

        /// <summary>
        ///     Mapped to smallint column in the database
        /// </summary>
        public short? NoOfBathrooms { get; set; }

        public virtual Asset Asset { get; set; }
    }
}
