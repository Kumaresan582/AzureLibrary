using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureLibrary.AzureServiceBus.Model
{
    public class Message:IMessage
    {
        public Message()
        {
            Headers = new Dictionary<string, object>();
        }

        /// <summary> Message body as string </summary>
        /// <value> The body. </value>
        public string Body { get; set; }

        /// <summary>
        /// To set the ContentType by default for the message
        /// </summary>
        public string ContentType { get; set; }

        /// <summary> Correlation-id for a messaage. </summary>
        /// <value> The correlation identifier. </value>
        public string CorrelationId { get; set; }

        /// <summary> 
        ///   Message unique id genearted from message bus for marking a message to be completed/abondon
        /// </summary>
        /// <value> The delevery tag. </value>
        public string DeleveryTag { get; set; }

        /// <summary> Custom properties of a message </summary>
        /// <value> The headers. </value>
        public IDictionary<string, object> Headers { get; set; }

        /// <summary> Id for the message </summary>
        /// <value> The message identifier. </value>
        public string MessageId { get; set; }

        /// <summary>
        /// Gets or Sets application specific subject
        /// </summary>
        public string Subject { get; set; }
        public DateTime? ScheduledEnqueueTimeUtc { get; set; }
    }
}
