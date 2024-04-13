namespace VacationHire.AdministrativeApi.Requests
{
    /// <summary>
    ///     Read from body when a request is issued to create a new asset
    /// </summary>
    public class CreateAssetRequest
    {
        /// <summary>
        ///     The name of the asset
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        ///     Additional description for the asset
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The identifier for the category, under which the item is listed
        /// </summary>
        public int CategoryId { get; set; }
    }
}