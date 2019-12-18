using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace ShoppingAPI.Common
{
    /// <summary>
    /// This class is responsible for the email.
    /// </summary>
    public static class Email
    {
        /// <summary>
        /// The sender name.
        /// </summary>
        public static string SenderName = ConfigurationManager.AppSettings["SenderName"];

        /// <summary>
        /// The sender email value.
        /// </summary>
        public static string SenderEmail = ConfigurationManager.AppSettings["SenderEmail"];

        /// <summary>
        /// Is mail sent value.
        /// </summary>
        public static bool isMailSent = false;

        /// <summary>
        /// Sends the mails.
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="senderDisplayName"></param>
        /// <param name="senderEmail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHTML"></param>
        /// <param name="isAsync"></param>
        public static void SendMail(string receiverEmail, string senderDisplayName, string senderEmail, string subject,
            string body, bool isBodyHTML, bool isAsync)
        {
            try
            {
                if (!string.IsNullOrEmpty(receiverEmail))
                {
                    var smtpClient = new SmtpClient();
                    var fromAddress = senderDisplayName == string.Empty
                        ? new MailAddress(senderEmail)
                        : new MailAddress(senderEmail, senderDisplayName);
                    var message = new MailMessage {From = fromAddress, Subject = subject, IsBodyHtml = isBodyHTML};
                    message.To.Add(new MailAddress(receiverEmail));
                    message.Body = FixMailString(body);
                    smtpClient.Host = ConfigurationManager.AppSettings["SMTPPath"];
                    smtpClient.EnableSsl = true;
                    if (ConfigurationManager.AppSettings["SMTPPort"] != null)
                        smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
                    if (ConfigurationManager.AppSettings["SMTPUsername"] != null)
                        smtpClient.Credentials =
                            new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsername"],
                                ConfigurationManager.AppSettings["SMTPPassword"]);

                    if (isAsync)
                    {
                        object state = message;
                        if (message.To.Count > 0) smtpClient.SendAsync(message, state);
                    }
                    else
                    {
                        smtpClient.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Fix the string to qmail systems by ensuring that the caridge returns and linefeeds are correctly set
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string FixMailString(string body)
        {
            return body.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n") + "\r\n\r\n";
        }
    }
}