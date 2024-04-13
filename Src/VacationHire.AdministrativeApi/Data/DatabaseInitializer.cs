using Microsoft.EntityFrameworkCore;
using VacationHire.Data.Data;
using VacationHire.Data.Enum;
using VacationHire.Data.Models;

namespace VacationHire.AdministrativeApi.Data
{
    /// <summary>
    ///     At startup the database is populated with some dummy data.
    ///
    /// CategoryName     AssetName      Id  Description         Mark    Model       Year
    /// Cars            City cars       1	Very nice Ford      Ford    Focus	    2024
    /// Cars            City cars	    2	Very nice Skoda     Skoda   Kamik	    2024
    /// Cars            Off road cars   3	Very big Jeep       Jeep    Wrangler	2024
    ///
    /// CategoryName	AssetName	    Id	Description	    Address	    NoOfRooms
    /// Cabins          Small apartment	1	Apartment 1	    Address 1	    2
    /// Cabins          Small apartment	2	Apartment 2	    Address 2	    1
    ////Cabins          Big apartment	3	Cabin 1	        Address 2	    3
    /// </summary>
    public static class DatabaseInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new VacationHireDbContext(serviceProvider.GetRequiredService<DbContextOptions<VacationHireDbContext>>());

            if (context.Categories.Any())
                return; // Database has been already populated

            // Generate some dummy categories
            var carCategory = context.Categories.Add(new Category { CategoryName = "Cars", Description = "Cars to be rented" });
            var cabinCategory = context.Categories.Add(new Category { CategoryName = "Cabins", Description = "Cabins to be rented" });

            // Generate some dummy assets
            var smallCarsAsset = context.Assets.Add(new Asset { AssetName = "City cars", Description = "Small cars", CategoryId = 1, Category = carCategory.Entity });
            var terrainCarsAsset = context.Assets.Add(new Asset { AssetName = "Off road cars", Description = "Cars for off road adventures", CategoryId = 1, Category = carCategory.Entity });
            var smallApartmentsAsset = context.Assets.Add(new Asset { AssetName = "Small apartment", Description = "Small apartment", CategoryId = 2, Category = cabinCategory.Entity });
            var bigApartmentsAsset = context.Assets.Add(new Asset { AssetName = "Big apartment", Description = "Big apartment", CategoryId = 2, Category = cabinCategory.Entity });

            // Add car assets
            context.CarAssets.AddRange(
                new CarAsset { State = (int)AssetState.Available, Description = "Very nice Ford", Mark = "Ford", Model = "Focus", Year = 2024, Mileage = 0, Asset = smallCarsAsset.Entity },
                new CarAsset { State = (int)AssetState.Available, Description = "Very nice Skoda", Mark = "Skoda", Model = "Kamik", Year = 2024, Mileage = 0, Asset = smallCarsAsset.Entity },
                new CarAsset { State = (int)AssetState.Available, Description = "Very big Jeep", Mark = "Jeep", Model = "Wrangler", Year = 2024, Mileage = 0, Asset = terrainCarsAsset.Entity }
            );

            // Add cabin assets
            context.CabinAssets.AddRange(new CabinAsset { State = (int)AssetState.Available, Description = "Apartment 1", Address = "Address 1", NoOfRooms = 2, NoOfBathrooms = 1, Asset = smallApartmentsAsset.Entity },
                new CabinAsset { State = (int)AssetState.Available, Description = "Apartment 2", Address = "Address 2", NoOfRooms = 1, NoOfBathrooms = 1, Asset = smallApartmentsAsset.Entity },
                new CabinAsset { State = (int)AssetState.Available, Description = "Cabin 1", Address = "Address 2", NoOfRooms = 3, NoOfBathrooms = 2, Asset = bigApartmentsAsset.Entity });

            context.SaveChanges();
        }
    }
}