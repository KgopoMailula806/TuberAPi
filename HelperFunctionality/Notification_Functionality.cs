using Newtonsoft.Json;
using QuadCore_Website.models.NonDatabaseModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using TuberAPI.models;

namespace QuadCore_Website.HelperFunctionality
{
    public class Notification_Functionality
    {        
        public static int nNotifications = 0;
        static CultureInfo provider = CultureInfo.InvariantCulture;
        /// <summary>
        ///    
        /// </summary>
        /// <returns>a number that dictates the type of result the process reaches</returns>
        public static int setNotification(Notification notification)
        {
            string bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(notification.DatePosted);
            string JsonNotification = "{\"id\":0,\"seen\":" + notification.Seen + " ,\"datePosted\": \"" + bookingRequestDate + "\",\"time\": \"" + notification.Time + "\",\"user_Table_Reference\":" + notification.User_Table_Reference + ",\"event_Table_Reference\":" + notification.Event_Table_Reference + ",\"PersonTheNotificationConcerns\":\"" + notification.PersonTheNotificationConcerns + "\"}";
            string pushNotificationUri = SITEConstants.BASE_URL+"api/Notification/pushnotification";
            string JsonString = ApiComnunication.postJsonEntitie(pushNotificationUri, JsonNotification);                        
            if(!JsonString.Equals("0"))
            {
                return Int32.Parse(JsonString);
            }else            
                return -1;
        }

        /// <summary>
        /// record the event that has occured and returns its primary key 
        /// </summary>
        /// <returns>a number that dictates the type of result the process reaches</returns>
        public static int setEvent(Event _event)
        {
            string JsonEvent = "{\"id\": 0, \"description\":\"" + _event.Description+"\",\"eventType\":\""+ _event.EventType + "\"}";
            string pushEventUri = SITEConstants.BASE_URL+"api/Event/pushEvent";
            string JsonStringUserObject = ApiComnunication.postJsonEntitie(pushEventUri, JsonEvent);                        

            if(!JsonStringUserObject.Equals("0")){
                return Int32.Parse(JsonStringUserObject);
            }
            //set the event notification
            return -1;
        }

        /// <summary>
        /// Method retreives notifications according the the user ID
        /// and  
        /// </summary>
        /// <returns></returns>
        public static string getNotifications(String Id)
        {
            int numNotifications = 0;
            string notificationHtml = "";
            string NotificationUri = SITEConstants.BASE_URL + "api/Notification/popUsersUnseenNotifications/" + Id;
            string JsonNotificationBody = ApiComnunication.getJsonEntities(NotificationUri);
            List<Notification> NotificationObj = JsonConvert.DeserializeObject<List<Notification>>(JsonNotificationBody);
            if (NotificationObj != null)
            {
                foreach (Notification nottficationData in NotificationObj)
                {
                    //get the event data
                    string JsonEventBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Event/GetEvent/" + nottficationData.Event_Table_Reference);
                    Event notificatioEvent = JsonConvert.DeserializeObject<Event>(HelperMethods.MakeDeserializable(JsonEventBody));
                    if (notificatioEvent == null)                  
                        continue;
                    
                    //a loop will run from here
                    // check for event ID from given Notification object
                    string eventType = notificatioEvent.EventType;
                    string eventID = "" + notificatioEvent.ID;
                    string noticationDescription = notificatioEvent.Description;

                    string bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(nottficationData.DatePosted);
                   

                    switch (eventType)
                    {
                        case "BookingRequest":                            
                            {
                                //if this concerns a tutor                                
                                string[] serparatedDescriptionText = HelperMethods.separateString(notificatioEvent.Description);
                                //if the tutor is registered to tutor the Subjects                                
                                    notificationHtml += getFormatBookingRequestNotification(notificatioEvent, nottficationData);
                                    numNotifications++;                                                                
                            }
                            break;
                        case "Session":
                            {
                                notificationHtml += getFormatSessionNotification(notificatioEvent, nottficationData);
                                numNotifications++;
                            }
                            break;
                        case "BookingFinalization":
                            {
                                //TODO Fomating for Booking finalization notification
                                notificationHtml += getFormatBookingFinalizationNotification(notificatioEvent, nottficationData);
                                numNotifications++;
                            }
                            break;
                        case "BookingCancelation":
                            {
                                notificationHtml += getFormatBookingCancelationNotification(notificatioEvent, nottficationData);
                                numNotifications++;
                            }
                            break;
                        case "BookingRenegotiation":
                            {
                                notificationHtml += getFormatBookingRenegotiationNotification(notificatioEvent, nottficationData);
                                numNotifications++;
                            }
                            break; 
                            case "BookingRejection":
                            {
                                notificationHtml += getFormatBookingRejectionNotification(notificatioEvent, nottficationData);
                                numNotifications++;
                            }
                            break;
                        case "InterviewMeeting":
                            {
                                notificationHtml += getFormatInterviewMeetingNotification(notificatioEvent, nottficationData);
                                numNotifications++;
                            }
                            break;
                    }

                    //notificationHtml = "<a class='dropdown-item noti_item' href='NotificationConfig.aspx?ID=" + eventType + "_" + eventID + "'>";
                }
            }
            
            //loop will end here
            return notificationHtml;
        }

