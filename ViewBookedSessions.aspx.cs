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
    public partial class ViewBookedSessions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Email"] == null)
                Response.Redirect("Login.aspx");
            if (!IsPostBack) {
                if (Request.QueryString["status"] != null)
                {
                    switch (Request.QueryString["status"].ToString())
                    {
                        case "-1":
                            {
                                canceled.Checked = true;
                                upcomming.Checked = false;
                                passed.Checked = false;
                            }
                            break;
                        case "0":
                            {
                                canceled.Checked = false;
                                upcomming.Checked = false;
                                passed.Checked = true;
                            }
                            break;
                        case "1":
                            {
                                canceled.Checked = false;
                                upcomming.Checked = true;
                                passed.Checked = false;
                            }
                            break;
                    }
                    setUpBookingSessions();
                }
                // the booked session details using the User table primary key
                setRecentlyExpiredSession();
            }
            
            
        }
        /// <summary>
        /// get the module name of the session
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string setModuleName(int id)
        {
            string bookingRes = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/GetIndividualBookingRequest/" + id);
            if(bookingRes.Length > 0)
            {
                BookingRequest b = JsonConvert.DeserializeObject<BookingRequest>(bookingRes);
                return "<td>" + ModuleFunctionality.getModule(b.ModuleID1).Module_Name + "</td>";
            }
            return "";
        }
        /// <summary>
        /// get the sessions on the user's command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_GetBookedSessions_Click(object sender, EventArgs e)
        {
            
            setUpBookingSessions();
        }

        /// <summary>
        /// Display function for client/tutor sessions booked or booked for
        /// </summary>        
        private string displaySession()
        {
            string rep = "<div class='col-md-3 course ftco-animate'>";
            rep += "<div class='img' style='background-image: url(images/course-1.jpg);'></div>";
            rep += "<div class='text pt-4'>";
            rep += "<p class='meta d-flex'>";
            rep += "<span><i class='icon-user mr-1'></i> " + "Jacob" + "</span>";
            rep += "<span><i class='icon-calendar mr-1'></i>" + "1999/03/01" + "</span>";
            rep += "<span><i class='icon-timer mr-1'></i>" + "16:30" + "</span>";
            rep += "<span><i class='icon-pin_drop mr-1'></i>" + "Congo" + "</span></p>";
            rep += "<h8><a href='#'>" + "Introduction to Statics" + "</a></h8>";
            rep += "<p>" + "Session Description/can be empty" + "</p>";
            rep += "<p><a href='SingleSession.aspx?ID=' " + "SomeID" + " class='btn btn-primary'>View Session</a></p>";
            rep += "</div></div>";

            return rep;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void setRecentlyExpiredSession()
        {
            //checks for session that should be expiered and deactivates them
            String uri = SITEConstants.BASE_URL + "api/ClientBooking/areTherAnyExpiredSessions/" + Session["ID"].ToString() + "/" + Session["UserStatus"].ToString();
            ApiComnunication.getJsonEntities(uri);
        }
        
       /// <summary>
       /// 
       /// </summary>
        private void setUpBookingSessions()
        {
            //See if either one of the seen or unseen field are checked
            if (!(upcomming.Checked) && !(passed.Checked) && !(canceled.Checked))
            {
                string strMsg = "please check either one of the passed  or upcomming sessions buttons";
                string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                Response.Write(script);
                return;
            }

            if (Session["UserStatus"].ToString().Equals("Client"))
            {
                actor_title.InnerText = "Tutor";
                List<ClientBooking> BookingObj = null;
                peeps.InnerHtml = "";
                int status = -2;

                if (upcomming.Checked) // get upcomming
                {
                    BookingObj = ClientBookings.GetAllUpcmmingClientBookings(Session["ID"].ToString());
                    status = (int)TuberEnumerations.SessionBookingEnumeration.UPCOMMING;
                    if (BookingObj == null)
                    {
                        string strMsg = "You have no you haven't had anysessions Sessions";
                        string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                        Response.Write(script);
                        sessionsTable.Visible = false;
                        noSessions.Visible = true;
                        return;
                    }
                }
                else if (passed.Checked) // get concluded booked sessions
                {
                    BookingObj = ClientBookings.GetAllUpcmmingClientBookingsWithStatus(Session["ID"].ToString(), (int)TuberEnumerations.SessionBookingEnumeration.DONE);
                    status = (int)TuberEnumerations.SessionBookingEnumeration.DONE;
                    if (BookingObj == null)
                    {
                        string strMsg = "You have no upcomming Sessions";
                        string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                        Response.Write(script);
                        sessionsTable.Visible = false;
                        noSessions.Visible = true;
                        return;
                    }
                }
                else if (canceled.Checked) //get canceled 
                {
                    BookingObj = ClientBookings.GetAllUpcmmingClientBookingsWithStatus(Session["ID"].ToString(), (int)TuberEnumerations.SessionBookingEnumeration.CANCELED);
                    status = (int)TuberEnumerations.SessionBookingEnumeration.CANCELED;
                    if (BookingObj == null)
                    {
                        string strMsg = "You have no canceled Sessions";
                        string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                        Response.Write(script);
                        sessionsTable.Visible = false;
                        noSessions.Visible = true;
                        return;
                    }
                }
                //format the booked session if there are any
                if (BookingObj != null)
                {
                    int counter = 0;
                    foreach (ClientBooking clientbooking in BookingObj)
                    {
                        clientbooking.Date_Time = QuadCore_Website.models.NonDatabaseModels.DateFormatting.getCorrectDateTimeFormat(clientbooking.Date_Time);
                        DateTime sessionDateTime = DateTime.Parse(clientbooking.Date_Time);

                        User tutorDetails = UserFunctionality.GetTutorsUserTableDetails("" + clientbooking.Tutor_Table_Reference);
                        if (tutorDetails != null)
                        {
                            string session = "";
                            session += "<tr class='text-center'>";
                            session += "<td>" + (++counter) + "</td>";
                            session += "<td>" + tutorDetails.FullNames + " " + tutorDetails.Surname + "</td>";
                            session += setModuleName(clientbooking.BookingDetails_BookingRequestTable_Reference);
                            session += "<td>" + tutorDetails.Email_Address + "</td>";
                            session += "<td>" + sessionDateTime.Date + "</td>";
                            session += "<td>" + sessionDateTime.TimeOfDay + "</td>";
                            //Session location
                            string location = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/GetBookingrequestLocation/" + clientbooking.BookingDetails_BookingRequestTable_Reference);
                            session += "<td>" + location + "</td>";
                            session += "<td>&nbsp</td><td><a href='ViewSingleBooking.aspx?BooKingID=" + clientbooking.Id + "&status=" + status + "' class='btn btn-primary'>view session</a></td></tr>";
                            peeps.InnerHtml += session;
                            sessionsTable.Visible = true;
                            noSessions.Visible = false;
                        }
                    }
                }
                else
                {
                    string strMsg = "unkown error, please refreash the browser tab ";
                    string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                    Response.Write(script);
                }
            }
            else if (Session["UserStatus"].ToString().Equals("Tutor"))
            {
                actor_title.InnerText = "Student";
                int counter = 0;
                // get client information
                //get the booking                
                List<ClientBooking> BookingObj = ClientBookings.GetAllTutorBookings(Session["ID"].ToString());
                peeps.InnerHtml = "";
                int status = -2;
                if (upcomming.Checked) // get upcomming
                {
                    BookingObj = ClientBookings.GetAllTutorBookings(Session["ID"].ToString());
                    status = (int)TuberEnumerations.SessionBookingEnumeration.UPCOMMING;
                    if (BookingObj == null)
                    {
                        string strMsg = "You have no you haven't had anysessions Sessions";
                        string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                        Response.Write(script);
                        sessionsTable.Visible = false;
                        noSessions.Visible = true;
                        return;
                    }
                }
                else if (passed.Checked) // get concluded booked sessions
                {
                    BookingObj = ClientBookings.GetAllTutorBookingsWithStatus(Session["ID"].ToString(), (int)TuberEnumerations.SessionBookingEnumeration.DONE);
                    status = (int)TuberEnumerations.SessionBookingEnumeration.DONE;
                    if (BookingObj == null)
                    {
                        string strMsg = "You have no upcomming Sessions";
                        string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                        Response.Write(script);
                        sessionsTable.Visible = false;
                        noSessions.Visible = true;
                        return;
                    }
                }
                else if (canceled.Checked) //get canceled 
                {
                    BookingObj = ClientBookings.GetAllTutorBookingsWithStatus(Session["ID"].ToString(), (int)TuberEnumerations.SessionBookingEnumeration.CANCELED);
                    status = (int)TuberEnumerations.SessionBookingEnumeration.CANCELED;
                    if (BookingObj == null)
                    {
                        string strMsg = "You have no canceled Sessions";
                        string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                        Response.Write(script);
                        sessionsTable.Visible = false;
                        noSessions.Visible = true;
                        return;
                    }
                }

                if (BookingObj != null)
                {
                    foreach (ClientBooking clientbooking in BookingObj)
                    {
                        User clientDetails = UserFunctionality.GetClientsUserTableDetails("" + clientbooking.Client_Table_Reference);
                        if (clientDetails != null)
                        {
                            clientbooking.Date_Time = QuadCore_Website.models.NonDatabaseModels.DateFormatting.getCorrectDateTimeFormat(clientbooking.Date_Time);
                            DateTime sessionDateTime = DateTime.Parse(clientbooking.Date_Time);
                            string session = "";
                            session += "<tr class='text-center'>";
                            session += "<td>" + (++counter) + "</td>";
                            session += "<td>" + clientDetails.FullNames + " " + clientDetails.Surname + "</td>";
                            session += setModuleName(clientbooking.BookingDetails_BookingRequestTable_Reference);
                            session += "<td>" + clientDetails.Email_Address + "</td>";
                            session += "<td>" + sessionDateTime.Date + "</td>";
                            session += "<td>" + sessionDateTime.TimeOfDay + "</td>";
                            //Session location
                            string location = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/GetBookingrequestLocation/" + clientbooking.BookingDetails_BookingRequestTable_Reference);

                            session += "<td>" + location + "</td>";
                            session += "<td>&nbsp</td><td><a href='ViewSingleBooking.aspx?BooKingID=" + clientbooking.Id + "&status=" + status + "' class='btn btn-primary'>view session</a></td></tr>";
                            peeps.InnerHtml += session;
                            sessionsTable.Visible = true;
                            noSessions.Visible = false;
                        }
                    }
                }
                else
                {
                    string strMsg = "unkown error, please refreash the browser tab ";
                    string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                    Response.Write(script);
                }
            }
        }
    }


}