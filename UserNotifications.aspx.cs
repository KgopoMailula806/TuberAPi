using QuadCore_Website.HelperFunctionality;
using QuadCore_Website.models.NonDatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuadCore_Website
{
    public partial class UserNotifications : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (HttpContext.Current.Session["Email"] != null)
                {

                    Notifications.InnerHtml += Notification_Functionality.getNotifications(HttpContext.Current.Session["ID"].ToString());
                    Notifications.InnerHtml += Notification_Functionality.CheckGeneralNotifications(HttpContext.Current.Session["UserStatus"].ToString(), Session["ID"].ToString());
                    Numnotification();

                }
                else
                    Response.Redirect("Login.aspx");
            }
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_GetSearForNotifications_Click(object sender, EventArgs e)
        {            
            TimeRange timeRange = new TimeRange();
            //See if either one of the seen or unseen field are checked
            if (!(Seen.Checked) && !(Unseen.Checked))
            {
                string strMsg = "please check either one of the seen or unseen buttons";
                string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                Response.Write(script);
                return; 
            }

            //get seenFlag 
            int seenFlag = -1;
            if (Seen.Checked) 
                seenFlag = 1;
            else if(Unseen.Checked)
                seenFlag = 0;

            switch (notificationFilter.SelectedValue)
            {
                case "0":
                    {           
                        Notifications.InnerHtml = Notification_Functionality.getNotifications(HttpContext.Current.Session["ID"].ToString(), timeRange, notificationFilter.SelectedValue, seenFlag);
                        notificationHeader.InnerText = "all seen and unseen notifications";                        
                        if (seenFlag == 0)
                        {
                            notificationHeader.InnerText = "all unseen notifications";
                        }
                        else
                        {
                            notificationHeader.InnerText = "all seen notifications";
                        }
                    }
                    break;
                case "1"://From selected Date to present
                    {
                        if (selectedDate.Value.Length <= 0)
                        {
                            string strMsg = "Please enter \"From\" date";
                            string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                            Response.Write(script);
                            break;
                        }                      

                        timeRange.earlierDate = selectedDate.Value;
                        try
                        {
                            DateTime fromDate = DateTime.Parse(timeRange.earlierDate);
                            //send the notification reqest
                            Notifications.InnerHtml = Notification_Functionality.getNotifications(HttpContext.Current.Session["ID"].ToString(), timeRange, notificationFilter.SelectedValue, seenFlag);
                            if (seenFlag == 0)
                            {
                                notificationHeader.InnerText = "all unseen notifications between now and " + timeRange.earlierDate;
                            }
                            else
                            {
                                notificationHeader.InnerText = "all seen notifications between now and : " + timeRange.earlierDate;
                            }
                        }
                        catch(Exception ex)
                        {
                            string strMsg = "Unknow error";
                            string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                            Response.Write(script);

                        }
                        
                       

                    }
                    break;
                case "2"://From selected Date to Other SelectedDate                    
                    {
                        if (selectedDate.Value.Length <= 0 && secondarySelectedDate.Value.Length <= 0)
                        {
                            string strMsg = "Please enter the missing \"To\" and \"from\" date";
                            string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                            Response.Write(script);
                            break;
                        }
                        
                        timeRange.earlierDate = selectedDate.Value;
                        timeRange.laterDate = secondarySelectedDate.Value;
                        try {    
                            DateTime fromDate = DateTime.Parse(timeRange.earlierDate);
                            DateTime toDate = DateTime.Parse(timeRange.laterDate);

                            Notifications.InnerHtml = Notification_Functionality.getNotifications(HttpContext.Current.Session["ID"].ToString(), timeRange, notificationFilter.SelectedValue, seenFlag);
                            if (seenFlag == 0)
                            {
                                notificationHeader.InnerText = "all unseen notifications From: " + timeRange.earlierDate + " to: " + timeRange.laterDate;
                            }
                            else
                            {
                                notificationHeader.InnerText = "all seen notifications From: " + timeRange.earlierDate + " to: " + timeRange.laterDate;
                            }
                        }
                            catch (Exception ex)
                        {
                            string strMsg = "There's a missing date that hasn't been entered";
                            string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                            Response.Write(script);

                        }
                    }
                    break;
                case "3"://Before selected Date
                    {
                        if (secondarySelectedDate.Value.Length <= 0)
                        {
                            string strMsg = "Please enter the missing \"To\" date";
                            string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                            Response.Write(script);
                            break;
                        }
                         
                        timeRange.laterDate = secondarySelectedDate.Value;
                        try 
                        { 
                            DateTime toDate = DateTime.Parse(timeRange.laterDate);

                            Notifications.InnerHtml = Notification_Functionality.getNotifications(HttpContext.Current.Session["ID"].ToString(), timeRange, notificationFilter.SelectedValue, seenFlag);
                            if (seenFlag == 0)
                            {
                                notificationHeader.InnerText = "all unseen notifications before: " + timeRange.laterDate;
                            }
                            else
                            {
                                notificationHeader.InnerText = "all seen notifications before: " + timeRange.laterDate;
                            }
                        }
                                catch (Exception ex)
                        {
                            string strMsg = "Please enter the missing \"To\" date";
                            string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                            Response.Write(script);

                        }

            }
                    break;
            }
           


        }

        /// <summary>
        /// For where either the filter ltems are selected
        /// </summary>       
        protected void OnSelectedIndexChange(object sender, EventArgs e)
        {            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void notificationFilter_TextChanged(object sender, EventArgs e)
        {
            string s = notificationFilter.SelectedItem.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Numnotification()
        {
            //Number of notifications will be extracted from the db
            string numNotifications = Notification_Functionality.getNumNotificatons(HttpContext.Current.Session["ID"].ToString()).ToString();

            
            if (Int32.Parse(numNotifications) > 0)
            {
                notificationHeader.InnerText = "all unseen notifications";
            }
            else
            {
                notificationHeader.InnerText = "No notifications";
            }
        }
    }
}