using Newtonsoft.Json;
using QuadCore_Website.HelperFunctionality;
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
    public partial class ShowNotificatioinDetails : System.Web.UI.Page
    {
        CultureInfo provider = CultureInfo.InvariantCulture;
        protected void Page_Load(object sender, EventArgs e)
        {
            // change notification status to seen 
            if (!IsPostBack)
            {

                if (HttpContext.Current.Session["Email"] != null)
                {

                    if (Request.QueryString["EventType"] != null && Request.QueryString["NotificationID"] != null)
                    {
                        switch (Request.QueryString["EventType"])
                        {
                            case "BookingRequest": //assess parameters for BookingRequest type notification
                                {
                                    if (Request.QueryString["BookingRequestID"] != null)
                                    {
                                        BookingRequestDiv.Visible = true;
                                        requestModule.InnerText = Request.QueryString["ModuleName"];

                                        //set seesion for storing Query string 
                                        string uril1 = Request.Url.AbsoluteUri;
                                        string[] qString = HelperMethods.separateString(uril1, '?');
                                        string uril2 = Request.QueryString.ToString();
                                        HttpContext.Current.Session["QueryString"] = qString[1];

                                        //get other details with the aid of the booking request Id passed through with the URL paramenter
                                        string bookingRequestURI = SITEConstants.BASE_URL + "api/BookingRequest/GetIndividualBookingRequest/" + Request.QueryString["BookingRequestID"];
                                        string clientDetailsURI = SITEConstants.BASE_URL + "api/User/GetUser/" + Request.QueryString["ClientsUserTableID"];

                                        string bookingRequestJsonObj = ApiComnunication.getJsonEntities(bookingRequestURI);
                                        string clientJsonObj = ApiComnunication.getJsonEntities(clientDetailsURI);

                                        if (bookingRequestJsonObj.Length > 0) //if the string is not empty
                                        {
                                            //Desirialise the Json body to a notification type object
                                            BookingRequest bookingRequestObj = JsonConvert.DeserializeObject<BookingRequest>(bookingRequestJsonObj);
                                            User clientObj = JsonConvert.DeserializeObject<User>(clientJsonObj);

                                            if (clientObj != null)
                                            {
                                                string bookingRequestDate = null;

                                                bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(bookingRequestObj.RequestDate);
                                                bookingRequestObj.EndTime = DateFormatting.getCorrectDateTimeFormat(bookingRequestObj.EndTime);
                                                string pSession = "" + (DateTime.Parse(bookingRequestDate) - DateTime.Parse(bookingRequestObj.EndTime)).Days;

                                                requestName.InnerText = clientObj.FullNames + " " + clientObj.Surname;
                                                requestDate.InnerText = bookingRequestObj.RequestDate;
                                                requestStartTime.InnerText = bookingRequestObj.RequestTime;
                                                requestEndTime.InnerText = bookingRequestObj.EndTime;
                                                requestNumber.InnerText = clientObj.Valid_Phone_Number;
                                                //requestLocation.InnerText = bookingRequestObj.ClientProposedLocation;
                                                requestLocationDIV.InnerHtml = "<label class=\"font-weight-bold\">Session Location:</label>";
                                                requestLocationDIV.InnerHtml += "<a href=\"LocationSelectionPage.aspx?" + qString[1] + "&RirectType=TutorMapViewBooking" + "&location=" + bookingRequestObj.ClientProposedLocation +
                                                                            "\" style=\"display: inline;\" id=\"requestLocation\" runat=\"server\">" + HelperMethods.separateString(bookingRequestObj.ClientProposedLocation, '_')[0] + "</a>";
                                                requestNPeriods.InnerText = "" + bookingRequestObj.Periods;
                                            }

                                            //makeNotificationUnseen();
                                        }
                                    }
                                }
                                break;
                            case "Session":
                                {
                                    Session.Visible = true;
                                    //makeNotificationUnseen();
                                }
                                break;
                            case "BookingFinalization":
                                {
                                    BookingFinalisationDetails.Visible = true;
                                    finalizationDescription.InnerText = Request.QueryString["Decription"];
                                    string clientDetailsURI = SITEConstants.BASE_URL + "api/User/GetUser/" + Request.QueryString["TutorUserTableID"];
                                    string clientJsonObj = ApiComnunication.getJsonEntities(clientDetailsURI);

                                    if (clientDetailsURI.Length > 0) //if the string is not empty
                                    {
                                        if (HttpContext.Current.Session["UserStatus"].Equals(SITEConstants.CLIENT_VAR))
                                            userFinalizationNameTitle.InnerText = "Tutor name";

                                        User clientObj = JsonConvert.DeserializeObject<User>(clientJsonObj);

                                        bookingFinalDate.InnerText = Request.QueryString["SessionDate"];
                                        bookingFinalModule.InnerText = Request.QueryString["ModuleName"];
                                        bookingFinalName.InnerText = clientObj.FullNames + " " + clientObj.Surname;
                                        makeNotificationUnseen();
                                    }

                                }
                                break;
                            case "BookingCancelation":
                                {
                                    BookingCancelation.Visible = true;

                                    makeNotificationUnseen();
                                }
                                break;
                            case "BookingRejection":
                                {
                                    BookingRejection.Visible = true;

                                    string bookingRequestURI = SITEConstants.BASE_URL + "api/BookingRequest/GetIndividualBookingRequest/" + Request.QueryString["BookingRequestID"];
                                    string bookingJson = ApiComnunication.getJsonEntities(bookingRequestURI);

                                    if (bookingJson.Length > 0)
                                    {
                                        BookingRequest booking = JsonConvert.DeserializeObject<BookingRequest>(bookingJson);
                                        rejectionModule.InnerText = Request.QueryString["ModuleName"];
                                        rejectionLocation.InnerText = booking.ClientProposedLocation;
                                        rejectionPeriods.InnerText = "" + booking.Periods;
                                        rejectionStart.InnerText = "" + booking.RequestTime;
                                        rejectionEnd.InnerText = "" + booking.EndTime;
                                        rejectionDate.InnerText = booking.RequestDate;
                                    }

                                    makeNotificationUnseen();
                                }
                                break;
                            case "BookingRenegotiation":
                                {
                                    RenegotiationDIV.Visible = true;
                                    renegotionView.Visible = true;

                                    string bookingRequestURI = SITEConstants.BASE_URL + "api/BookingRequest/GetIndividualBookingRequest/" + Request.QueryString["BookingRequestID"];
                                    string bookingRequestJsonObj = ApiComnunication.getJsonEntities(bookingRequestURI);

                                    if (bookingRequestJsonObj.Length > 0) //if the string is not empty
                                    {
                                        //Desirialise the Json body to a notification type object
                                        BookingRequest bookingRequestObj = JsonConvert.DeserializeObject<BookingRequest>(bookingRequestJsonObj);

                                        //option to choose booking to either negotiate or note
                                        string format = "dd/MM/yyyy HH:mm:ss";
                                        string bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(Request.QueryString["AltDate"]);

                                        string ps = "" + (DateTime.Parse(bookingRequestDate) - DateTime.Parse(Request.QueryString["EndTime"])).Days;
                                        string altDate = DateFormatting.getCorrectDateTimeFormat(Request.QueryString["AltDate"]);
                                        rvDate.InnerText = DateTime.Parse(altDate).Date.ToString();
                                        string endTime = DateFormatting.getCorrectDateTimeFormat(Request.QueryString["EndTime"]);
                                        rvETime.InnerText = DateTime.Parse(endTime).TimeOfDay.ToString();
                                        rvLocation.InnerText = bookingRequestObj.ClientProposedLocation;
                                        rvTLocation.InnerText = bookingRequestObj.tutorProposedLocation;
                                        //get the module name 
                                        string moduleName = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Modules/GetModuleName/" + bookingRequestObj.ModuleID1);
                                        rvModule.InnerText = moduleName;
                                        rvSTime.InnerText = DateTime.Parse(altDate).TimeOfDay.ToString();
                                        rvReason.InnerText = Request.QueryString["Reason"].ToString();
                                        rvPeriods.InnerText = "" + bookingRequestObj.Periods;

                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        NoDetailsMessage.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// changes the seen status of the notification currently being view by the user
        /// </summary>
        private void makeNotificationUnseen()
        {
            string notificationNumberUrl = SITEConstants.BASE_URL + "api/Notification/ChangeSeenStatus/" + Request.QueryString["NotificationID"];
            ApiComnunication.getJsonEntities(notificationNumberUrl);
        }

        /// <summary>
        /// Accept the booking upon seeing the request for the first time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AcceptBookingBtn_Click(object sender, EventArgs e)
        {

            string userId_ = HttpContext.Current.Session["ID"].ToString();

            // string bodyResponse = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/changeBookingRequestAcceptedStatus/" + Request.QueryString["BookingRequestID"].ToString() + "/" + 1);
            string tutor_Table_Reference = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetTutorTableReferenceByForeignKey/" + userId_);
            string client_Table_Reference = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Client/GetClientTablereferenceByForeignKey/" + Request.QueryString["ClientsUserTableID"]);
            string uri = SITEConstants.BASE_URL + "api/BookingRequest/changeBookingRequestAcceptedStatusCheckTutorAvailability/" + Request.QueryString["BookingRequestID"].ToString() + "/" + 1 + "/" + tutor_Table_Reference;
            string bodyResponse = ApiComnunication.getJsonEntities(uri);
            if (bodyResponse.Equals("-2"))
            {
                string alertText = "<h5>Sorry, Seems like you have a booking that is a too close this one</h5>";
                alert.InnerHtml = alertText;
                return; //exit method
            }
            else if (bodyResponse.Equals("1"))
            { //insert a new row into the tutor booking table
                if (BookingRequestFunctionality.FinalisesBooking(userId_, Request.QueryString["BookingRequestID"]))

                //Update Booking request Table with the tutor Id                
                {
                    //insert a new row into the Client booking table
                    string bookingRequestURI = SITEConstants.BASE_URL + "api/BookingRequest/GetClientsRequestDate/" + Request.QueryString["BookingRequestID"];

                    string sessionDateTime = ApiComnunication.getJsonEntities(bookingRequestURI);
                    sessionDateTime = DateFormatting.getCorrectDateTimeFormat(sessionDateTime);

                    // bookingRequestJsonObj.Replace("-", "/");//replace hyphen with the forward slash

                    string newClientBooking_json = "{\"id\": 0,\"date_Time\": \"" + sessionDateTime + "\",\"isActive\": 1,\"bookingDetails_BookingRequestTable_Reference\":" + Request.QueryString["BookingRequestID"] + ",\"tutor_Table_Reference\": " + tutor_Table_Reference + ",\"client_Table_Reference\":" + client_Table_Reference + "}";
                    string response = ApiComnunication.postJsonEntitie(SITEConstants.BASE_URL + "api/ClientBooking/AddClientBooking", newClientBooking_json);
                    //send notification to the client as a result                     
                    Event bookingEvent = new Event();
                    bookingEvent.EventType = "BookingFinalization";

                    //decription specify the description of the notification 
                    //elemets of the description,1) name of the module, 2) the date, 3) Booking request Id, 4) the The Tutors's User table reference
                    bookingEvent.Description = "Booking has been finalized with tutor_" + Request.QueryString["ModuleName"] + "_" + sessionDateTime + "_" + Request.QueryString["BookingRequestID"] + "_" + HttpContext.Current.Session["ID"].ToString();

                    int eventId = Notification_Functionality.setEvent(bookingEvent);
                    if (eventId > 0)
                    {
                        Notification notification = new Notification();
                        notification.DatePosted = DateTime.Today.ToString();
                        notification.Time = DateTime.Now.TimeOfDay.ToString();
                        notification.Seen = 0;
                        notification.PersonTheNotificationConcerns = "Client";
                        notification.Event_Table_Reference = eventId;
                        notification.User_Table_Reference = Int32.Parse(Request.QueryString["ClientsUserTableID"]);
                        Notification_Functionality.setNotification(notification);

                        //change notification seen status 
                        makeNotificationUnseen();
                        //Redirect
                        Response.Redirect("Confirmed.aspx?ConfirmatioType=Booking_Finalization");
                    }
                }
                else
                {
                    //Todo Alert that the booking could not be finalized
                }
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RejectClientBookingRequest_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["UserStatus"] != null && HttpContext.Current.Session["ID"].ToString() != null)
            {
                string userId_ = HttpContext.Current.Session["ID"].ToString();
                string bodyResponse = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/changeBookingRequestAcceptedStatus/" + Request.QueryString["BookingRequestID"].ToString() + "/" + -1);
                Event bookingEvent = new Event();

                bookingEvent.EventType = "BookingRejection";

                //Get My details
                string clientDetailsURI = SITEConstants.BASE_URL + "api/User/GetUser/" + Request.QueryString["ClientsUserTableID"];
                string clientJsonObj = ApiComnunication.getJsonEntities(clientDetailsURI);

                if (clientJsonObj != null)
                {
                    //0) decription 1) Tutors user table ID 2) alternative date
                    User clientObj = JsonConvert.DeserializeObject<User>(clientJsonObj);
                    if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                        bookingEvent.Description = "The Client: " + clientObj.FullNames + " has rejected the request for " + Request.QueryString["ModuleName"];
                    else
                        bookingEvent.Description = "The Tutor: " + ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/GetTutorName/" + userId_) + " has rejected the request for " + Request.QueryString["ModuleName"];

                    bookingEvent.Description += "_" + Request.QueryString["ModuleName"] + "_" +
                                                     HttpContext.Current.Session["ID"] + "_" + clientObj.Id; ;

                    // set the booking request event and return its primary key
                    int eventId = Notification_Functionality.setEvent(bookingEvent);
                    if (eventId > 0)
                    {
                        Notification notification = new Notification();
                        notification.DatePosted = DateTime.Today.ToString();
                        notification.Time = DateTime.Now.TimeOfDay.ToString();
                        notification.Seen = 0;
                        if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                            notification.PersonTheNotificationConcerns = "Tutor";
                        else
                            notification.PersonTheNotificationConcerns = "Client";
                        notification.User_Table_Reference = Int32.Parse(Request.QueryString["ClientsUserTableID"]);
                        notification.Event_Table_Reference = eventId;

                        if (Notification_Functionality.setNotification(notification) > 0)
                        {

                            if (Notification_Functionality.setNotification(notification) > 0)
                            {
                                makeNotificationUnseen();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Negotiate_BookingWithClient_Click(object sender, EventArgs e)
        {
            //send a notification to the client if their request hasn't be accepted yet
            frstRenegotiationDiv.Visible = true;
        }
        /// <summary>
		///  This methods Submits the negotiated booking and
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void SubmitNegotiate_BookingWithClient_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["UserStatus"] != null)
            {
                string proposedDate = (DateTime.Parse(date.Value + " " + time.Value)).ToString();
                string propsedTime = time.Value;
                string endTime = (DateTime.Parse(date.Value + " " + time.Value)).ToString();
                int periods = Int32.Parse(requestNPeriods.InnerText);
                for (int i = 0; i < periods; i++)
                {
                    //calculate the endTime
                    endTime = "" + DateTime.Parse(endTime).AddHours(1);
                }
                Event bookingEvent = new Event();

                bookingEvent.EventType = "BookingRenegotiation";

                //Get My details
                string clientDetailsURI = SITEConstants.BASE_URL + "api/User/GetUser/" + Request.QueryString["ClientsUserTableID"];
                clientDetailsURI = SITEConstants.BASE_URL + "api/User/GetUser/" + Request.QueryString["ClientsUserTableID"];
                string clientJsonObj = ApiComnunication.getJsonEntities(clientDetailsURI);

                if (clientJsonObj != null)
                {
                    //0) decription 1) Tutors user table ID 2) alternative date
                    User clientObj = JsonConvert.DeserializeObject<User>(clientJsonObj);
                    bookingEvent.Description = "The Tutor: " + clientObj.FullNames + " " +
                                                              clientObj.Surname + " you requested for " +
                                                              Request.QueryString["ModuleName"] + " is asking for a date change  _" +
                                                              HttpContext.Current.Session["ID"] + "_"
                                                              + proposedDate + "_" +
                                                              Request.QueryString["BookingRequestID"] + "_" +
                                                              Request.QueryString["ModuleName"] + "_" +
                                                              endTime + "_" +
                                                              reason.Value;
                    // set the booking request event and return its primary key
                    int eventId = Notification_Functionality.setEvent(bookingEvent);
                    if (eventId > 0)
                    {
                        Notification notification = new Notification();
                        notification.DatePosted = DateTime.Today.ToString();
                        notification.Time = DateTime.Now.TimeOfDay.ToString();
                        notification.Seen = 0;
                        notification.PersonTheNotificationConcerns = "Client";

                        notification.User_Table_Reference = Int32.Parse(Request.QueryString["ClientsUserTableID"]);
                        notification.Event_Table_Reference = eventId;

                        if (Notification_Functionality.setNotification(notification) > 0)
                        {
                            makeNotificationUnseen();
                            Response.Redirect("Confirmed.aspx?ConfirmatioType=BookingRenegotiation");
                        }
                    }
                    else
                    {

                    }
                }

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NoToBookingRequestNogotiationBtn_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["UserStatus"] != null && HttpContext.Current.Session["ID"].ToString() != null)
            {
                string userId_ = HttpContext.Current.Session["ID"].ToString();
                string bodyResponse = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/changeBookingRequestAcceptedStatus/" + Request.QueryString["BookingRequestID"].ToString() + "/" + -1);
                Event bookingEvent = new Event();

                bookingEvent.EventType = "BookingRejection";

                //Get My details
                string clientDetailsURI = SITEConstants.BASE_URL + "api/User/GetUser/" + Request.QueryString["OtherUserID"];
                string clientJsonObj = ApiComnunication.getJsonEntities(clientDetailsURI);

                if (clientJsonObj != null)
                {
                    //0) decription 1) Tutors user table ID 2) alternative date
                    User clientObj = JsonConvert.DeserializeObject<User>(clientJsonObj);
                    if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                        bookingEvent.Description = "The Client: " + ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/GetTutorName/" + userId_) + " has cancelled the booking for " + Request.QueryString["ModuleName"];
                    else
                        bookingEvent.Description = "The Tutor: " + ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/GetTutorName/" + userId_) + "has rejected the request for " + Request.QueryString["ModuleName"];

                    bookingEvent.Description += "_" + Request.QueryString["ModuleName"] + "_" +
                                                     HttpContext.Current.Session["ID"] + "_" + clientObj.Id; ;

                    // set the booking request event and return its primary key
                    int eventId = Notification_Functionality.setEvent(bookingEvent);
                    if (eventId > 0)
                    {
                        Notification notification = new Notification();
                        notification.DatePosted = DateTime.Today.ToString();
                        notification.Time = DateTime.Now.TimeOfDay.ToString();
                        notification.Seen = 0;
                        if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                            notification.PersonTheNotificationConcerns = "Tutor";
                        else
                            notification.PersonTheNotificationConcerns = "Client";
                        notification.User_Table_Reference = Int32.Parse(Request.QueryString["OtherUserID"]);
                        notification.Event_Table_Reference = eventId;

                        if (Notification_Functionality.setNotification(notification) > 0)
                        {
                            makeNotificationUnseen();
                            Response.Redirect("Confirmed.aspx?ConfirmatioType=BookingRejection&status=" + HttpContext.Current.Session["UserStatus"]);
                        }
                    }
                    else
                    {

                    }
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Negotiate_BookingTimeResponse_Click(object sender, EventArgs e)
        {
            RenegotiationControlls.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AcceptBookingRequestNegotiationBtn_Click(object sender, EventArgs e)
        {

            string userId_ = HttpContext.Current.Session["ID"].ToString();


            string tutor_Table_Reference = "";
            string client_Table_Reference = "";
            string uri = "";
            if (HttpContext.Current.Session["UserStatus"].Equals("Client")) //if current account belongs to client
            {
                tutor_Table_Reference = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetTutorTableReferenceByForeignKey/" + Request.QueryString["OtherUserID"]);
                client_Table_Reference = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Client/GetClientTablereferenceByForeignKey/" + userId_);
                uri = SITEConstants.BASE_URL + "api/BookingRequest/changeBookingRequestAcceptedStatusCheckCLientAvailability/" + Request.QueryString["BookingRequestID"].ToString() + "/" + 1 + "/" + client_Table_Reference;
            }
            else //if current account belongs to tutor
            {
                tutor_Table_Reference = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetTutorTableReferenceByForeignKey/" + userId_);
                client_Table_Reference = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Client/GetClientTablereferenceByForeignKey/" + Request.QueryString["OtherUserID"]);
                uri = SITEConstants.BASE_URL + "api/BookingRequest/changeBookingRequestAcceptedStatusCheckTutorAvailability/" + Request.QueryString["BookingRequestID"].ToString() + "/" + 1 + "/" + tutor_Table_Reference;

            }
            string bodyResponse = ApiComnunication.getJsonEntities(uri);
            if (bodyResponse.Equals("-2"))
            {
                string alertText = "<h5>Sorry, Seems like you have a booking that is a too close this one</h5>";
                alert.InnerHtml = alertText;
                return; //exit method
            }
            else if (bodyResponse.Equals("1"))
            {
                //insert a new row into the tutor booking table

                //Update Booking request Table with the tutor Id                
                if (BookingRequestFunctionality.FinalisesBooking(userId_, Request.QueryString["BookingRequestID"]))
                {
                    //insert a new row into the Client booking table
                    string strDate = DateFormatting.getCorrectDateTimeFormat(Request.QueryString["AltDate"]);
                    string finalDate = DateTime.Parse(strDate).ToString("MM/dd/yyyy HH:mm:ss");


                    string newClientBooking_json = "{\"id\": 0,\"date_Time\": \" " + finalDate + "\",\"isActive\": 1,\"bookingDetails_BookingRequestTable_Reference\":" + Request.QueryString["BookingRequestID"] + ",\"periods\": 0,\"tutor_Table_Reference\": " + tutor_Table_Reference + ",\"client_Table_Reference\":" + client_Table_Reference + "}";
                    string response = ApiComnunication.postJsonEntitie(SITEConstants.BASE_URL + "api/ClientBooking/AddClientBooking", newClientBooking_json);

                    //insert a new row into the Tutor booking table                   

                    //send notification to the client as a result                     
                    Event bookingEvent = new Event();
                    bookingEvent.EventType = "BookingFinalization";

                    //decription specify the description of the notification 
                    //elemets of the description,1) name of the module, 2) the date, 3) Booking request Id, 4) the The Tutors's User table reference
                    if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                        bookingEvent.Description += "Booking has been finalized with Client";
                    else
                        bookingEvent.Description += "Booking has been finalized with tutor";

                    bookingEvent.Description += "_" + Request.QueryString["ModuleName"] + "_" + finalDate + "_" + Request.QueryString["BookingRequestID"] + "_" + HttpContext.Current.Session["ID"].ToString();

                    //set the event
                    int eventId = Notification_Functionality.setEvent(bookingEvent);
                    if (eventId > 0)
                    {
                        Notification notification = new Notification();
                        notification.DatePosted = DateTime.Today.ToString();
                        notification.Time = DateTime.Now.TimeOfDay.ToString();
                        notification.Seen = 0;
                        if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                            notification.PersonTheNotificationConcerns = "Tutor";
                        else
                            notification.PersonTheNotificationConcerns = "Client";

                        notification.Event_Table_Reference = eventId;
                        notification.User_Table_Reference = Int32.Parse(Request.QueryString["OtherUserID"]);

                        if (Notification_Functionality.setNotification(notification) > 0)
                        {
                            //change notification seen status                                                 
                            makeNotificationUnseen();
                            //Redirect
                            Response.Redirect("Confirmed.aspx?ConfirmatioType=Booking_Finalization");
                        }
                    }
                }
                else
                {
                    //Todo Alert that the booking could not be finalized
                }
            }
            else
            {
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitAltNegotiate_BookingWithClient_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["UserStatus"] != null)
            {
                string proposedDate = (DateTime.Parse(date1.Value + " " + time1.Value)).ToString();
                string propsedTime = time1.Value;
                string endTime = (DateTime.Parse(date1.Value + " " + time1.Value)).ToString();

                int periods = Int32.Parse(_periods.Value);
                for (int i = 0; i < periods; i++)
                {
                    //calculate the endTime
                    endTime = "" + DateTime.Parse(endTime).AddHours(1);
                }

                Event bookingEvent = new Event();

                bookingEvent.EventType = "BookingRenegotiation";

                //Get My details
                string clientDetailsURI = SITEConstants.BASE_URL + "api/User/GetUser/" + HttpContext.Current.Session["ID"];
                string clientJsonObj = ApiComnunication.getJsonEntities(clientDetailsURI);

                //Request.QueryString["NotificationID"];

                if (clientJsonObj != null)
                {

                    //0) decription 1) Tutors user table ID 2) alternative date
                    User clientObj = JsonConvert.DeserializeObject<User>(clientJsonObj);
                    if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                        bookingEvent.Description = "The Client: ";
                    else
                        bookingEvent.Description = "The Tutor: ";

                    string brID = Request.QueryString["BookingRequestID"];
                    string modID = Request.QueryString["ModuleName"];

                    bookingEvent.Description += clientObj.FullNames + " " +
                                                clientObj.Surname + " you requested for " +
                                                modID + "is asking for a date change  _" +
                                                HttpContext.Current.Session["ID"] + "_" +
                                                proposedDate + "_" +
                                                brID + "_" +
                                                Request.QueryString["ModuleName"] + "_" +
                                                endTime + "_" +
                                                rrVReason.Value;

                    // set the booking request event and return its primary key
                    int eventId = Notification_Functionality.setEvent(bookingEvent);
                    if (eventId > 0)
                    {
                        Notification notification = new Notification();
                        notification.DatePosted = DateTime.Today.ToString();

                        notification.Time = DateTime.Now.TimeOfDay.ToString();
                        notification.Seen = 0;

                        if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
                            notification.PersonTheNotificationConcerns = "Client";
                        else
                            notification.PersonTheNotificationConcerns = "Tutor";

                        notification.User_Table_Reference = Int32.Parse(Request.QueryString["OtherUserID"]);
                        notification.Event_Table_Reference = eventId;

                        if (Notification_Functionality.setNotification(notification) > 0)
                        {
                            //change notification seen status                                                 
                            makeNotificationUnseen();
                            //Redirect
                            Response.Redirect("Confirmed.aspx?ConfirmatioType=BookingRenegotiation");
                        }
                    }
                    else
                    {
                    }
                }
            }
        }
    }
}