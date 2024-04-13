namespace VacationHire.RentalApprovalFunction.Workflow
{
    /// <summary>
    ///     Assume the outcome will be one of the following: rental approved or rental rejected.
    /// </summary>
    public interface IRentalRequestState
    {
        void Approve();

        void Reject();
    }

}