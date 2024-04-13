namespace VacationHire.Data.Enum
{
    /// <summary>
    ///     Different states an asset can be in
    /// </summary>
    public enum AssetState
    {
        Error = -1,

        Available = 0,

        RentalPending = 1,

        Rented = 2,

        ReturnPending = 3,

        WaitingForPayment = 4
    }
}