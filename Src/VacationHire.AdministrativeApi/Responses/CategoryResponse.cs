namespace VacationHire.AdministrativeApi.Responses
{
    /// <summary>
    ///     Mapped to a category entry from the database
    /// </summary>
    public class CategoryResponse
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}