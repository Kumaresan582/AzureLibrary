using AzureLibrary.AzureServiceBus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureLibrary.AzureServiceBus.Interface
{
    public interface IQueueProvider
    {
        IMessagePublisher GetPublisher(string queueName, QueueOptions queueOptions = null);
    }
}
