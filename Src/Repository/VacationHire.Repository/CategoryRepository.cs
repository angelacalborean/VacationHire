using Microsoft.EntityFrameworkCore;
using VacationHire.Data.Data;
using VacationHire.Data.Models;
using VacationHire.Repository.Interfaces;

namespace VacationHire.Repository
{
    public class CategoryRepository(VacationHireDbContext dbContext) : RepositoryBase(dbContext), ICategoryRepository
    {
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await Context.Categories.ToListAsync();
        }

        public Task<Category?> GetById(int id)
        {
            return Context.Categories.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Category> AddCategory(Category category)
        {
            try
            {
                var result = await Context.Categories.AddAsync(category);
                await Context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}