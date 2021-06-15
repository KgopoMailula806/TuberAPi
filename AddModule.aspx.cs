using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class AddModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] != null  || Response.Cookies["Email"] != null)
            {
                if (Response.Cookies["UserStatus"] != null) // should never happen but just to be safe 
                {
                    switch (Response.Cookies["UserStatus"].Value)
                    {
                        case "Manager":
                            {

                            }break;
                        default: /*Response.Redirect("#");*/ break; //Only a manager status has clearance to this page
                    }
                }
                else Response.Redirect("Login.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            string newModule = NewModule.Value;
            string moduleCode = ModuleCode.Value;
        }
    }
}