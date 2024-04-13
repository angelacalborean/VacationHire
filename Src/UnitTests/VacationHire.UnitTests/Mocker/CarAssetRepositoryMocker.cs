using Moq;
using VacationHire.Data.Models;
using VacationHire.Repository.Interfaces;

namespace VacationHire.UnitTests.Mocker
{
    internal static class CarAssetRepositoryMocker
    {
        public static Mock<ICarAssetRepository> GetCarAssetRepository(IList<CarAsset>? carAssets = null)
        {
            var repository = new Mock<ICarAssetRepository>();

            repository.Setup(m => m.GetAll()).ReturnsAsync(carAssets);

            repository.Setup(m => m.GetById(It.IsAny<int>()))
                .ReturnsAsync((int id) => carAssets.FirstOrDefault(c => c.Id == id));

            repository.Setup(m => m.GetByAssetId(It.IsAny<int>()))
                .ReturnsAsync((int id) => carAssets.Where(c => c.AssetId == id).ToList());

            repository.Setup(m => m.Add(It.IsAny<CarAsset>()))
                .ReturnsAsync((CarAsset carAsset) => carAsset);

            return repository;
        }

        public static Mock<ICarAssetRepository> GetCarAssetRepository_WithException()
        {
            var repository = new Mock<ICarAssetRepository>();

            repository.Setup(m => m.GetAll()).ThrowsAsync(new Exception("GetAll exception"));

            repository.Setup(m => m.GetById(It.IsAny<int>())).ThrowsAsync(new Exception("GetById exception"));

            repository.Setup(m => m.GetByAssetId(It.IsAny<int>())).ThrowsAsync(new Exception("GetByAssetId exception"));

            repository.Setup(m => m.Add(It.IsAny<CarAsset>())).ThrowsAsync(new Exception("Add exception"));

            return repository;
        }
    }
}
