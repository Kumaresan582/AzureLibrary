using AzureLibrary.SendGrid;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace SendGridEmail
{
    public class program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                SendGridProcess sendGridProcess = new SendGridProcess(
                       "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",
                       "vignesh.vaf@yandex.com",
                       "Vignesh",
                       "Testing Purpose",
                       "d-40664f8c17354565994f6e118f89dace"
                   );

                string email = "kumarkumaresan135@gmail.com";
                bool result = await sendGridProcess.SendEmailAsync(email);

                if (result)
                {
                    Console.WriteLine("Email sent successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to send the email.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}