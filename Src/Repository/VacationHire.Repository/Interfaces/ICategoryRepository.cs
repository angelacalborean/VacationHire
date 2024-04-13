using VacationHire.Data.Models;

namespace VacationHire.Repository.Interfaces
{
    /// <summary>
    ///     Represents the repository for the Category management
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        ///     List all categories
        /// </summary>
        /// <returns>Returns all available categories</returns>
        Task<IEnumerable<Category>> GetCategories();

        /// <summary>
        ///     Get an category by its unique identifier
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <returns>Category that matches the id, null otherwise</returns>
        Task<Category?> GetById(int id);

        /// <summary>
        ///    Add a new category to the database
        /// </summary>
        /// <param name="category">Category to be added</param>
        Task<Category> AddCategory(Category category);
    }
}