        internal static string getNotifications(string Id, TimeRange timeRange, string selectedValue, int seenFlag)
        {

                int numNotifications = 0;
                string notificationHtml = "";
                string NotificationUri = SITEConstants.BASE_URL + "api/Notification/popUsersUnseenNotificationsTimeRange/" + Id + "/" + selectedValue + "/" + seenFlag;
                string jsonBody = "{"+
                                    "\"laterDate\": \""+timeRange.laterDate + "\","+
                                    "\"earlierDate\": \"" + timeRange.earlierDate + "\"" +
                                   "}";
                string JsonNotificationBody = ApiComnunication.postJsonEntitie(NotificationUri, jsonBody);
                if (JsonNotificationBody.Equals("0"))
                    return "";
                List<Notification> NotificationObj = JsonConvert.DeserializeObject<List<Notification>>(JsonNotificationBody);
                if (NotificationObj != null)
                {
                    foreach (Notification nottficationData in NotificationObj)
                    {
                        //get the event data
                        string JsonEventBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Event/GetEvent/" + nottficationData.Event_Table_Reference);
                        Event notificatioEvent = JsonConvert.DeserializeObject<Event>(HelperMethods.MakeDeserializable(JsonEventBody));
                        if (notificatioEvent == null)
                            continue;

                        //a loop will run from here
                        // check for event ID from given Notification object
                        string eventType = notificatioEvent.EventType;
                        string eventID = "" + notificatioEvent.ID;
                        string noticationDescription = notificatioEvent.Description;

                        nottficationData.DatePosted = DateFormatting.getCorrectDateTimeFormat(nottficationData.DatePosted);
                        DateTime time = Convert.ToDateTime(nottficationData.DatePosted);

                        switch (eventType)
                        {
                            case "BookingRequest":
                                {
                                    //if this concerns a tutor                                
                                    string[] serparatedDescriptionText = HelperMethods.separateString(notificatioEvent.Description);
                                    //if the tutor is registered to tutor the Subjects                                
                                    notificationHtml += getFormatBookingRequestNotification(notificatioEvent, nottficationData);
                                    numNotifications++;
                                }
                                break;
                            case "Session":
                                {
                                    notificationHtml += getFormatSessionNotification(notificatioEvent, nottficationData);
                                    numNotifications++;
                                }
                                break;
                            case "BookingFinalization":
                                {
                                    //TODO Fomating for Booking finalization notification
                                    notificationHtml += getFormatBookingFinalizationNotification(notificatioEvent, nottficationData);
                                    numNotifications++;
                                }
                                break;
                            case "BookingCancelation":
                                {
                                    notificationHtml += getFormatBookingCancelationNotification(notificatioEvent, nottficationData);
                                    numNotifications++;
                                }
                                break;
                            case "BookingRenegotiation":
                                {
                                    notificationHtml += getFormatBookingRenegotiationNotification(notificatioEvent, nottficationData);
                                    numNotifications++;
                                }
                                break;
                            case "BookingRejection":
                                {
                                    notificationHtml += getFormatBookingRejectionNotification(notificatioEvent, nottficationData);
                                    numNotifications++;
                                }
                                break;
                            case "InterviewMeeting":
                                {
                                    notificationHtml += getFormatInterviewMeetingNotification(notificatioEvent, nottficationData);
                                    numNotifications++;
                                }
                                break;
                        }

                        //notificationHtml = "<a class='dropdown-item noti_item' href='NotificationConfig.aspx?ID=" + eventType + "_" + eventID + "'>";
                    }
                }

                //loop will end here
                return notificationHtml;
            
        }

