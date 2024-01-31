using Azure.Messaging.ServiceBus;
using AzureLibrary.AzureServiceBus.Interface;
using AzureLibrary.AzureServiceBus.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureLibrary.AzureServiceBus.ServiceBus
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly ServiceBusSender _serviceBusSenderClient;

        public MessagePublisher(ServiceBusSender serviceBusSender)
        {
            _serviceBusSenderClient = serviceBusSender;
        }

        public async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
        {
            ServiceBusMessage msg = new ServiceBusMessage(Encoding.UTF8.GetBytes(message.Body));

            if (!string.IsNullOrWhiteSpace(message.Subject))
                msg.Subject = message.Subject;

            if (message.ScheduledEnqueueTimeUtc.HasValue)
                msg.ScheduledEnqueueTime = message.ScheduledEnqueueTimeUtc.Value;

            if (message.Headers != null)
            {
                foreach (KeyValuePair<string, object> item in message.Headers)
                {
                    msg.ApplicationProperties.Add(item.Key, item.Value);
                }
            }
            msg.ContentType = message.ContentType == null ? Model.Constants.DefaultContentType : message.ContentType;
            msg.MessageId = message.MessageId;
            msg.CorrelationId = message.CorrelationId;

            try
            {
                await _serviceBusSenderClient.SendMessageAsync(msg, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the data.", ex);
            }
        }
    }
}