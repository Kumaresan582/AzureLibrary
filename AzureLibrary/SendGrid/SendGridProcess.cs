using SendGrid;
using SendGrid.Helpers.Mail;

namespace AzureLibrary.SendGrid
{
    public class SendGridProcess
    {
        private readonly string _SendGridApiKey;
        private readonly string _SendGridFrom;
        private readonly string _SendGridName;
        private readonly string _SendGridSubject;
        private readonly string _SendGridTemplateId;

        public SendGridProcess(string sendgridapiKey, string sendgridfrom, string sendgridname, string sendgridsubject, string sendGridTemplateId)
        {
            _SendGridApiKey = sendgridapiKey;
            _SendGridTemplateId = sendGridTemplateId;
            _SendGridFrom = sendgridfrom;
            _SendGridName = sendgridname;
            _SendGridSubject = sendgridsubject;
        }

        public async Task<bool> SendEmailAsync(string email)
        {
            var client = new SendGridClient(_SendGridApiKey);

            var msg = new SendGridMessage
            {
                From = new EmailAddress(_SendGridFrom, _SendGridName),
                Subject = _SendGridSubject,
                PlainTextContent = "hii"
            };

            msg.AddTo(new EmailAddress(email, "User Mail"));

            var response = await client.SendEmailAsync(msg);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