        public static int getNumNotificatons(string Id)
        {
            string notificationNumberUrl = SITEConstants.BASE_URL + "api/Notification/getNumberOfUnreadNotifications/" + Id;
            return Int32.Parse(ApiComnunication.getJsonEntities(notificationNumberUrl));
        }
      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDescriminator"></param>
        /// <returns></returns>
        public static string CheckGeneralNotifications(string userDescriminator, string TutorID)
        {
            string notificationHtml = "";
            int numNotifications = 0;
            //get General notifications
            string generalNotificationUri = SITEConstants.BASE_URL + "api/Notification/popUsersGenerallyUnseenNotifications/" + 0;
            string JsonNotificationBody = ApiComnunication.getJsonEntities(generalNotificationUri);
            List<Notification> NotificationObj = JsonConvert.DeserializeObject<List<Notification>>(JsonNotificationBody);
            if (NotificationObj != null)
            {
                foreach (Notification nottficationData in NotificationObj)
                {

                    //get the event data
                    string JsonEventBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Event/GetEvent/" + nottficationData.Event_Table_Reference);
                    Event notificatioEvent = JsonConvert.DeserializeObject<Event>(HelperMethods.MakeDeserializable(JsonEventBody));

                    //a loop will run from here
                    // check for event ID from given Notification object
                    string eventType = notificatioEvent.EventType;
                    string eventID = "" + notificatioEvent.ID;
                    string noticationDescription = notificatioEvent.Description;

                   // DateTime time = Convert.ToDateTime(nottficationData.DatePosted);
                    
                    switch (eventType)
                    {
                        case "BookingRequest":
                            {
                                //if this concerns a tutor
                                if(nottficationData.PersonTheNotificationConcerns.Equals(userDescriminator)) {
                                
                                    string[] serparatedDescriptionText = HelperMethods.separateString(notificatioEvent.Description);
                                    //if the tutor is registered to tutor the Subjects
                                    if (ModuleFunctionality.IsTutorRegisteredForThisModule(serparatedDescriptionText[1],TutorID)) 
                                    {
                                        notificationHtml += getFormatBookingRequestNotification(notificatioEvent, nottficationData);
                                        numNotifications++;
                                    }                                    
                                }                             
                            }
                            break;
                        case "Session":
                            {
                                notificationHtml += getFormatSessionNotification(notificatioEvent, nottficationData);
                                numNotifications++;
                            }
                            break;
                    }                                 
                }
            }

            //notificationHtml += "<h6 class='noti_tittle'>" + nNotifications.ToString() + " New  Notifications</h6>"; 
            
            return notificationHtml;
        }

        /// <summary>
        ///  Todo: Needs work
        /// </summary>
        /// <param name="notificatioEvent"></param>
        /// <returns>Html text that is specillized form Session Notifications</returns>
        private static string getFormatSessionNotification(Event notificatioEvent, Notification nottficationData)
        {
            string notificationHtml = "";
            string bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(nottficationData.DatePosted);
            string[] serparatedDescriptionText = HelperMethods.separateString(notificatioEvent.Description);

            DateTime time;
            try
            {
                time = DateTime.Parse(bookingRequestDate);
            }
            catch (Exception ec)
            {
                time = ((Convert.ToDateTime(bookingRequestDate).AddMinutes(DateTime.Parse(nottficationData.Time).Minute)).AddHours(DateTime.Parse(nottficationData.Time).Hour).AddSeconds(DateTime.Parse(nottficationData.Time).Second));
            }
            notificationHtml = "<a onmouseover='orangeBG(this)' onmouseout='whiteBG(this)' class='dropdown-item noti_item' href='ShowNotificatioinDetails.aspx?Decription=" + serparatedDescriptionText[0] +
                                                                                                        "&ModuleName=" + serparatedDescriptionText[1] +
                                                                                                        "&EventType=" + notificatioEvent.EventType +
                                                                                                        "&ModuleID=" + serparatedDescriptionText[2] +
                                                                                                        "&BookingRequestID=" + serparatedDescriptionText[3] +
                                                                                                        "&ClientsUserTableID=" + serparatedDescriptionText[4] +
                                                                                                        "&NotificationID=" + nottficationData.ID + "'>";

            notificationHtml += formatGenericAncor(notificatioEvent.EventType,
                                        time,
                                        nottficationData.User_Table_Reference,
                                        serparatedDescriptionText[0],
                                        serparatedDescriptionText[1]);
            return notificationHtml;
        }

