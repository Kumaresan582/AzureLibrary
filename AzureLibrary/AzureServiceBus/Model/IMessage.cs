using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureLibrary.AzureServiceBus.Model
{
    public interface IMessage
    {
        /// <summary> Message body as string </summary>
        /// <value> The body. </value>
        string Body { get; set; }

        /// <summary>
        /// To set the ContentType by default for the message 
        /// </summary>
        string ContentType { get; set; }

        /// <summary> Correlation-id for a messaage. </summary>
        /// <value> The correlation identifier. </value>
        string CorrelationId { get; set; }

        /// <summary> 
        ///   Message unique id genearted from message bus for marking a message to be completed/abondon
        /// </summary>
        /// <value> The delevery tag. </value>
        string DeleveryTag { get; set; }

        /// <summary> Custom properties of a message </summary>
        /// <value> The headers. </value>
        IDictionary<string, object> Headers { get; set; }

        /// <summary> Id for the message </summary>
        /// <value> The message identifier. </value>
        string MessageId { get; set; }

        /// <summary>
        /// Gets or Sets application specific subject
        /// </summary>
        string Subject { get; set; }

        DateTime? ScheduledEnqueueTimeUtc { get; set; }
    }
}
