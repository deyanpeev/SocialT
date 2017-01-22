namespace SocialT.Web.Services
{
    using System.Threading.Tasks;
    using System.Net.Mail;
    using System;
    using System.Web.Configuration;
    using System.Net;
    using System.Diagnostics;

    public static class GeneralMailMessageService
    {
        public static async Task SendEmail(string to, string subject, string textAsHtml)
        {
            //MailMessage mailMsg = new MailMessage();
            //mailMsg.To.Add(new MailAddress(to));
            //mailMsg.From = new MailAddress(WebConfigurationManager.AppSettings["EmailFrom"], "SocialT");

            //// Subject and multipart/alternative Body
            //mailMsg.Subject = subject;
            //mailMsg.IsBodyHtml = true;
            //mailMsg.Body = textAsHtml;

            //// Init SmtpClient and send
            //var credentials = new NetworkCredential(WebConfigurationManager.AppSettings["Username"],
            //                                         WebConfigurationManager.AppSettings["EmailPassword"]);

            //SmtpClient smtpClient = new SmtpClient(WebConfigurationManager.AppSettings["EmailHost"],
            //                                         Convert.ToInt32(WebConfigurationManager.AppSettings["EmailPortNumber"]));
            ////System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("username@domain.com", "yourpassword");
            //smtpClient.Credentials = credentials;

            //smtpClient.Send(mailMsg);

            Debug.WriteLine("Mail sended.");
        }
    }
}