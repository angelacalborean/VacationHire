using Microsoft.Extensions.Logging;
using Moq;
using VacationHire.Data.Data;
using VacationHire.Data.Models;
using VacationHire.Repository;
using VacationHire.Repository.Interfaces;
using VacationHire.UnitTests.Helper;
using VacationHire.UnitTests.Mocker;
using Xunit;

namespace VacationHire.UnitTests
{
    /// <summary>
    ///     In order to add unit test for the repository classes there are multiple ways to do it:
    ///     - One way is to use an in-memory database
    ///     - Use a local database - this might slow down the tests
    ///     - Another way is to use a mocking framework like Moq
    /// </summary>
    public class CarAssetRepositoryTests
    {
        public static ICarAssetRepository GetCarAssetRepository(List<CarAsset> carAssets, Mock<ILogger<CarAssetRepository>>? logger = null)
        {
            var contextMock = DbContextMock.GetMock<CarAsset, VacationHireDbContext>(carAssets, x => x.CarAssets);

            logger ??= new Mock<ILogger<CarAssetRepository>>();
            return new CarAssetRepository(contextMock, logger.Object);
        }


        [Fact]
        public async Task GetCarAssets_NoData_ReturnsEmptyList()
        {
            var repository = GetCarAssetRepository([]);
            var result = await repository.GetAll();
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCarAssets_DataExists_ReturnsCorrectValues()
        {
            var carAssets = TestDataGenerator.GenerateCarAssets();
            var repository = GetCarAssetRepository(carAssets);

            var result = await repository.GetAll();
            var collection = result.ToList();
            Assert.NotEmpty(collection);
            Assert.Equal(carAssets.Count, collection.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public async Task GetById_NoData_ReturnsEmptyList(int carAssetId)
        {
            var repository = GetCarAssetRepository([]);
            var result = await repository.GetById(carAssetId);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetById_DataExists_ReturnsCorrectValues()
        {
            var carAssets = TestDataGenerator.GenerateCarAssets();
            var repository = GetCarAssetRepository(carAssets);

            var result = await repository.GetById(carAssets[0].Id);
            Assert.NotNull(result);
            Assert.Equal(result.Description, carAssets[0].Description);

            // maybe add fluent assertions to check on all properties
        }

    }
}