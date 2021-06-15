using Newtonsoft.Json;
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
    public partial class Tuber : System.Web.UI.MasterPage
    {        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (HttpContext.Current.Session["Email"] != null)
                {
                    logout.Visible = true;
                    login.Visible = false;
                    sessions.Visible = true;
                    Modules.Visible = true;
                    ViewRequests.Visible = true;
                    profile_.Visible = true;
                    requestTutor_.Visible = false;
                    homeLink.Visible = true;
                   // Modules.Visible = true;
                    switchAccountProperty.Visible = true;

                    //Notification Example
                    notiBox.Visible = true;
                    

                    //For controlling visibility
                    controllPageVisbility();
                    setRecentlyExpiredSession();
                    //For controlling Notifications
                    Notifications.InnerHtml = Notification_Functionality.getNotifications(HttpContext.Current.Session["ID"].ToString());
                    Notifications.InnerHtml += Notification_Functionality.CheckGeneralNotifications(HttpContext.Current.Session["UserStatus"].ToString(), HttpContext.Current.Session["ID"].ToString());                  
                    Numnotification();

                    //User
                    userBox.Visible = true;
                    userType.InnerText = HttpContext.Current.Session["UserStatus"].ToString();

                   
                    if (HttpContext.Current.Session["UserStatus"].ToString().Equals(SITEConstants.CLIENT_VAR))
                    {
                        
                        userType.InnerText = "Student";
                        //set the user type in the database
                        ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/changeCurrentUserStatus/" + HttpContext.Current.Session["ID"] + "/" + SITEConstants.CLIENT_VAR);
                    }
                    else if (HttpContext.Current.Session["UserStatus"].ToString().Equals(SITEConstants.TUTOR_VAR))
                    {
                        userType.InnerText = SITEConstants.TUTOR_VAR;
                        //set the user type in the database
                        ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/changeCurrentUserStatus/" + HttpContext.Current.Session["ID"] + "/" + userType.InnerText);

                    }
                    else if (HttpContext.Current.Session["UserStatus"].ToString().Equals(SITEConstants.MANAGER_VAR))
                    {
                        userType.InnerText = SITEConstants.MANAGER_VAR;
                        profile_.Visible = false;
                        requestTutor_.Visible = false;
                        //set the user type in the database
                        ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/changeCurrentUserStatus/" + HttpContext.Current.Session["ID"] + "/" + userType.InnerText);
                    }
                    name.InnerText = HttpContext.Current.Session["Email"].ToString();
                }
                else
                {
                }

            }
        }

        /// <summary>
        ///  This regularly sets
        /// </summary>
        public void Numnotification()
        {
            //Number of notifications will be extracted from the db
            string numNotifications = Notification_Functionality.getNumNotificatons(HttpContext.Current.Session["ID"].ToString()).ToString();
            noti_number.InnerText = numNotifications;
            if (Int32.Parse(numNotifications) > 0)
            {
                SeeAllAchor.InnerText = "See all notifications";
            }
            else
            {
                SeeAllAchor.InnerText = "No notifications";
            }
        }
 

        /// <summary>
        /// This method controlls the visibility of what the general webpage aspects that the users can s
        /// </summary>
        protected void controllPageVisbility()
        {
            HttpContext.Current.Session["HasMultipleAccounts"] = "False"; //initially
            if (HttpContext.Current.Session["UserStatus"].Equals("Client"))
            {
                requestTutorNavItem.Visible = true;
                viewBookedSessions.Visible = true;
                ViewRequests.Visible = false;

                // check if also I'm a tutor                 
                string result = GenericHelperFunctionality.checkIfUserHasSecondaryAccount(HttpContext.Current.Session["ID"].ToString(), HttpContext.Current.Session["UserStatus"].ToString());
                if (result.Equals("1")) // the client is also a tutor
                {
                    HttpContext.Current.Session["HasMultipleAccounts"] = "True"; //To activate switch roles property
                }
            }
            else if (HttpContext.Current.Session["UserStatus"].Equals("Tutor"))
            {

                //check if the tutor account is active
                string respons  = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/isTutorAccepted/" + HttpContext.Current.Session["ID"]);
                if (respons.Equals("1"))
                {
                    requestTutorNavItem.Visible = false;                    
                    viewBookedSessions.Visible = true;
                    ViewRequests.Visible = true;
                    switchAccountProperty.Visible = true;
                }
                else
                {
                    requestTutorNavItem.Visible = false;
                    Modules.Visible = false;
                    ViewRequests.Visible = false;
                    switchAccountProperty.Visible = false;
                    viewBookedSessions.Visible = false;
                }

                // check if also I'm a Client 
                string result = GenericHelperFunctionality.checkIfUserHasSecondaryAccount(HttpContext.Current.Session["ID"].ToString(), HttpContext.Current.Session["UserStatus"].ToString());
                if (result.Equals("1")) // the client is also a tutor
                {
                    HttpContext.Current.Session["HasMultipleAccounts"] = "True";
                }
            }
            else if (HttpContext.Current.Session["UserStatus"].Equals("Manager"))
            {
                ManagerTerminal.Visible = true;
                Modules.Visible = false;
                contact.Visible = false;
                ViewRequests.Visible = false;
            }

            if (HttpContext.Current.Session["HasMultipleAccounts"] != null)
            {
                switch (HttpContext.Current.Session["HasMultipleAccounts"])
                {
                    case "True":
                        {
                            switch (HttpContext.Current.Session["UserStatus"])
                            {
                                case "Tutor": //If this is the case then there's a client account  that exists
                                    {
                                        string innerText = "";

                                        innerText += "<p class='mb-0'><a href='SwitchAccount.aspx?UserStatus=Client' class='btn py-2 px-3 btn-primary d-flex align-items-center justify-content-center'>";
                                        innerText += "<span class='icon-arrow-left mr-2'></span>switch account";
                                        innerText += "</a></p>";                                     

                                        switchAccountProperty.InnerHtml = innerText;
                                    }
                                    break;
                                case "Client": //If this is the case then there's a tutor account that exists
                                    {
                                        string innerText = "";

                                        innerText += "<p class='mb-0'><a href='SwitchAccount.aspx?UserStatus=Tutor' class='btn py-2 px-3 btn-primary d-flex align-items-center justify-content-center'>";
                                        innerText += "<span class='icon-arrow-left mr-2'></span>switch account";
                                        innerText += "</a></p>";
                                        switchAccountProperty.InnerHtml = innerText;
                                    }
                                    break;
                            }
                        }
                        break;
                    case "False":
                        {
                            switch (HttpContext.Current.Session["UserStatus"])
                            {
                                case "Tutor": //If this is the case then there's a client account  that exists
                                    {
                                        string innerText = "";
                                        innerText += "<p class='mb-0'><a href='UserRegistration.aspx?As=Client' class='btn py-2 px-3 btn-primary d-flex align-items-center justify-content-center'>";
                                        innerText += "<span class='icon-user-circle mr-2'></span>sign up as a client";
                                        innerText += "</a></p>";                                        

                                        switchAccountProperty.InnerHtml = innerText;
                                    }
                                    break;
                                case "Client": //If this is the case then there's a tutor account that exists
                                    {
                                        string innerText = "";
                                        innerText += "<p class='mb-0'><a href='UserRegistration.aspx?As=Tutor' class='btn py-2 px-3 btn-primary d-flex align-items-center justify-content-center'>";
                                        innerText += "<span class='icon-user-circle mr-2'></span>become a tutor";
                                        innerText += "</a></p>";
                                        switchAccountProperty.InnerHtml = innerText;
                                        
                                    }
                                    break;
                            }
                        }
                        break;
                }

            }

        }

        /// <summary>
        /// 
        /// </summary>
        protected void setRecentlyExpiredSession()
        {
            //checks for session that should be expiered and deactivates them
            ApiComnunication.getJsonEntities( SITEConstants.BASE_URL + "api/ClientBooking/areTherAnyExpiredSessionsWithUserTableID/" + Session["ID"].ToString() + "/" + Session["UserStatus"].ToString());
        }
    }
}