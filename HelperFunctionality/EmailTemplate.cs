using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace QuadCore_Website.HelperFunctionality
{
    public class EmailTemplate
    {
        // add Asyc=true
        // in <% %> tags in the pages required
        public void send(string ourName, string recipientEmail, string msgText)
        {
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "tuber.bizness@gmail.com",
                    Password = "testData123"
                }
            };

            // from email address w/ a display name of "Tutor Recruitment"
            MailAddress from = new MailAddress("tuber.bizness@gmail.com", ourName); // our name is gonna be the from in the recipients p.o.v

            // to email address w/ a user display name
            MailAddress to = new MailAddress(recipientEmail, "Tuber Client");

            MailMessage message = new MailMessage()
            {
                From = from,
                Subject = "Tutor Application",
                Body = msgText,
                IsBodyHtml = true, // see EmailHelper class for easy formatting

            };

            message.To.Add(to);

            client.SendCompleted += Client_SendCompleted; // c# lamda expression
            client.SendMailAsync(message);
        }

        public void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // fails to send email
            }

            // message is sent successfully
        }
    }
}