namespace VacationHire.RentalService.BusinessObjects
{
    /// <summary>
    ///     Extend the class with specific properties for a car rentable item
    ///     - such properties should be retrieved from the data persistence layer
    ///     - provide additional information to the user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <param name="assetId"></param>
    /// <param name="state"></param>
    public class CarRentable(int id, string description, int assetId, string state) : Rentable(id, description, assetId, state)
    {
        public string Model { get; set; }

        public short? Year { get; set; }
    }
}