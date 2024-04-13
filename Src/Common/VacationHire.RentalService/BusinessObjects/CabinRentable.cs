namespace VacationHire.RentalService.BusinessObjects
{
    /// <summary>
    ///     Extend the class with specific properties for a cabin rentable item
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <param name="categoryId"></param>
    /// <param name="state"></param>
    public class CabinRentable(int id, string description, int categoryId, string state) : Rentable(id, description, categoryId, state)
    {
        public string Address { get; set; }

        public short? NoOfRooms { get; set; }
    }
}