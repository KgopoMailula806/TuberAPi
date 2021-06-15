using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class SingleSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //id fetched from the url parameter
            //int sessionID = Convert.ToInt32(Request.QueryString["ID"]);

            //TODO: use Api to fetch object to display

            //TODO: use object to display the session


        }

        private string displaySession()
        {
            //TODO: check who it is.
            string display = "<p><b>Tutor/Client Name: </b>Skyler</p>";
            display += "<p><b>Module: </b> Calculas</p>";
            display += "<p><b>Location: </b> Congo</p>";
            display += "<p><b>Date: </b> 1999/03/01</p>";
            display += "<p><b>Time:</b> 16:20</p>";
            display += "<label class='font-weight-bold'>Session Description/Help Wanted In:</label>";
            display += "<p>Session description</p>";


            return display;
        }
    }
}