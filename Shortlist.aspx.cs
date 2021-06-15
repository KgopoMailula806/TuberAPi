using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuberAPI.models;

namespace QuadCore_Website
{
    public partial class Meetings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            meetings.InnerHtml = displayMeetings();

            if (Request.QueryString["action"] != null && Request.QueryString["meetingID"] != null)
            {
                //log the meeting using the url parameters
                logMeeting(Request.QueryString["meetingID"]);
            }
        }

        public void logMeeting(string meetingID)
        {
            string response = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Meeting/logMeeting/" + meetingID);
            if (Convert.ToInt32(response) == 1)
            {
                return;
            }
        }

        private string displayMeetings()
        {
            string display = "";
            int counter = 0;
            foreach (Meeting m in MeetingFunctionality.GetShortList())
            {
                User tutor = MeetingFunctionality.getUser(m.TutorID);
                if (tutor != null)
                {
                    display += "<tr class='text-centre'><td>" + ++counter + "</td>";
                    display += "<td>" + tutor.FullNames + " " + tutor.Surname + "</td>";
                    display += "<th>&nbsp</th>";
                    display += "<td class='text-nowrap'>" +
                               "<p style='display: inline-table'>";                              

                    display += "<td><p><a class='btn btn-primary' href='?action=logMeting&meetingID=" + m.Id + "'>log meeting</a></p></td>";

                }

            }

            return display;
        }
    }
}