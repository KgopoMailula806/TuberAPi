using Newtonsoft.Json;
using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuberAPI.models;
using System.Net.Mail;
using System.Net;

namespace QuadCore_Website
{
    public partial class ApplicationView : System.Web.UI.Page
    {
        static string tId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //tId = Request.QueryString["tID"];
            string uId = Request.QueryString["uID"];
            string tutorIDPath = SITEConstants.BASE_URL + "api/Tutor/GetTutorTablePrimaryKey/" + uId;
            tId = ApiComnunication.getJsonEntities(tutorIDPath);
            //get information from database
            string UriPath = SITEConstants.BASE_URL + "api/User/GetUser/" + uId;
            string userTableResponseBody = ApiComnunication.getJsonEntities(UriPath);

            User userResponseObj = JsonConvert.DeserializeObject<User>(userTableResponseBody);

            if (userResponseObj != null)
            {

                string innerText = "";

                innerText += "<p><b>Name: </b>" + userResponseObj.FullNames + "</p>";
                innerText += "<p><b>Surname: </b>" + userResponseObj.Surname + "</p>";
                innerText += "<p><b>Number: </b> " + userResponseObj.Valid_Phone_Number + "</p>";
                innerText += "<p><b>Email: </b> " + userResponseObj.Email_Address + "</p>";
                innerText += "<p><b>Gender: </b> " + userResponseObj.Gender + "</p>";
                innerText += "<p><b>Age: </b> " + userResponseObj.Age + "</p>";
                TutorInfor.InnerHtml = innerText;

                Session["TutorName"] = userResponseObj.FullNames + " " + userResponseObj.Surname;
                Session["TutorEmail"] = userResponseObj.Email_Address;

                //Get image
                Document image = FileFunctionality.GetFile(userResponseObj.Image);
                if (image != null)               
                    TutorImage.ImageUrl = "data:image/" + image.Extension + ";base64," + image.DocumentData;

                string documentText = "";
                string clearanceID = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/TutorDocument/GetClearance/" + tId);
                string cvID = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/TutorDocument/GetCV/" + tId);
                string transcriptID = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/TutorDocument/GetTranscript/" + tId);

                documentText += "<p><b>Curriculum Vitae: </b> <br><a href='pdfViewer.aspx?dId=" + cvID + "&type=CV' target='_blank'> open here <a/></p>";
                documentText += "<p><b>Academic Record: </b> <br><a href='pdfViewer.aspx?dId=" + transcriptID + "&type=AR' target='_blank'> open here <a/></p>";
                documentText += "<p><b>Police Clearance: </b> <br><a href='pdfViewer.aspx?dId=" + clearanceID + "&type=PC' target='_blank'> open here <a/></p>";

                documents.InnerHtml = documentText;

            }
        }

        protected void btnSubMeeting_Click(object sender, EventArgs e)
        {

            string tyd = time.Value;
            string datum = date.Value;
            string plek = Venue.SelectedValue;
            string uId = Request.QueryString["uID"];
            string tutorIDPath = SITEConstants.BASE_URL + "api/Tutor/GetTutorTablePrimaryKey/" + uId;
            
            string tId = ApiComnunication.getJsonEntities(tutorIDPath);

            bool isAdded = MeetingFunctionality.addMeeting(datum, tyd, plek, tId,"Meeting");

            if(isAdded)
            {
                Response.Write("<script>alert('Meeting was set successfully')</script>");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "closeForm();", true);               

                //send notifiaction to the tutor
                Event bookingEvent = new Event();
                bookingEvent.EventType = "InterviewMeeting";

                //decription specify the description of the notification 
                //elemets of the description,1) time, 2) the date, 3) user table Id
                bookingEvent.Description = "You have been selected a tutor possition you applied for_" +
                                                                                                  tyd + "_" +
                                                                                                  datum + "_" + uId;

                int eventId = Notification_Functionality.setEvent(bookingEvent);
                if (eventId > 0)
                {
                    Notification notification = new Notification();
                    notification.DatePosted = DateTime.Today.ToString();
                    notification.Time = DateTime.Now.TimeOfDay.ToString();
                    notification.Seen = 0;
                    notification.PersonTheNotificationConcerns = "Tutor";
                    notification.Event_Table_Reference = eventId;
                    notification.User_Table_Reference = Int32.Parse(uId);
                    Notification_Functionality.setNotification(notification);

                }
            }

