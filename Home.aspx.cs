using Newtonsoft.Json;
using QuadCore_Website.HelperFunctionality;
using QuadCore_Website.models.NonDatabaseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if ((Session["Email"] != null || Response.Cookies["Email"] != null) && HttpContext.Current.Session["ID"] != null)
            {
                //Do nothing
                signInBtn.Visible = false;
                if (HttpContext.Current.Session["UserStatus"] != null)
                {
                    switch (HttpContext.Current.Session["UserStatus"])
                    {
                        case "Tutor":
                            {
                                string respons = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/isTutorAccepted/" + HttpContext.Current.Session["ID"]);
                                if (respons.Equals("0"))
                                    Response.Redirect("ApplicationPending.aspx");
                            }
                        break;
                    }
                    

                }
            }
            else
                Response.Redirect("Login.aspx");
            

        }
    }
}