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
    public partial class ElapsedSessions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setElapsedSessions();
        }


        public void setElapsedSessions()
        {

            //Get sessions with isCompleted equal to 1
            List<Tutorial_Session> actSessions = SessionFunctionality.getActiveSessions();
            //Display sessions

            string display = "";
            int counter = 0;
            foreach (Tutorial_Session m in actSessions)
            {
                User cli = UserFunctionality.GetClientsUserTableDetails(m.Client_Reference.ToString());
                if (cli == null)
                    continue;

                User tutortDets = UserFunctionality.GetTutorsUserTableDetails(m.Tutor_Id.ToString());
                if (tutortDets == null)
                    continue;
                BookingRequest booking = BookingRequestFunctionality.getBooking(m.ClientBookingID);
                if (booking == null)
                    continue; //Default case

                Module mod = ModuleFunctionality.getModule(booking.ModuleID1);
                if (mod == null)
                    continue;

                display += "<tr class='text-centre'><td>" + ++counter + "</td>";
                display += "<td>" +mod.Module_Code +"</td><th>&nbsp</th>";
                display += "<td>" + tutortDets.FullNames + " " + tutortDets.Surname + "</td><th>&nbsp</th>";
                display += "<td>" + cli.FullNames + " " + cli.Surname + "</td><th>&nbsp</th>";
                display += "<td>" + m.Geographic_Location + "</td><th>&nbsp</th>";
                display += "<td>" + m.Session_Start_Time + "</td><th>&nbsp</th>";
                display += "<td>" + m.Session_End_Time + "</td><th>&nbsp</th>";
                display += "<th>&nbsp</th>";

            }

            elapsedSession.InnerHtml = display;
        }
    }
}