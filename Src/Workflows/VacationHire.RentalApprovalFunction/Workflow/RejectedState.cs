namespace VacationHire.RentalApprovalFunction.Workflow
{
    public class RejectedState : IRentalRequestState
    {
        public void Approve()
        {
            // Cannot approve after rejection
        }

        public void Reject()
        {
            // Rental request is rejected
        }
    }
}