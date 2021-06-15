using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net.Mail;
using System.Net;
using TuberAPI.models;
using Newtonsoft.Json;

namespace QuadCore_Website
{
    public partial class MeetingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            meetings.InnerHtml = displayMeetings();
            if (Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"] == "Cancel")
                {
                    cancel(Request.QueryString["meetingID"]);
                }                
            }
        }

        private void cancel(string meetingID)
        {
            string response = ApiComnunication.DeleteByURLEntity(SITEConstants.BASE_URL + "api/Meeting/DeleteMeeting/" + meetingID);
            if (Convert.ToInt32(response) == 1)
            {
                string userJson = ApiComnunication.DeleteByURLEntity(SITEConstants.BASE_URL + "api/User/GetUser/" + Request.QueryString["uID"].ToString());
                if(userJson.Length > 0)
                {
                    User user = JsonConvert.DeserializeObject<User>(userJson);
                    send("Tuber Recruitment Centre", user.Email_Address, EmailHelper.meetingCancelation(user.FullNames + " " + user.Surname, "Keane Mailula"));
                }
                return;
            }
        }

        private string displayMeetings()
        {
            string display = "";
            int counter = 0;
            foreach(Meeting m in MeetingFunctionality.GetMeetings())
            {
                User tutor = MeetingFunctionality.getUser(m.TutorID);
                if (tutor == null)
                    continue;

                display += "<tr class='text-centre'><td>"+ ++counter +"</td>";
                display += "<td>"+ tutor.FullNames + " " + tutor.Surname +"</td><th>&nbsp</th>";
                display += "<td>"+ m.Venue +"</td><th>&nbsp</th>";
                display += "<td>"+ m.Time +"</td>";
                display += "<td>" + m.Date + "</td>";
                display += "<th>&nbsp</th>";
                display += "<td class='text-nowrap'>" +
                           "<p style='display: inline-table'><a class='btn btn-primary' href='ApplicationView.aspx?tID=" + m.TutorID + "&uID=" + tutor.Id + "'>view application</a></p>" +
                           "<a class='btn btn-primary' href='EditMeeting.aspx'>reschedule</a>" +
                           "<a class='btn btn-primary' href='?action=Cancel&meetingID="+m.Id+"&uID=" + tutor.Id + "'>cancel</a></p></td>";
            }

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

            // message is sent successfully

        }

    }
}