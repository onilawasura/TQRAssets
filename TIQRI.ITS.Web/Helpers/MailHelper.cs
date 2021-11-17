using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace TIQRI.ITS.Web.Helpers
{
    public static class MailHelper
    {
        public static void SendNotification(string messageSubject, string messageContent, string toAddressList)
        {
            try
            {
                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], ConfigurationManager.AppSettings["FromName"]);
                var fromPassword = ConfigurationManager.AppSettings["FromPassword"];

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                var message = new MailMessage()
                {
                    Subject = messageSubject,
                    Body = messageContent,
                    IsBodyHtml = true
                };
                message.From = fromAddress;

                foreach (var address in toAddressList.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.To.Add(address);
                }
                smtp.Send(message);

            }
            catch (Exception exception)
            {
            }

        }
    }
}