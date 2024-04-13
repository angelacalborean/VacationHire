using VacationHire.RentalService.BusinessObjects;
using VacationHire.RentalService.Responses;

namespace VacationHire.RentalService.Interfaces
{
    /// <summary>
    ///     Interface for handle rental operations
    /// </summary>
    public interface IRentableService
    {
        /// <summary>
        ///     Retrieve all rentable items
        /// </summary>
        /// <returns></returns>
        Task<IList<Rentable>> GetRentableItems();


        /// <summary>
        ///     Retrieves a rentable item by its unique identifier
        /// </summary>
        /// <param name="id">Unique identifier</param>
        /// <returns></returns>
        Task<Rentable> GetItemById(int id);

        /// <summary>
        ///     When an item is rented out, it is added to the rental approval workflow
        /// - I run on the assumption that a rental request is generated, and needs to be approved before the item is rented out
        /// - When this method is called, the user gets informed tha the rental request is pending approval
        /// </summary>
        /// <param name="rentableItem">Item to be rent</param>
        /// <returns>An acknowledgment response to the user</returns>
        Task<RentRequestResult> RentAnItem(Rentable? rentableItem);

        /// <summary>
        ///     When an item is returned, it is added to the return check workflow 
        /// - When this method is called, the user gets informed tha the rental request is pending approval
        /// - During the return check workflow the return check, the item is checked for damages and the user gets the accurate invoice
        /// </summary>
        /// <param name="rentableItem">Item that was rented and now it is returned</param>
        /// <returns>An acknowledgment response to the user</returns>
        Task<ReturnRequestResult> ReturnAnItem(Rentable rentableItem);
    }
}