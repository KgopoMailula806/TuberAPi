using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuberAPI.models;

namespace QuadCore_Website.HelperFunctionality
{
    public class MeetingFunctionality
    {

        public static User getUser(int id)
        {
            string response = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/Tutor/GetUserTableDetails/" + id);
            if(response != null)
            {
                try
                {
                    User resObj = JsonConvert.DeserializeObject<User>(response);
                    return resObj;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException());
                }
                return null;
                
            }

            return null;
        }

        public static List<Meeting> GetMeetings()
        {
            string url = SITEConstants.BASE_URL+"api/Meeting/getMeetings";
            string strMeetings = ApiComnunication.getJsonEntities(url);

            if(strMeetings != null)
            {
                return JsonConvert.DeserializeObject<List<Meeting>>(strMeetings);
            }

            return null;
        }

        public static List<Meeting> GetShortList()
        {
            string url = SITEConstants.BASE_URL+ "api/Meeting/getShortlist";
            string strMeetings = ApiComnunication.getJsonEntities(url);

            if (strMeetings != null)
            {
                return JsonConvert.DeserializeObject<List<Meeting>>(strMeetings);
            }

            return null;
        }

        public static List<Meeting> getVerdictObjects()
        {
            string url = SITEConstants.BASE_URL + "api/Meeting/getVerdictObjs";
            string strMeetings = ApiComnunication.getJsonEntities(url);

            if (strMeetings != null)
            {
                return JsonConvert.DeserializeObject<List<Meeting>>(strMeetings);
            }

            return null;
        }

        public static bool addMeeting(string date, string time, string venue, string TutorID, string type)
        {
            string url = SITEConstants.BASE_URL+"api/Meeting/AddMeeting";
            string body = "{\"Id\":0 ," +
                          "\"Date\":\"" + date + "\"," +
                          "\"Time\":\""+ time +"\"," +
                          "\"Type\":\""+ type +"\"," +
                          "\"Venue\":\""+ venue +"\"," +
                          "\"Attended\":"+ 0 +"," +
                          "\"Minutes\":\"\"," +
                          "\"TutorID\":" + TutorID + "}";

            string response = ApiComnunication.postJsonEntitie(url, body);
            if(response != null)
            {
                return true;
            }
            return false;
        }
    }
}