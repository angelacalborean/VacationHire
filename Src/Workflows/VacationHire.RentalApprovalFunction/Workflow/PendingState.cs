namespace VacationHire.RentalApprovalFunction.Workflow
{
    public class PendingState : IRentalRequestState
    {
        public void Approve()
        {
            // waiting for approval
        }

        public void Reject()
        {
            // waiting for rejection
        }
    }
}