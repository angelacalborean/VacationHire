using VacationHire.RentalService.BusinessObjects;

namespace VacationHire.RentalsApi.Responses
{
    public class CarAssetResponse
    {
        public CarAssetResponse() { }

        public CarAssetResponse(Rentable car)
        {
            Id = car.Id;
            Description = car.Description;
        }

        public int Id { get; set; }

        public string Description { get; set; }
    }
}