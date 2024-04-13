namespace VacationHire.AdministrativeApi.Requests
{
    /// <summary>
    ///     Read from body when a request is issued to create a new car asset: state will be set to Available
    /// </summary>
    public class CreateCarAssetRequest
    {
        public int AssetId { get; set; }


        public string Description { get; set; }


        public string Mark { get; set; }


        public string Model { get; set; }


        public short? Year { get; set; }


        public int? Mileage { get; set; }
    }
}