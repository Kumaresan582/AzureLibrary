/*using Azure.Messaging.ServiceBus;

namespace ReceiveProject
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            ServiceBusClient client;

            ServiceBusProcessor processor;

            async Task MessageHandler(ProcessMessageEventArgs args)
            {
                string body = args.Message.Body.ToString();
                Console.WriteLine($"Received: {body}");

                await args.CompleteMessageAsync(args.Message);
            }

            Task ErrorHandler(ProcessErrorEventArgs args)
            {
                Console.WriteLine(args.Exception.ToString());
                return Task.CompletedTask;
            }

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient("Endpoint=sb://servicequeue.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=EkaUVeT+KxhKvKQ/IeqKC28k19YQaUnZh+ASbDH3m+Y=", clientOptions);

            processor = client.CreateProcessor("checkpath", new ServiceBusProcessorOptions());

            try
            {
                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;

                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");

                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}*/

using Azure.Messaging.ServiceBus;

namespace ReceiveProject
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            while (true)
            {
                await ReceiveAndProcessMessage();

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

        private static async Task ReceiveAndProcessMessage()
        {
            ServiceBusClient client = new ServiceBusClient("Endpoint=sb://servicequeue.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=EkaUVeT+KxhKvKQ/IeqKC28k19YQaUnZh+ASbDH3m+Y=");
            ServiceBusProcessor processor = client.CreateProcessor("checkpath", new ServiceBusProcessorOptions());

            async Task MessageHandler(ProcessMessageEventArgs args)
            {
                string body = args.Message.Body.ToString();
                Console.WriteLine($"Received: {body}");

                await args.CompleteMessageAsync(args.Message);
            }

            Task ErrorHandler(ProcessErrorEventArgs args)
            {
                Console.WriteLine(args.Exception.ToString());
                return Task.CompletedTask;
            }

            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            await processor.StartProcessingAsync();

            Console.WriteLine("Waiting for messages... Press any key to stop.");
            Console.ReadKey();

            await processor.StopProcessingAsync();
            await processor.DisposeAsync();
            await client.DisposeAsync();
        }
    }
}