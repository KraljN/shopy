using Application.Email;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Implementation.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        public void Send(SendEmail send)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("pera96392@gmail.com", "Ppera12345")
            };

            var message = new MailMessage("pera96392@gmail.com", send.SendTo);
            message.Subject = send.Subject;
            message.Body = send.Content;
            message.IsBodyHtml = true;
            //If there is exception you need to allow less secure apps in your mail account.
            //Your user will be created even with this exception
            smtp.Send(message);
        }
    }
}
