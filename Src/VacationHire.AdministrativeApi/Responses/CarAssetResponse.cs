namespace VacationHire.AdministrativeApi.Responses
{
    public class CarAssetResponse
    {
        public int Id { get; set; }

        public int AssetId { get; set; }

        public int AssetName { get; set; }

        public Enum AssetState { get; set; }

        public string Description { get; set; }

        public string Mark { get; set; }


        public string Model { get; set; }


        public short? Year { get; set; }


        public int? Mileage { get; set; }
    }
}