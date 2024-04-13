using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VacationHire.AdministrativeApi.Controllers;
using VacationHire.AdministrativeApi.Mapper;
using VacationHire.AdministrativeApi.Responses;
using VacationHire.Data.Models;
using VacationHire.Repository.Interfaces;
using VacationHire.UnitTests.Helper;
using VacationHire.UnitTests.Mocker;
using Xunit;

namespace VacationHire.UnitTests
{
    /// <summary>
    ///     Only a method is tested here
    /// In a real project I would have split the unit tests in multiple projects, but for this example I will keep them in the same project. 
    /// </summary>
    public class CarAssetsControllerTests
    {
        [Fact]
        public async Task GetCarAssets_NoData_ReturnsNotFound()
        {
            var repository = CarAssetRepositoryMocker.GetCarAssetRepository((List<CarAsset>)null);
            var controller = GetCarAssetController(repository: repository);
            var response = (ObjectResult)await controller.ListAllCarAssets();

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
            ApiResponseAssertionHelper.CheckApiResponseContentType(response, "No car assets found.");

            repository.Verify(m => m.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetCarAssets_ExistsData_ReturnsCorrectResponse()
        {
            var carAssets = TestDataGenerator.GenerateCarAssets();
            var repository = CarAssetRepositoryMocker.GetCarAssetRepository(carAssets);

            var controller = GetCarAssetController(repository: repository);
            var response = (ObjectResult)await controller.ListAllCarAssets();

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            var carAssetsResponse = response.Value as List<CarAssetResponse>;
            Assert.NotNull(carAssetsResponse);
            Assert.Equal(carAssets.Count, carAssetsResponse.Count);

            repository.Verify(m => m.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetCarAssets_OnException_ReturnsInternalServerError()
        {
            var repository = CarAssetRepositoryMocker.GetCarAssetRepository_WithException();

            var controller = GetCarAssetController(repository: repository);
            var response = (ObjectResult)await controller.ListAllCarAssets();

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, response.StatusCode);

            ApiResponseAssertionHelper.CheckApiResponseContentType(response, "GetAll exception");

            repository.Verify(m => m.GetAll(), Times.Once);
        }

        private static CarAssetController GetCarAssetController(Mock<ICarAssetRepository> repository)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            var mapper = mappingConfig.CreateMapper();

            var logger = new Mock<ILogger<CarAssetController>>().Object;

            var controller = new CarAssetController(repository.Object, mapper, logger);
            return controller;
        }
    }
}