        /// <summary>
        ///  For the notification discription text for the 
        /// </summary>
        /// <param name="notificatioEvent"></param>
        /// <returns></returns>
        private static string getFormatBookingRequestNotification(Event notificatioEvent, Notification nottficationData)
        {            
            string notificationHtml = "";
            string bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(nottficationData.DatePosted);
            DateTime time;
            try
            {
                time = DateTime.Parse(bookingRequestDate);
            }
            catch (Exception ec)
            {
                time = ((Convert.ToDateTime(bookingRequestDate).AddMinutes(DateTime.Parse(nottficationData.Time).Minute)).AddHours(DateTime.Parse(nottficationData.Time).Hour).AddSeconds(DateTime.Parse(nottficationData.Time).Second));
            }
            // The description has to be separated because it also contains the 0) The event description, 1) Module name, 2) BookingRequest Id , and 3) the clients ID
            string[] serparatedDescriptionText = HelperMethods.separateString(notificatioEvent.Description);

            //this achor redirects to a page that give the details of the notification
            if (serparatedDescriptionText.Length < 1)
                return "";
            try {
                

                notificationHtml = "<a onmouseover='orangeBG(this)' onmouseout='whiteBG(this)' class='dropdown-item' href='ShowNotificatioinDetails.aspx?Decription=" + serparatedDescriptionText[0] + 
                                                                                                        "&ModuleName=" + serparatedDescriptionText[1] +
                                                                                                        "&EventType=" + notificatioEvent.EventType +
                                                                                                        "&ModuleID=" + serparatedDescriptionText[2] +
                                                                                                        "&BookingRequestID=" + serparatedDescriptionText[3] + 
                                                                                                        "&ClientsUserTableID="+ serparatedDescriptionText[4]+
                                                                                                        "&NotificationID=" + nottficationData.ID+"'>";

                notificationHtml += formatGenericAncor(notificatioEvent.EventType,
                                        time,
                                        nottficationData.User_Table_Reference,
                                        serparatedDescriptionText[0],
                                        serparatedDescriptionText[1]);

            }
            catch (IndexOutOfRangeException)
            {
                notificationHtml = "<a class='dropdown-item' href='ShowNotificatioinDetails.aspx?Decription=" + serparatedDescriptionText[0] + "'>";
                notificationHtml += "<p class='noti_text'>" + serparatedDescriptionText[0] + ": " + serparatedDescriptionText[1] + "</p>";
                notificationHtml += "<span class='noti_time'>" + "</span></a>";
            }
            return notificationHtml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificatioEvent"></param>
        /// <param name="nottficationData"></param>
        /// <returns></returns>
        private static string getFormatBookingCancelationNotification(Event notificatioEvent, Notification nottficationData)
        {

            string notificationHtml = "";
            string bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(nottficationData.DatePosted);
            DateTime time;
            try
            {
                time = DateTime.Parse(bookingRequestDate);
            }
            catch (Exception ec)
            {
                time = ((Convert.ToDateTime(bookingRequestDate).AddMinutes(DateTime.Parse(nottficationData.Time).Minute)).AddHours(DateTime.Parse(nottficationData.Time).Hour).AddSeconds(DateTime.Parse(nottficationData.Time).Second));
            }
            // The description has to be sepparated becasue it also contains the 0) The event description, 1) BookingRequest Id
            string[] serparatedDescriptionText = HelperMethods.separateString(notificatioEvent.Description);

            //this achor redirects to a page that give the details of the notification
            if (serparatedDescriptionText.Length < 1)
                return "";

            notificationHtml = "<a onmouseover='orangeBG(this)' onmouseout='whiteBG(this)' class='dropdown-item' href='ShowNotificatioinDetails.aspx?Decription=" + serparatedDescriptionText[0] +
                                                                                                          "&NotificationID=" + nottficationData.ID +      
                                                                                                          "&EventType=" + notificatioEvent.EventType + "'>";

            notificationHtml += formatGenericAncor(notificatioEvent.EventType,
                                        time,
                                        nottficationData.User_Table_Reference,
                                        serparatedDescriptionText[0],
                                        serparatedDescriptionText[1]);

            return notificationHtml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificatioEvent"></param>
        /// <param name="nottficationData"></param>
        /// <returns></returns>
        private static string getFormatBookingFinalizationNotification(Event notificatioEvent, Notification nottficationData)
        {

            string notificationHtml = "";
            string bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(nottficationData.DatePosted);
            DateTime time;
            try
            {
                time = DateTime.Parse(bookingRequestDate);
            }
            catch(Exception ec)
            {
                time = ((Convert.ToDateTime(bookingRequestDate).AddMinutes(DateTime.Parse(nottficationData.Time).Minute)).AddHours(DateTime.Parse(nottficationData.Time).Hour).AddSeconds(DateTime.Parse(nottficationData.Time).Second));
            }

            // The description has to be sepparated becasue it also contains the 0) The event description, 1) BookingRequest Id             
            char[] seperator = {'_' };
            string[] serparatedDescriptionText = notificatioEvent.Description.Split(seperator);
            //this achor redirects to a page that give the details of the notification
            if (serparatedDescriptionText.Length < 1)
                return "";
            //elemets of the description,1) name of the module, 2) the date, 3) Booking request Id, 4) the The Tutors's User table reference
            //Request.QueryString["ModuleName"] + "_" + bookingRequestJsonObj + "_" + Request.QueryString["BookingRequestID"] + "_" + Session["ID"].ToString();
            try
            {
                notificationHtml = "<a onmouseover='orangeBG(this)' onmouseout='whiteBG(this)' class='dropdown-item' href='ShowNotificatioinDetails.aspx?Decription=" + serparatedDescriptionText[0] +
                                                                                                          "&EventType="+ notificatioEvent.EventType +
                                                                                                          "&ModuleName=" + serparatedDescriptionText[1] +
                                                                                                          "&SessionDate=" + serparatedDescriptionText[2] +
                                                                                                          "&NotificationID=" + nottficationData.ID +
                                                                                                          "&BookingRequestID=" + serparatedDescriptionText[3] +
                                                                                                          "&TutorUserTableID=" + serparatedDescriptionText[4] +
                                                                                                          "'>";

                notificationHtml += formatGenericAncor(notificatioEvent.EventType,
                                        time,
                                        nottficationData.User_Table_Reference,
                                        serparatedDescriptionText[0],
                                        serparatedDescriptionText[1]);
                   
            }
            catch (IndexOutOfRangeException )
            {
                notificationHtml = "<a class='dropdown-item' href='ShowNotificatioinDetails.aspx?Decription=" + serparatedDescriptionText[0] + "'>";
                notificationHtml += "<p class='noti_text'>" + serparatedDescriptionText[0] + ": " + "</p>";
                notificationHtml += "<span class='noti_time'>"  + "</span></a>";
            }
                      
            //time passed since notification was generated          
            return notificationHtml;
        }

        /// <summary>
        /// Specifies the notification for a booking request date change
        /// </summary>
        /// <param name="notificatioEvent"></param>
        /// <param name="nottficationData"></param>
        /// <returns></returns>
        private static string getFormatBookingRenegotiationNotification(Event notificatioEvent, Notification nottficationData)
        {

            string notificationHtml = "";            
            string bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(nottficationData.DatePosted);
            DateTime time;
            try
            {
                time = DateTime.Parse(bookingRequestDate);
            }
            catch(Exception ec)
            {
                time = ((Convert.ToDateTime(bookingRequestDate).AddMinutes(DateTime.Parse(nottficationData.Time).Minute)).AddHours(DateTime.Parse(nottficationData.Time).Hour).AddSeconds(DateTime.Parse(nottficationData.Time).Second));
            }

            

            // The description has to be sepparated becasue it also contains the 0) decription 1) Tutors user table ID 2) alternative date
            char[] seperator = { '_' };
            string[] serparatedDescriptionText = notificatioEvent.Description.Split(seperator);
            //this achor redirects to a page that give the details of the notification
            if (serparatedDescriptionText.Length < 1)
                return "";
            //0) decription 1) Tutors user table ID 2) alternative date           
            try
            {
                notificationHtml = "<a onmouseover='orangeBG(this)' onmouseout='whiteBG(this)' class='dropdown-item' href='ShowNotificatioinDetails.aspx?Decription=" + serparatedDescriptionText[0] +
                                                                                                          "&EventType=" + notificatioEvent.EventType +                                                                                                          
                                                                                                          "&OtherUserID=" + serparatedDescriptionText[1] +
                                                                                                          "&AltDate=" + serparatedDescriptionText[2] +
                                                                                                          "&NotificationID=" + nottficationData.ID +
                                                                                                          "&BookingRequestID=" + serparatedDescriptionText[3] +
                                                                                                          "&ModuleName=" + serparatedDescriptionText[4] +
                                                                                                          "&EndTime=" + serparatedDescriptionText[5] +
                                                                                                          "&Reason=" + serparatedDescriptionText[6] +
                                                                                                          "'>";

                notificationHtml += formatGenericAncor(notificatioEvent.EventType,
                                         time,
                                         nottficationData.User_Table_Reference,
                                         serparatedDescriptionText[0],
                                         serparatedDescriptionText[1]);
            }
            catch (IndexOutOfRangeException)
            {
                notificationHtml = "<a class='dropdown-item' href='ShowNotificatioinDetails.aspx?Decription=" + serparatedDescriptionText[0] + "'>";
                notificationHtml += "<p class='noti_text'>" + serparatedDescriptionText[0] + ": " + serparatedDescriptionText[1] + "</p>";
                notificationHtml += "<span class='noti_time'>" + "</span></a>";
            }

            //time passed since notification was generated          
            return notificationHtml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificatioEvent"></param>
        /// <param name="nottficationData"></param>
        /// <returns></returns>
        private static string getFormatBookingRejectionNotification(Event notificatioEvent, Notification nottficationData)
        {
            string notificationHtml = "";
            string bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(nottficationData.DatePosted);
            DateTime time;
            try
            {
                time = DateTime.Parse(bookingRequestDate);
            }
            catch (Exception ec)
            {
                time = ((Convert.ToDateTime(bookingRequestDate).AddMinutes(DateTime.Parse(nottficationData.Time).Minute)).AddHours(DateTime.Parse(nottficationData.Time).Hour).AddSeconds(DateTime.Parse(nottficationData.Time).Second));
            }

            // The description has to be sepparated becasue it also contains the 0) decription 1) Tutors user table ID 2) alternative date
            char[] seperator = {'_' };
            string[] serparatedDescriptionText = notificatioEvent.Description.Split(seperator);
            //this achor redirects to a page that give the details of the notification
            if (serparatedDescriptionText.Length < 1)
                return "";
            //0) decription 1) Tutors user table ID 2) alternative date
            string notificationNumberUrl = SITEConstants.BASE_URL + "api/Notification/ChangeSeenStatus/" + nottficationData.ID;
            //ApiComnunication.getJsonEntities(notificationNumberUrl);
            try
            {
                notificationHtml = "<a onmouseover='orangeBG(this)' onmouseout='whiteBG(this)' class='dropdown-item' href='ShowNotificatioinDetails.aspx?Decription=" + serparatedDescriptionText[0] +
                                                                                                          "&ModuleName=" + serparatedDescriptionText[1] +
                                                                                                          "&EventType=" + notificatioEvent.EventType +                                                                                                                                                                                                                   
                                                                                                          "&NotificationID=" + nottficationData.ID +
                                                                                                          "&OtherUserID=" + serparatedDescriptionText[2] +
                                                                                                          "&BookingRequestID=" + serparatedDescriptionText[3] +                                                                                                                                                                                                                 
                                                                                                          "'>";
                notificationHtml += formatGenericAncor(notificatioEvent.EventType,
                                        time,
                                        nottficationData.User_Table_Reference,
                                        serparatedDescriptionText[0],
                                        serparatedDescriptionText[1]);

            }
            catch (IndexOutOfRangeException)
            {
                
                notificationHtml = "<p class='noti_text'> An error occured when trying to process this notification</p>";
                
            }            
            //time passed since notification was generated          
            return notificationHtml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificatioEvent"></param>
        /// <param name="nottficationData"></param>
        /// <returns></returns>
        private static string getFormatInterviewMeetingNotification(Event notificatioEvent, Notification nottficationData)
        {
            string notificationHtml = "";
            string bookingRequestDate = DateFormatting.getCorrectDateTimeFormat(nottficationData.DatePosted);
            DateTime time;
            try
            {
                time = DateTime.Parse(bookingRequestDate);
            }
            catch (Exception ec)
            {
                time = ((Convert.ToDateTime(bookingRequestDate).AddMinutes(DateTime.Parse(nottficationData.Time).Minute)).AddHours(DateTime.Parse(nottficationData.Time).Hour).AddSeconds(DateTime.Parse(nottficationData.Time).Second));
            }
            //elemets of the description,1) time, 2) the date, 3) user table Id
            char[] seperator = { '_' };
            string[] serparatedDescriptionText = notificatioEvent.Description.Split(seperator);
            if (serparatedDescriptionText.Length < 1)
                return "";
            try
            {
                notificationHtml = "<a onmouseover='orangeBG(this)' onmouseout='whiteBG(this)' class='dropdown-item' href='ShowNotificatioinDetails.aspx?Decription=" + serparatedDescriptionText[0] +
                                                                                                          "&Time=" + serparatedDescriptionText[1] +
                                                                                                          "&EventType=" + notificatioEvent.EventType +
                                                                                                          "&NotificationID=" + nottficationData.ID +
                                                                                                          "&Date=" + serparatedDescriptionText[2] +
                                                                                                          "&UserTableID=" + serparatedDescriptionText[3] +
                                                                                                          "'>";
                notificationHtml += formatGenericAncor(notificatioEvent.EventType,
                                        time,
                                        nottficationData.User_Table_Reference,
                                        serparatedDescriptionText[0],
                                        serparatedDescriptionText[1]);

            }
            catch (IndexOutOfRangeException)
            {

                notificationHtml = "<p class='noti_text'> An error occured when trying to process this notification</p>";

            }
            return notificationHtml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeDescription"></param>
        /// <param name="time"></param>
        private static string addTimeAgoToDescription2(DateTime time)
        {
            string timeDescription = "";
            if(Math.Abs((DateTime.Now - time).Days) > 1)
            {
                timeDescription = Math.Abs((DateTime.Now - time).Days) + " days ago";
            }
            else if (Math.Abs((DateTime.Now - time).Hours) < 24)
            {
                if (Math.Abs((DateTime.Now - time).Minutes) < 60)
                {
                    timeDescription = Math.Abs((DateTime.Now - time).Hours) + " hours and "+Math.Abs((DateTime.Now - time).Minutes) + " minutes ago";
                }else
                    timeDescription = Math.Abs((DateTime.Now - time).Hours) + " hours ago";
            }
            else if (Math.Abs((DateTime.Now - time).Minutes) < 60)
            {
                if (Math.Abs((DateTime.Now - time).Seconds) < 60)
                {
                    timeDescription = (DateTime.Now - time).Minutes + " minutes and " + Math.Abs((DateTime.Now - time).Seconds) + " seconds ago";
                }else
                timeDescription = Math.Abs((DateTime.Now - time).Minutes) + " minutes ago";
            }
            else if (Math.Abs((DateTime.Now - time).Seconds) < 60)
            {
                timeDescription = Math.Abs((DateTime.Now - time).Seconds) + " seconds ago";
            }
            else            
            {
                timeDescription = "time error";
            }
            return timeDescription; 

        }
        private static string addTimeAgoToDescription(DateTime time)
        {
            string timeDescription = "";

            timeDescription = Math.Abs((DateTime.Now - time).Days) + " days " + 
                Math.Abs((DateTime.Now - time).Hours) + " hours and "+
                Math.Abs((DateTime.Now - time).Minutes) + "minutes ago";
            
            return timeDescription;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        
        private static string formatGenericAncor(string NotificationType,
                                                DateTime time,
                                                int User_Table_Reference,
                                                string DescriptionText,
                                                string otherProperty)
        {
            string notificationHtml = "";

            //format the time of the requests 
            string timeDescription = addTimeAgoToDescription(time);

            //specifying that it is a special request
            if (User_Table_Reference != 0)
                notificationHtml += "<div><div class='dropdown-item-text'>" + "Special: " + NotificationType + "</div>";
            else
                notificationHtml += "<div><div class='dropdown-item-text'>" + NotificationType + "</div>";

            notificationHtml += "<div class='dropdown-item-text'>"+ DescriptionText + ": " + otherProperty + "</div></div>";
            notificationHtml += "<p class='dropdown-item-text time-color' style='color:gray;'>"+ timeDescription;
            notificationHtml += "</p></a>";

            return notificationHtml;
        }

    }
}