using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuberAPI.models;
using Newtonsoft.Json;

namespace QuadCore_Website.HelperFunctionality
{
    public class SessionFunctionality
    {
        public static Tutorial_Session getSession(int id)
        {
            Tutorial_Session payObj = null;
            string numOfOutPays = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL +"api/TutorialSession/getSession/"+ id);
            if (!numOfOutPays.Equals(-1))
            {
                payObj = JsonConvert.DeserializeObject<Tutorial_Session>(numOfOutPays);
                
            }

            return payObj;

        }

        public static List<Tutorial_Session> getActiveSessions()
        {
            List<Tutorial_Session> activeSess = null;
            string sessions = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/TutorialSession/getCompletedSessions");
            if(!sessions.Equals(-1))
            {
                activeSess = JsonConvert.DeserializeObject<List<Tutorial_Session>>(sessions);
            }


            return activeSess;
        }
    }
}