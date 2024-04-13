namespace VacationHire.AdministrativeApi.Responses
{
    public class AssetResponse
    {
        /// <summary>
        ///    The unique identifier for the asset
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The name of the asset
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        ///     Additional description for the asset
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The category under which the asset is listed
        /// </summary>
        public string CategoryName { get; set; }
    }
}