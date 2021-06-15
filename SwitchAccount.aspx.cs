using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class SwitchAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["UserStatus"] != null)
            {
                Session["UserStatus"] = Request.QueryString["UserStatus"];
                Response.Redirect("Home.aspx");
            }
        }
    }
}