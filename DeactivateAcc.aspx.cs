using Newtonsoft.Json;
using QuadCore_Website.HelperFunctionality;
using QuadCore_Website.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuberAPI.models;
using System.Net;
using System.Net.Mail;

namespace QuadCore_Website
{
    public partial class DeactivateAcc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            //Remember to hash password

            //Check if the session is valid
            if (Session["Email"] != null || Response.Cookies["Email"] != null)
            {
                if (!(Password.Value.Equals("") && !email.Value.Equals("")))
                {
                    string checkUser = SITEConstants.BASE_URL+ "api/User/getUserByEmail/" + email.Value + "/" + Password.Value;
                    string JsonStringUserObject = ApiComnunication.getJsonEntities(checkUser);
                    
                    User existUser = JsonConvert.DeserializeObject<User>(JsonStringUserObject);

                    if (existUser != null)
                    {
                        //GetUserID and user reason
                        Reason res = new Reason();
                        res.Id = 0;
                        res.userID = existUser.Id;
                        if(Reason.Value == "")
                        {
                            res.content = "No reason given";
                        }
                        else { res.content = Reason.Value; }
                        
                        // send email
                        send("Tuber Account Management", email.Value, EmailHelper.makeDeactivateAccEmail(existUser.FullNames + " " + existUser.Surname,
                            "Jacob Muzonde", "?bariId=" + res.userID + "&resContent=" + res.content));
                       
                    }
                }    
                
            }
            else
            {
                //Person must sign in to disable their account.
                Response.Redirect("Login.aspx");
            }
        
        }

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
                Subject = "Account Deactivation",
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
            alert.Visible = true;
            body.Visible = false;

            //Logout user
            Session["Email"] = null;
            Session["ID"] = null;
            Session["UserStatus"] = null;

        }
    }
}