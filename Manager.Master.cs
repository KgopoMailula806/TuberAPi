using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class Manager : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* if(Session["UserStatus"].ToString() != "Manager")
             {
                 sidebar.Visible = false;
             }

            //string numNotifications = Notification_Functionality.getNumNotificatons(HttpContext.Current.Session["ID"].ToString()).ToString();

            noti_number.InnerText = numNotifications;
            if (Int32.Parse(numNotifications)> 0)
            {
                SeeAllAchor.InnerText = "See all notifications";
            }
            else
            {
                SeeAllAchor.InnerText = "No notifications";
            }*/
        }
    }
}