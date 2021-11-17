using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TIQRI.ITS.Domain.Helpers
{
    public class EmailHelper
    {
        public Task SendEMailsync(string emailTo, string subject, string message)
        {
            var fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromAccount"], ConfigurationManager.AppSettings["FromName"]);
            var toAddress = new MailAddress(emailTo);
            var fromPassword = ConfigurationManager.AppSettings["FromPassword"];

            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            return smtp.SendMailAsync(mailMessage);
        }
    }
}
