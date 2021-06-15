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
	public partial class OutstandingPayments : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//Get Outstanding payments
			setOutstandingPayments();
		}

		public void setOutstandingPayments()
		{
			


            string display = "";
            int counter = 0;
            foreach (Invoice m in ReportHelper.getNumberOfOutstandingPayments())
            {
                Tutorial_Session sess = SessionFunctionality.getSession(m.Session_ID);
                if (sess == null)
                    continue;

                User cli = MeetingFunctionality.getUser(m.Client_ID);
                if (cli == null)
                    continue;
               
                BookingRequest booking = BookingRequestFunctionality.getBooking(sess.ClientBookingID);
                if (booking == null)
                    continue; //Default case

                Module mod = ModuleFunctionality.getModule(booking.ModuleID1);
                if (mod == null)
                    continue;

                display += "<tr class='text-centre'><td>" + ++counter + "</td>";
                display += "<td>" + cli.FullNames + " " + cli.Surname + "</td><th>&nbsp</th>";
                display += "<td>" + mod.Module_Code + "</td><th>&nbsp</th>";
                display += "<td>" + m.Date_Issued + "</td>";
                display += "<td>" + m.Amount + "</td>";
                display += "<th>&nbsp</th>";
                display += "<th>&nbsp</th>";
                display += "<th>&nbsp</th>";
                display += "<th>&nbsp</th>";


            }

            outPays.InnerHtml = display;
        }
	}
}