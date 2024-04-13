using VacationHire.Data.Enum;
using VacationHire.Data.Models;

namespace VacationHire.UnitTests.Helper
{
    public static class TestDataGenerator
    {
        /// <summary>
        ///     To work easier with test data:
        /// - use Fixture
        /// - use a common class for tests generated data
        /// </summary>
        /// <returns></returns>
        public static List<CarAsset> GenerateCarAssets()
        {
            return
            [
                new CarAsset { State = (int)AssetState.Available, Description = "Very nice Ford", Mark = "Ford", Model = "Focus", Year = 2024, Mileage = 0, AssetId = 1 },
                new CarAsset { State = (int)AssetState.Available, Description = "Very nice Skoda", Mark = "Skoda", Model = "Kamik", Year = 2024, Mileage = 0, AssetId = 1 },
                new CarAsset { State = (int)AssetState.Available, Description = "Very big Jeep", Mark = "Jeep", Model = "Wrangler", Year = 2024, Mileage = 0, AssetId = 3 }
            ];
        }
    }
}