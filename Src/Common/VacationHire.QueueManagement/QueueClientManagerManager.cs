using System.Text;
using Azure.Identity;
using Azure.Storage.Queues;
using VacationHire.QueueManagement.Interfaces;

namespace VacationHire.QueueManagement
{
    /// <summary>
    ///     Improvements:
    ///         - this class has a hardcoded queue storage account name and queue name
    ///         - these values should be read from the configuration (have an IApplicationSettings interface that reads the configuration
    /// and be injected here)
    ///         - Further improvements: have a QueueManagerFactory to create the QueueManager instance if different queues are used
    /// </summary>
    public class QueueClientManagerManager : IQueueClientManager
    {
        private QueueClient _queueClient;


        /// <summary>
        ///     Inserts a message into an existing queue.
        /// </summary>
        /// <param name="message">A message can be either a string (in UTF-8 format) or a byte array. Max 64 KB.</param>
        /// <returns></returns>
        public async Task<bool> QueueMessage(string message)
        {
            if (_queueClient == null)
                await Initialize();

            // If you want to test it, either add a valid queue storage account name and ensure that your user has permissions to access it
            // or comment the next line

            await _queueClient.SendMessageAsync(ToBase64Encode(message));
            return true;
        }

        /// <summary>
        ///     This method needs to be called before accessing other methods
        /// </summary>
        /// <returns></returns>
        private async Task Initialize()
        {
            _queueClient = await GetQueueClient("TestQueue");
        }

        /// <summary>
        ///     This is a demo implementation of how to get a queue client: the storage account name is hard coded;
        ///   It should be read from Application configuration
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        private static async Task<QueueClient> GetQueueClient(string queueName)
        {
            var queueStorageAccountUri = new Uri("https://queueStorageAccountName.queue.core.windows.net");
            var queueServiceClient = new QueueServiceClient(queueStorageAccountUri, GetAzureCredentials());

            var queueClient = queueServiceClient.GetQueueClient(queueName);
            if (await queueClient.ExistsAsync())
                return queueClient;

            await queueClient.CreateIfNotExistsAsync();
            return queueClient;
        }

        /// <summary>
        ///    Keep ManagedIdentityCredentials as a valid authorization credential
        /// </summary>
        /// <returns></returns>
        private static DefaultAzureCredential GetAzureCredentials()
        {
            return new DefaultAzureCredential(new DefaultAzureCredentialOptions
            {
                ExcludeAzureCliCredential = true,
#if !DEBUG
                ExcludeVisualStudioCodeCredential = true,
                ExcludeVisualStudioCredential = true,
#endif
                ExcludeEnvironmentCredential = true,
                ExcludeInteractiveBrowserCredential = true,
                ExcludeSharedTokenCacheCredential = true
            });
        }

        /// <summary>
        ///     Plain text to base64: improvements: move it in a common class with extension methods
        /// </summary>
        /// <param name="plainText">String to Encode</param>
        /// <returns></returns>
        private static string ToBase64Encode(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}