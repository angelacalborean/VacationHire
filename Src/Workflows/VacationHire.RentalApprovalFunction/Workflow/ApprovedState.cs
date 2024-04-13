namespace VacationHire.RentalApprovalFunction.Workflow
{
    public class ApprovedState : IRentalRequestState
    {
        public void Approve()
        {
            // Rental request is approved
        }

        public void Reject()
        {
            // Cannot reject after approval
        }
    }
}