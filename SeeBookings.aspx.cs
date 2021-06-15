using Newtonsoft.Json;
using QuadCore_Website.HelperFunctionality;
using QuadCore_Website.models;
using QuadCore_Website.models.NonDatabaseModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuberAPI.models;

namespace QuadCore_Website
{
    public partial class SeeBookings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
            //Redirect to this current page  with additional url parameters
            if(Request.QueryString["action"] != null && HttpContext.Current.Session["ID"] != null)
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                string userId_ = HttpContext.Current.Session["ID"].ToString();
                switch (Request.QueryString["action"].ToString())
                {
                    case "accept":
                        {
                            // accept a session
                            string bodyResponse = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/changeBookingRequestAcceptedStatus/" + Request.QueryString["BookingRequestID"].ToString() + "/" + 1);
                            
                            //insert a new row into the Client booking table
                            string bookingRequestURI = SITEConstants.BASE_URL + "api/BookingRequest/GetClientsRequestDate/" + Request.QueryString["BookingRequestID"];
                            
                            string dateString =  HelperMethods.MakeDeserializable(ApiComnunication.getJsonEntities(bookingRequestURI));
                            dateString = HelperMethods.MakeDeserializable(dateString, '"');                            
                            string bookingRequestDate = null;
                            
                            bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(dateString);
                            string tutor_Table_Reference = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetTutorTableReferenceByForeignKey/" + userId_);

                            string client_Table_Reference = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/getClientIDByBookingRequestID/" + Request.QueryString["BookingRequestID"].ToString());

                            string newClientBooking_json = "{\"id\": 0,\"date_Time\": \"" + bookingRequestDate + "\",\"isActive\": 1,\"bookingDetails_BookingRequestTable_Reference\":" + Request.QueryString["BookingRequestID"] + ",\"tutor_Table_Reference\": " + tutor_Table_Reference + ",\"client_Table_Reference\":" + client_Table_Reference + "}";
                            string response = ApiComnunication.postJsonEntitie(SITEConstants.BASE_URL + "api/ClientBooking/AddClientBooking", newClientBooking_json);

                            //insert a new row into the Tutor booking table                   

                            //send notification to the client as a result                     
                            Event bookingEvent = new Event();
                            bookingEvent.EventType = "BookingFinalization";

                            //decription specify the description of the notification 
                            //elemets of the description,1) name of the module, 2) the date, 3) Booking request Id, 4) the The Tutors's User table reference
                            bookingEvent.Description = "Booking has been finalized with tutor_" + Request.QueryString["ModuleName"] + "_" + bookingRequestDate + "_" + Request.QueryString["BookingRequestID"] + "_" + HttpContext.Current.Session["ID"].ToString();

                            int eventId = Notification_Functionality.setEvent(bookingEvent);
                            if (eventId > 0)
                            {
                                Notification notification = new Notification();
                                notification.DatePosted = DateTime.Today.ToString();
                                notification.Time = DateTime.Now.TimeOfDay.ToString();
                                notification.Seen = 0;
                                notification.PersonTheNotificationConcerns = "Client";
                                notification.Event_Table_Reference = eventId;
                                notification.User_Table_Reference = Int32.Parse(ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Client/GetClientTableID/" + client_Table_Reference));
                                Notification_Functionality.setNotification(notification);

                                //change notification seen status                                                                                 
                                //Response.Redirect("Confirmed.aspx?ConfirmatioType=Booking_Finalization");
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('booking accepted');", true);
                            }
                       
                            else
                            {
                               //Todo Alert that the booking could not be finalized
                             }
                        }
                            break;
                        case "reject":
                            {
                            // reject application request and
                            string bodyResponse = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/changeBookingRequestAcceptedStatus/" + Request.QueryString["BookingRequestID"].ToString() + "/" + -1);

                            Event bookingEvent = new Event();

                            bookingEvent.EventType = "BookingRejection";

                            //Get My details
                            string client_Table_Reference = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/getClientIDByBookingRequestID/" + Request.QueryString["BookingRequestID"].ToString());
                            string clientUserTableID = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Client/GetClientTableID/" + client_Table_Reference);
                            string clientDetailsURI = SITEConstants.BASE_URL + "api/User/GetUser/" + clientUserTableID;
                            string clientJsonObj = ApiComnunication.getJsonEntities(clientDetailsURI);

                                if (clientJsonObj != null)
                                {
                                    //0) decription 1) Tutors user table ID 2) alternative date
                                    User clientObj = JsonConvert.DeserializeObject<User>(clientJsonObj);
                                    if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                                        bookingEvent.Description = "The Client: " + clientObj.FullNames + " has cancelled the booking for " + Request.QueryString["ModuleName"];
                                    else
                                        bookingEvent.Description = "The Tutor: " + ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/GetTutorName/" + userId_) + " has rejected the request for " + Request.QueryString["ModuleName"];

                                    bookingEvent.Description += "_" + Request.QueryString["ModuleName"] + "_" +
                                                                     HttpContext.Current.Session["ID"] + "_" + clientObj.Id;

                                    // set the booking request event and return its primary key
                                    int eventId = Notification_Functionality.setEvent(bookingEvent);
                                    if (eventId > 0)
                                    {
                                        Notification notification = new Notification();
                                        notification.DatePosted = DateTime.Today.ToString();
                                        notification.Time = DateTime.Now.TimeOfDay.ToString();
                                        notification.Seen = 0;
                                        notification.PersonTheNotificationConcerns = "Client";
                                        notification.User_Table_Reference = clientObj.Id;
                                        notification.Event_Table_Reference = eventId;

                                        if (Notification_Functionality.setNotification(notification) > 0)
                                        {
                                            initialDisplay();
                                         }
                                    }
                                    else
                                    {

                                    }
                                }
                            
                            }
                            break;
                    case "refreashPage":
                        {
                            // refereashpage application request and
                            initialDisplay();
                        }
                        break;
                    default:
                        {
                            initialDisplay();
                        }break;
                }
            }
            else
            {                
               initialDisplay();               
            }           
        }
         /// <summary>
         /// 
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        protected void Refreash_Click(object sender, EventArgs e)
        {
            Response.Redirect("SeeBookings.aspx?action=refreashPage");
        }
        /// <summary>
        /// 
        /// </summary>
        protected void initialDisplay()
        {

            if (HttpContext.Current.Session["UserStatus"] != null)
            {
                string userId_ = HttpContext.Current.Session["ID"].ToString();
                switch (HttpContext.Current.Session["UserStatus"])
                {
                    case "Client":
                        {
                            tutorOrClientHeading.InnerText = "Tutor Name";
                            //get the booking requests                             
                            string clientRequestsURL = SITEConstants.BASE_URL + "api/BookingRequest/GetBookingRequestByClientReference/" + userId_;
                            string bodyResponse = ApiComnunication.getJsonEntities(clientRequestsURL);
                            List<BookingRequest> bookingRequests = JsonConvert.DeserializeObject<List<BookingRequest>>(bodyResponse);

                            string tableRows = "";
                            foreach (BookingRequest bookingRequest in bookingRequests)
                            {
                                tableRows += "<tr>";
                                tableRows += "<td>" + ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Modules/getModuleNameByModuleID/" + (bookingRequest.ModuleID1)) + "</td>";
                                tableRows += "<td>&nbsp</td>";
                                tableRows += "<td>" + bookingRequest.RequestDate + "</td>";
                                tableRows += "<td>&nbsp</td>";
                                if (bookingRequest.Client_Reference != 0)
                                    tableRows += "<td>" + ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/GetTutornameByTutorTablePK/" + bookingRequest.Tutor_Reference) + "</td>";
                                tableRows += "<td>&nbsp</td>";

                                tableRows += "<td>&nbsp</td>";

                                if (bookingRequest.Is_Accepted == 1)
                                {
                                    tableRows += "<td>Accepted</td>";
                                    //acceptOrReject.Text = "Accept";
                                }
                                else if (bookingRequest.Is_Accepted == -1)
                                {
                                    tableRows += "<td>rejected</td>";

                                }
                                else
                                {
                                    tableRows += "<td>pending</td>";
                                    tableRows += "<td></td>";
                                }


                                tableRows += "</tr>";
                            }
                            RequestEntries.InnerHtml = tableRows;
                        }
                        break;
                    case "Tutor":
                        {
                            tutorOrClientHeading.InnerText = "Client Name";
                            //get the bookings   
                            string tutorRequestsURL = SITEConstants.BASE_URL + "api/BookingRequest/GetBookingRequestByTutorReference/" + userId_;
                            string bodyResponse = ApiComnunication.getJsonEntities(tutorRequestsURL);
                            List<BookingRequest> bookingRequests = JsonConvert.DeserializeObject<List<BookingRequest>>(bodyResponse);

                            //its client bookings becasue the reference to the tutor is stored also in the table
                            //List<ClientBooking> clientBookings = ClientBookings.GetAllTutorBookings(Session["ID"].ToString());

                            string tableRows = "";
                            foreach (BookingRequest bookingRequest in bookingRequests)
                            {
                                string modulename = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Modules/getModuleNameByModuleID/" + (bookingRequest.ModuleID1));
                                
                                tableRows += "<tr class='text-center'>";
                                if (bookingRequest.Client_Reference != 0)
                                    tableRows += "<td>" + ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/GetClientByClientTablePK/" + bookingRequest.Client_Reference) + "</td>";
                                else
                                    tableRows += "<td>&nbsp</td>";
                                tableRows += "<td>&nbsp</td>";
                                tableRows += "<td>" + modulename + "</td>";
                                tableRows += "<td>&nbsp</td>";
                                tableRows += "<td>" + bookingRequest.RequestDate + "</td>";
                                tableRows += "<td>&nbsp</td>";

                                if (bookingRequest.Is_Accepted == 1)
                                {
                                    tableRows += "<td class='text-success'>accepted</td>";
                                    tableRows += "<td>&nbsp</td>";
                                    tableRows += "<td class='text-primary'>no action</td>";
                                }
                                else if (bookingRequest.Is_Accepted == -1)
                                {
                                    tableRows += "<td class='text-danger'>rejected</td>";
                                    tableRows += "<td>&nbsp</td>";
                                    tableRows += "<td class='text-primary'>no action</td>";
                                }
                                else
                                {
                                    tableRows += "<td class='text-primary'>pending</td>";
                                    tableRows += "<td>&nbsp</td>";
                                    tableRows += "<td class='text-nowrap'><p style='display: inline-table'><a class='btn btn-primary' href='SeeBookings.aspx?action=accept&BookingRequestID=" + bookingRequest.Id + "&ModuleName=" + modulename + "'>accept</a>&nbsp;" +
                                            "<a class='btn btn-primary' href='SeeBookings.aspx?action=reject&BookingRequestID=" + bookingRequest.Id + "&ModuleName=" + modulename + "'>reject</a></p></td>";
                                }

                                tableRows += "</tr>";
                            }

                            RequestEntries.InnerHtml = tableRows;
                        }
                        break;
                }
            }
        }
    }
}