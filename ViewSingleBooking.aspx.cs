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

namespace QuadCore_Website
{
    public partial class ViewSingleBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
                Response.Redirect("Login.aspx");

            if(Request.QueryString["status"] != null)
            {
                switch (Request.QueryString["status"].ToString())
                {
                    case "-1":
                        {
                            CancelBooking.Visible = false;
                        }
                        break;
                    case "0":
                        {
                            CancelBooking.Visible = false;
                        }
                        break;
                    case "1":
                        {
                            CancelBooking.Visible = true;
                        }
                        break;
                }
            }

            if(!IsPostBack)
            {
                // the booked session details using the User table primary key
                if (Session["UserStatus"].ToString().Equals("Client"))
                {
                    // get tutor information
                    //get the booking belonging to the client
                    ClientBooking clientbooking = ClientBookings.GetBooking(Request.QueryString["BooKingID"].ToString());

                    if (clientbooking != null)
                    {

                        DateTime sessionDateTime = DateTime.Parse(clientbooking.Date_Time);

                        User tutorDetails = UserFunctionality.GetTutorsUserTableDetails("" + clientbooking.Tutor_Table_Reference);
                        if (tutorDetails != null)
                        {
                            //string session = "";
                            requestName.InnerText = tutorDetails.FullNames + " " + tutorDetails.Surname;
                            requestNumber.InnerText = tutorDetails.Email_Address;
                            requestLocation.InnerText = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/GetBookingrequestLocation/" + clientbooking.BookingDetails_BookingRequestTable_Reference);
                            requestModule.InnerText = setModuleName(clientbooking.BookingDetails_BookingRequestTable_Reference);
                            requestDate.InnerText = sessionDateTime.Date.ToString();
                            requestStartTime.InnerText = sessionDateTime.TimeOfDay.ToString();


                            //session += "<div class=\"col-md-3 course ftco-animate\">";
                            ////randomly select session
                            //session += "<div class=\"img\" style=\"background-image: url(images/course-1.jpg);\"></div>";
                            //session += "<div class=\"text pt-4\">";
                            //session += "<p class=\"meta d-flex\">";
                            ////Tutor name
                            //session += "<span><i class=\"icon-user mr-1\"></i></span>";
                            //session += "<span><i class=\"icon-user mr-1\"></i>" +  + "</span>";
                            ////Session Date
                            //session += "<span><i class=\"icon-calendar mr-1\"></i>Z" + + "</span>";
                            //session += "<span><i class=\"icon-timer mr-1\"></i>" +  + "</span>";
                            ////Session location
                            //string location = 
                            //session += "<span><i class=\"icon-pin_drop mr-1\"></i>" + location + "</span>";
                            //session += "</p>";
                            //session += "<h8><a> Module Name</a></h8>";
                            //session += "<p>Session Description/can be empty</p>";                      
                            //session += "</div>";
                            //session += "</div>";

                            //sessionPanel.InnerHtml += session;
                        }
                    }
                    else
                    {
                    }
                }
                else if (Session["UserStatus"].ToString().Equals("Tutor"))
                {
                    // get client information
                    //get the booking                
                    ClientBooking clientbooking = ClientBookings.GetBooking(Request.QueryString["BooKingID"].ToString());
                    if (clientbooking != null)
                    {

                        User clientDetails = UserFunctionality.GetClientsUserTableDetails("" + clientbooking.Client_Table_Reference);
                        if (clientDetails != null)
                        {
                            DateTime sessionDateTime = DateTime.Parse(clientbooking.Date_Time);

                            requestName.InnerText = clientDetails.FullNames + " " + clientDetails.Surname;
                            requestNumber.InnerText = clientDetails.Email_Address;
                            requestLocation.InnerText = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/GetBookingrequestLocation/" + clientbooking.BookingDetails_BookingRequestTable_Reference);
                            requestModule.InnerText = setModuleName(clientbooking.BookingDetails_BookingRequestTable_Reference);
                            requestDate.InnerText = sessionDateTime.Date.ToString();
                            requestStartTime.InnerText = sessionDateTime.TimeOfDay.ToString();

                            //string session = "";
                            //session += "<div class=\"col-md-3 course ftco-animate\">";
                            ////randomly select session
                            //session += "<div class=\"img\" style=\"background-image: url(images/course-1.jpg);\"></div>";
                            //session += "<div class=\"text pt-4\">";
                            //session += "<p class=\"meta d-flex\">";
                            ////Tutor name
                            //session += "<span><i class=\"icon-user mr-1\"></i>" +  + "</span>";
                            //session += "<span><i class=\"icon-user mr-1\"></i>" +  + "</span>";
                            ////Session Date
                            //session += "<span><i class=\"icon-calendar mr-1\"></i>Z" + sessionDateTime.Date + "</span>";
                            //session += "<span><i class=\"icon-timer mr-1\"></i>" + sessionDateTime.TimeOfDay + "</span>";
                            ////Session location
                            //string location = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/GetBookingrequestLocation/" + clientbooking.BookingDetails_BookingRequestTable_Reference);
                            //session += "<span><i class=\"icon-pin_drop mr-1\"></i>" + location + "</span>";
                            //session += "</p>";
                            //session += "<h8><a href = \"#\" > Module Name</a></h8>";
                            //session += "<p>Session Description/can be empty</p>";                          
                            //session += "</div>";
                            //session += "</div>";
                            //sessionPanel.InnerHtml += session;
                        }
                    }
                    else
                    {
                    }
                }
            }
            
        }
        /// <summary>
        /// Redirects back to the bookins page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewBookedSessions.aspx?status=" + Request.QueryString["status"].ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string setModuleName(int id)
        {
            string bookingRes = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/GetIndividualBookingRequest/" + id);
            if (bookingRes.Length > 0)
            {
                BookingRequest b = JsonConvert.DeserializeObject<BookingRequest>(bookingRes);
                return ModuleFunctionality.getModule(b.ModuleID1).Module_Name;
            }

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelBookingBookingWith_Click(object sender, EventArgs e)
        {
            //deactivate the booking request
            if (Request.QueryString["BooKingID"] != null)
            {
                String deactivationURL = SITEConstants.BASE_URL + "api/ClientBooking/deactivateBooking/" + Request.QueryString["BooKingID"].ToString() + "/" + HttpContext.Current.Session["UserStatus"].ToString();
                String response = ApiComnunication.getJsonEntities(deactivationURL);
                if (response.Equals("1"))
                {
                    //BookingCancelation was successful
                    string strMsg = "Booking Cancelation was successful";
                    string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                    Response.Write(script);
                   // sandNotificatio();

                }
                else if (response.Equals("0"))
                {
                    //BookingCancelation was unsuccesful
                    string strMsg = "Booking Cancelation was unsuccessful";
                    string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                    Response.Write(script);
                }

            }
            else
            {
                Response.Redirect("Login.aspx");
            }            
        }

        private void sandNotificatio()
        {
           
            ClientBooking clientbooking = ClientBookings.GetBooking(Request.QueryString["BooKingID"].ToString());
            string userId_ = HttpContext.Current.Session["ID"].ToString();

            Event bookingEvent = new Event();

            bookingEvent.EventType = "BookingCancelation";

        
            //get Module
            String moduleID = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/ClientBooking/getBookingModuleID/" + clientbooking.BookingDetails_BookingRequestTable_Reference); 
            String module = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Modules/GetModuleName/" + clientbooking.BookingDetails_BookingRequestTable_Reference);
            if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                bookingEvent.Description = "The Tutor: " + ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/GetTutorName/" + userId_) + "has rejected the request for " + module + " Set For " + clientbooking.Date_Time;
            else
                bookingEvent.Description = "The Tutor: " + ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/GetTutorName/" + userId_) + "has rejected the request for " + module + " Set For " + clientbooking.Date_Time;

            bookingEvent.Description += "_" + module + "_" + userId_ ;

            // set the booking request event and return its primary key
            int eventId = Notification_Functionality.setEvent(bookingEvent);
            if (eventId > 0)
            {
                Notification notification = new Notification();
                notification.DatePosted = DateTime.Today.ToString();
                notification.Time = DateTime.Now.TimeOfDay.ToString();
                notification.Seen = 0;
                if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                {
                    notification.PersonTheNotificationConcerns = "Tutor";
                    notification.User_Table_Reference = Int32.Parse(ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetUserTableID/" +clientbooking.Tutor_Table_Reference));
                }
                else
                { 
                    notification.PersonTheNotificationConcerns = "Client";
                    notification.User_Table_Reference = Int32.Parse(ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetUserTableID/" + clientbooking.Client_Table_Reference));
                }
                   
                notification.Event_Table_Reference = eventId;

                if (Notification_Functionality.setNotification(notification) > 0)
                {

                    if (Notification_Functionality.setNotification(notification) > 0)
                    {
                        string strMsg = "Booking Cancelation was successful";
                        string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                        Response.Write(script);
                        Response.Redirect("Confirmed.aspx?ConfirmatioType=BookingRejection&status=" + HttpContext.Current.Session["UserStatus"]);
                    }
                }
            }
            else
            {
            }
        }
 
    }
}