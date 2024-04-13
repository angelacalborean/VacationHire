namespace VacationHire.RentalApprovalFunction.Workflow
{
    public class RentalApprovalProcess : IRentalRequestState
    {
        public IRentalRequestState RentalRequest { get; set; }

        public RentalApprovalProcess()
        {
            RentalRequest = new PendingState();
        }

        public void Approve()
        {
            RentalRequest.Approve();
        }

        public void Reject()
        {
            RentalRequest.Reject();
        }
    }
}
