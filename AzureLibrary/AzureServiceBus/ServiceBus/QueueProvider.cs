using Azure.Messaging.ServiceBus.Administration;
using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureLibrary.AzureServiceBus.Model;
using AzureLibrary.AzureServiceBus.Interface;

namespace AzureLibrary.AzureServiceBus.ServiceBus
{
    public class QueueProvider: IQueueProvider
    {
        private readonly string _connectionString;
        private readonly IDictionary<string, ServiceBusSender> _pool;
        private ServiceBusClient _serviceBusClient;
        private ServiceBusAdministrationClient _namespaceManager;
        public QueueProvider(string connectionString)
        {
            _connectionString = connectionString;
            _pool = new Dictionary<string, ServiceBusSender>();
        }

        private ServiceBusAdministrationClient managementClient => _namespaceManager ??
           (_namespaceManager = new ServiceBusAdministrationClient(_connectionString));

        private ServiceBusClient serviceBusClient => _serviceBusClient ??
                                           (_serviceBusClient = new ServiceBusClient(_connectionString, new ServiceBusClientOptions
                                           {
                                               RetryOptions = new ServiceBusRetryOptions()
                                               {
                                                   Mode = ServiceBusRetryMode.Fixed,
                                                   Delay = TimeSpan.FromSeconds(2),
                                                   MaxDelay = TimeSpan.FromMinutes(3),
                                                   MaxRetries = 5
                                               }
                                           }));

        private ServiceBusSender PublisherClient(string queuename, QueueOptions queueOptions)
        {
            lock (_pool)
            {
                ServiceBusSender client;
                var token = $"{queuename}::{10}";

                if (_pool.TryGetValue(token, out client) && client != null && !client.IsClosed)
                    return client;

                if (!managementClient.QueueExistsAsync(queuename).Result)
                {
                    if (queueOptions != null)
                    {
                        CreateQueueOptions createQueueOptions = new CreateQueueOptions(queuename) { LockDuration = queueOptions.LockDuration, MaxDeliveryCount = queueOptions.MaxDeliveryCount };
                        managementClient.CreateQueueAsync(createQueueOptions);
                    }
                    else
                    {
                        managementClient.CreateQueueAsync(queuename);
                    }
                }


                client = serviceBusClient.CreateSender(queuename);

                _pool[token] = client;

                return client;
            }
        }

        public IMessagePublisher GetPublisher(string path, QueueOptions queueOptions = null)
        {
            return new MessagePublisher(PublisherClient(path, queueOptions));
        }

    }
}
