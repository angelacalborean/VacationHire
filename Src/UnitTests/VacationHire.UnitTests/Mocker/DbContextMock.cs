using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace VacationHire.UnitTests.Mocker
{
    /// <summary>
    ///     Mock DbContext entities
    /// </summary>
    public class DbContextMock
    {
        public static TContext GetMock<TData, TContext>(List<TData> sourceList, Expression<Func<TContext, DbSet<TData>>> dbSetSelectionExpression) where TData : class where TContext : DbContext
        {
            var dbContext = new Mock<TContext>();
            var mock = sourceList.AsQueryable().BuildMockDbSet();
            dbContext.Setup(dbSetSelectionExpression).Returns(mock.Object);
            return dbContext.Object;
        }
    }
}