            // send the email
            send("Tuber Recruitment", Session["TutorEmail"].ToString(), EmailHelper.makeMeetingEmail(Session["TutorName"].ToString(), "Jacob Muzonde",datum,tyd, plek),"accept");

        }

        //[WebMethod]
        //public static void Shortlist()
        //{
        //    bool isAdded = MeetingFunctionality.addMeeting("", "", "", tId, "Shortlist");
        //    if (isAdded)
        //    {
        //        send("Tuber Recruitment", Session["TutorEmail"].ToString(), EmailHelper.makeRejectionEmail(Session["TutorName"].ToString(), "Jacob Muzonde"), "reject");
        //    }
        //    return;
        //    }
        //}

        //[WebMethod]
        //public static void Reject()
        //{
        //    string response = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL +"api/Tutor/rejectTutor/" + tId);
        //    if (Convert.ToInt32(response) == 1)
        //    {
        //        send("Tuber Recruitment", Session["TutorEmail"].ToString(), EmailHelper.makeMeetingEmail(Session["TutorName"].ToString(), "Jacob Muzonde", "", "", ""), "shortlist");
        //        return;
        //    }
        //}

        protected void btnShortlist_Click(object sender, EventArgs e)
        {
            string uId = Request.QueryString["uID"];
            string tutorIDPath = SITEConstants.BASE_URL + "api/Tutor/GetTutorTablePrimaryKey/" + uId;

            string tId = ApiComnunication.getJsonEntities(tutorIDPath);
            bool isAdded = MeetingFunctionality.addMeeting("", "", "", tId, "Shortlist");
            if(isAdded)
            {
                //send("Tuber Recruitment", Session["TutorEmail"].ToString(), EmailHelper.makeMeetingEmail(Session["TutorName"].ToString(), "Jacob Muzonde", "", "", ""), "shortlist");
            }
        }

        protected void btnRejection_Click(object sender, EventArgs e)
        {
            string uId = Request.QueryString["uID"];
            string tutorIDPath = SITEConstants.BASE_URL + "api/Tutor/GetTutorTablePrimaryKey/" + uId;

            string tId = ApiComnunication.getJsonEntities(tutorIDPath);
            string response = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/rejectTutor/" + tId);
            if(Convert.ToInt32(response) == 1)
            {
                send("Tuber Recruitment", Session["TutorEmail"].ToString(), EmailHelper.makeRejectionEmail(Session["TutorName"].ToString(), "Jacob Muzonde"),"reject");
            }
            

        }     

        protected void btnAddshortList_Click(object sender, EventArgs e)
        {
            string uId = Request.QueryString["uID"];
            string tutorIDPath = SITEConstants.BASE_URL + "api/Tutor/GetTutorTablePrimaryKey/" + uId;
            tId = ApiComnunication.getJsonEntities(tutorIDPath);
            bool isAdded = MeetingFunctionality.addMeeting("", "", "", tId, "Shortlist");
            if (isAdded)
            {
                //send("Tuber Recruitment", Session["TutorEmail"].ToString(), EmailHelper.makeMeetingEmail(Session["TutorName"].ToString(), "Jacob Muzonde","","",""),"shortlist");
                return;
            }
        }

        public void send(string ourName, string recipientEmail, string msgText, string type)
        {
            Session["EmailType"] = type; 

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
            if (Session["EmailType"].ToString().Equals("accept"))
            {
                Response.Redirect("MeetingList.aspx");
            }
            else if(Session["EmailType"].ToString().Equals("shortlist"))
            {

            }
            else if (Session["EmailType"].ToString().Equals("reject"))
            {
                Response.Write("<script>alert('Tutor is Rejcted')</script>");
                Response.Redirect("TutorApplications.aspx");
            }
        }

    }
}