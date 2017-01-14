namespace SocialT.Web.Services
{
    using Microsoft.AspNet.Identity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Configuration;

    using System.Text;
    using System.Net.Mail;
    using System.Net.Mime;

    public class IdentityMessageService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task ConfigSendGridasync(IdentityMessage message)
        {
            var myMessage = SendGrid.Mail.GetInstance();

            myMessage.AddTo(message.Destination);
            myMessage.From = new System.Net.Mail.MailAddress(WebConfigurationManager.AppSettings["EmailFrom"], "SocialT");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            var credentials = new NetworkCredential(WebConfigurationManager.AppSettings["Username"],
                                                     WebConfigurationManager.AppSettings["EmailPassword"]);

            // Create a Web transport for sending email.
            var transportWeb = SendGrid.Transport.Web.GetInstance(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                //Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }
    }
}
