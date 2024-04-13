using VacationHire.Data.Data;

namespace VacationHire.Repository
{
    /// <summary>
    ///     Base repository class
    /// Not much use for now, but can be used to add common repository methods in the future: methods with timeouts logic, retries, etc.
    /// </summary>
    /// <param name="dbContext"></param>
    public class RepositoryBase(VacationHireDbContext dbContext)
    {
        protected readonly VacationHireDbContext Context = dbContext;
    }
}