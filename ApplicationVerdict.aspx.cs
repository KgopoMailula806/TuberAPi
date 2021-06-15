using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuberAPI.models;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;

namespace QuadCore_Website
{
    public partial class ApplicationVerdict : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                meetings.InnerHtml = displayMeetings();
                if (Request.QueryString["verdict"] != null && Request.QueryString["tID"] != null)
                {
                    switch(Request.QueryString["verdict"])
                    {
                        case "accept":
                            {
                                if (accept(Request.QueryString["tID"]))
                                {
                                    string strMsg = "Tutor has been accepted";
                                    string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                                    send("Tuber Recruiment Centre", Session["verdictEmail"].ToString(), EmailHelper.makeAcceptedEmail(Session["verdictName"].ToString(), "Keane Mailula"));

                                    Response.Write(script);
                                }
                                else
                                {
                                    string strMsg = "Tutor has been rejected";
                                    string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                                    Response.Write(script);
                                    Response.Redirect("ManagerDashboard.aspx");
                                }
                            }
                            break;
                        case "reject":
                            {

                                if (reject(Request.QueryString["tID"]))
                                {
                                    string strMsg = "Booking Cancelation was successful";
                                    string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";

                                    send("Tuber Recruitment Centre", Session["verdictEmail"].ToString(), EmailHelper.makeRejectionEmail(Session["verdictName"].ToString(), "Keane Mailula"));

                                    Response.Write(script);

                                }
                                else
                                {
                                    string strMsg = "Booking Cancelation was successful";
                                    string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                                    Response.Write(script);
                                    Response.Redirect("ManagerDashboard.aspx");
                                }
                            }
                            break;
                    }
                }
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TutorID"></param>
        public bool accept(string TutorID)
        {
            string response = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/acceptTutor/" + TutorID);
            if (Convert.ToInt32(response) == 1)
            {
                string userJson = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "/api/User/GetUserDetailsByTutorID/" + TutorID);
                if(userJson.Length > 0)
                {
                    User user = JsonConvert.DeserializeObject<User>(userJson);
                    Session["verdictName"] = user.FullNames + " " + user.Surname;
                    Session["verdictEmail"] = user.Email_Address;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TutorID"></param>
        public bool reject(string TutorID)
        {
            string response = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/rejectTutor/" + TutorID);
            if (Convert.ToInt32(response) == 1)
            {
                string userJson = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "/api/User/GetUserDetailsByTutorID/" + TutorID);
                if (userJson.Length > 0)
                {
                    User user = JsonConvert.DeserializeObject<User>(userJson);
                    Session["verdictName"] = user.FullNames + " " + user.Surname;
                    Session["verdictEmail"] = user.Email_Address;
                }
                return true;
            }
            return false;
        }

        private string displayMeetings()
        {
            string display = "";
            int counter = 0;
            List<Meeting> meetings = MeetingFunctionality.getVerdictObjects();
            
            if(meetings != null)
            {
                foreach (Meeting m in meetings)
                {
                    User tutor = MeetingFunctionality.getUser(m.TutorID);

                    display += "<tr class='text-centre'><td>" + ++counter + "</td>";
                    display += "<td>" + tutor.FullNames + " " + tutor.Surname + "</td><th>&nbsp</th>";
                    display += "<td>" + m.Venue + "</td><th>&nbsp</th>";
                    display += "<td>" + m.Time + "</td>";
                    display += "<td>" + m.Date + "</td>";
                    display += "<td class='text-nowrap'>" +
                               "<p style='display: inline-table'>" +
                               "<a class='btn btn-primary' href='?verdict=accept&tID=" + m.TutorID + "'>Appoint Tutor</a>" +
                               "<a class='btn btn-primary' href='?verdict=reject&tID=" + m.TutorID + "'>Reject Application</a></p></td>";
                }
                return display;
            }

            verdic.InnerHtml = "There no log applications available";
            return display;


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

            Session["verdictName"] = null;
            Session["verdictEmail"] = null;

            // message is sent successfully
            Response.Redirect("ManagerDashboard.aspx");
            
        }
    }
}