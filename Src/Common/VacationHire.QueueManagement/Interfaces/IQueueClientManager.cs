namespace VacationHire.QueueManagement.Interfaces
{
    /// <summary>
    ///     Interface for handling queue management operations
    /// </summary>
    public interface IQueueClientManager
    {
        /// <summary>
        ///     Insert a message into a queue
        /// </summary>
        /// <param name="message">Message to be inserted</param>
        /// <returns></returns>
        Task<bool> QueueMessage(string message);

        ///// <summary>
        /////     Gets the next message in a queue.
        ///// </summary>
        ///// <returns></returns>
        //Task<string> DequeueMessage();

        ///// <summary>
        /////     Returns the estimate number of messages in a queue.
        ///// </summary>
        ///// <returns></returns>
        //Task<int> GetQueueLength();
    }
}