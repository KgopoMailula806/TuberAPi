using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using QuadCore_Website.HelperFunctionality;

namespace QuadCore_Website
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnEmail_Click(object sender, EventArgs e)
        {
            string recipient = email.Value;
            
            // check if the user email is in db
            string checkUrl = SITEConstants.BASE_URL + "api/User/CheckIfEmailExists/" + recipient;
            string checkResponse = ApiComnunication.getJsonEntities(checkUrl);

            if(checkResponse.Equals("1")) // 1 = yes
            {
                string message = EmailHelper.setHTML("Use the link below reset your password, it expires in 10 minutes.", "reset my password",
                SITEConstants.Base_SITE_URL + "anoynmus_tuber_password_reset.aspx?email=" + recipient + "&timeStamp=" + string.Format("{0: dd/MM/yyyy hh:mm:ss}", DateTime.Now));

                // send reset email
                send("Tuber Reset Centre", recipient, message, "Password Reset.");

            }
            else
            {
                // tell user to re-type password
                string msgBox = "<div class='col-md-12'>";
                msgBox += "<label class='font-weight-bold' for='message'>Message</label>";
                msgBox += "<p style='color: red;'>Invalid Email, Try Again!</p></div>";
                ErrorMsgBox.Visible = true;
                ErrorMsgBox.InnerHtml = msgBox;

                email.Value = "";
            }

            
        }

        public void send(string ourName, string recipientEmail, string msgText, string subject)
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
                Subject = subject,
                Body = msgText,
                IsBodyHtml = true, // see EmailHelper class for easy formatting

            };

            message.To.Add(to);

            client.SendCompleted += Client_SendCompleted; // c# lamda expression
            client.SendMailAsync(message);
        }

        public void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string msgBox = "<div class='col-md-12'>";
            msgBox += "<label class='font-weight-bold' for='message'>Message</label>";
            
            
            if (e.Error != null)
            {
                msgBox += "<p style='color: red;'>Something Went Wrong When Sending You the Email, Try Again!</p></div>";
            }
            else
            {
                msgBox += "<p style='color: green;'>Email Is Sent To Your Email Address!</p></div>";
            }

            ErrorMsgBox.Visible = true;
            ErrorMsgBox.InnerHtml = msgBox;
        }
    }
}