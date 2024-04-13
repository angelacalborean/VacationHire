namespace VacationHire.AdministrativeApi.Requests
{
    /// <summary>
    ///     Read from body when a request is issued to create a new category
    /// </summary>
    public class CreateCategoryRequest
    {
        /// <summary>
        ///     The name of the new category
        /// </summary>
        public required string CategoryName { get; set; }

        /// <summary>
        ///     Additional description for the new category
        /// </summary>
        public required string Description { get; set; }
    }
}