using AzureLibrary.AzureServiceBus.Interface;
using AzureLibrary.AzureServiceBus.Model;
using AzureLibrary.AzureServiceBus.ServiceBus;
using System.Threading;

namespace check
{
    public class program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Publisher!");
            string connectionString = "Endpoint=sb://servicequeue.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=EkaUVeT+KxhKvKQ/IeqKC28k19YQaUnZh+ASbDH3m+Y=";

            QueueProvider queueProvider = new QueueProvider(connectionString);
            string queueName = "checkpath";

            QueueOptions queueOptions = new QueueOptions
            {
                LockDuration = TimeSpan.FromSeconds(15),
                MaxDeliveryCount = 2
            };

            var publisher = queueProvider.GetPublisher(queueName, queueOptions);

            PublishMessagesWithCallBack(publisher);
            Console.WriteLine();
        }

        public static void PublishMessagesWithCallBack(IMessagePublisher publisher)
        {
            var headers = new Dictionary<string, object>();

            headers.Add("x-sampleheader1", "Sample1");
            headers.Add("x-sampleheader2", "Sample2");


            IList<IMessage> messages = new List<IMessage>();

            for (int i = 1; i <= 2; i++)
            {
                Message message = new Message()
                {
                    Body = string.Format("Sample Massage {0}", i),
                    MessageId = Guid.NewGuid().ToString(),
                    CorrelationId = Guid.NewGuid().ToString(),
                    Headers = headers
                };
                //messages.Add(message);
                publisher.PublishAsync(message).GetAwaiter().GetResult();
            }

            //publisher.PublishAsync(messages).GetAwaiter().GetResult();
        }
    }
}