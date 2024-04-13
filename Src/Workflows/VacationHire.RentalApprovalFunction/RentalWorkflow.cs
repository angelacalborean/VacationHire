using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace VacationHire.RentalApprovalFunction
{
    /// <summary>
    ///     Dummy implementation of a rental approval workflow.
    /// </summary>
    public class RentalWorkflow
    {
        [FunctionName("RentalWorkflow")]
        public void Run([QueueTrigger("angelatest")] string myQueueItem, ILogger log)
        {
            // A new message was added to the queue, this function will be triggered

            // The item is of RentAcknowledgeResponse type, gets deserialized and processed

            // The rental request is approved or rejected: use SendGrid to send email to someone that can approve it

            // After the process is completed, notify the user of the outcome
        }
    }
}