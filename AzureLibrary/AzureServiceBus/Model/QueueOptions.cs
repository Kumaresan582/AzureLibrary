using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureLibrary.AzureServiceBus.Model
{
    public class QueueOptions
    {
        public int MaxDeliveryCount { get; set; }
        public TimeSpan LockDuration { get; set; }
    }
}
