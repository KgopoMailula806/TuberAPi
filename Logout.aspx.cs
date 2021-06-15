using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Email"] = null;
                Session["ID"] = null;
                Session["UserStatus"] = null;
                Response.Redirect("Home.aspx");
                if (Request.Cookies["Email"] != null)
                {
                    Response.Cookies["Email"].Expires = DateTime.Now.AddDays(-1);
                    Response.Redirect("Home.aspx");
                }
            }
        }
    }
}