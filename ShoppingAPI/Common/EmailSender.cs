using System;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Common
{
    /// <summary>
    /// This class is responsible for Email Sender.
    /// </summary>
    public class EmailSender
    {
        /// <summary>
        /// </summary>
        private readonly string br = "<br/>";

        /// <summary>
        /// Initializes the email sender.
        /// </summary>
        public EmailSender()
        {
            RecipientName = "";
        }

        /// <summary>
        /// Gets or sets the To value.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the recipient name.
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// Sends the forgot password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="isReset"></param>
        public void SendForgotPasswordAsync(string password, bool isReset = false)
        {
            try
            {
                var body = new StringBuilder();
                body.Append($"Dear {RecipientName}, {br}{br}");
                if (isReset)
                    body.Append($"The admin has reset the password for you, {br}");
                else
                    body.Append($"You have requested for a password reset, {br}");

                body.Append($"Your new password is {password} {br}{br}");
                body.Append(
                    $"Notes: You must login with this new password and your old password will no longer be accepted {br}{br}");
                body.Append($"Best regards, {br}");
                body.Append("ShoppingKart");
                new Task(() =>
                {
                    Email.SendMail(To, Email.SenderName, Email.SenderEmail, "ShoppingKart | Password Reset",
                        body.ToString(), true, true);
                }).Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}