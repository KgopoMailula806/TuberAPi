using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace QuadCore_Website.HelperFunctionality
{
    public class EmailHelper
    {
        public static string makeDeactivateAccEmail(string name, string manager, string urlParameters)
        {
            string message = "";

            message += "Dear " + name + "<br>";
            message += "Please use the link below to deactivated your account <br>";
            message += "Kind Regards <br>";
            message += manager + " (Systems Manager)";
            return setHTML(message, "deactivate account", SITEConstants.Base_SITE_URL + "/ExitingPage.aspx" + urlParameters);
        }

        public static string makeAcceptedEmail(string name, string manager)
        {
            string message = "Re: Tutor Application 2020 <br><br>";

            message += "Dear " + name + "<br>";
            message += "Further to your recent application for the position of Tutoring in our ranks, <br>we are excited to inform you that you have been accepted to become a Tuber Tutor";
            message += "Congratulations, your tutoring account has been activated .<br><br>";
            message += "Kind Regards <br>";
            message += manager + " (Manager of Tuber Recruitment Team)";

            return setHTML(message, "welcome to the team", SITEConstants.Base_SITE_URL + "/login.aspx");
        }

        public static string makeMeetingEmail(string name, string manager, string date, string time, string venue)
        {
            string message = "Re: Tutor Application 2020 <br><br>";

            message += "Dear " + name + "<br>";
            message += "Thank you for taking the time to talk to us about the Tutoring position.<br>We would enjoy getting to know you and we’d like to invite you for a interview at our office.<br>";
            message += "Your interview will be with Mr. King Jacob and will last approximately 30 minutes.<br>";
            message += "Date: "+ date +"<br>";
            message += "Time: "+ time +"<br>";
            message += "Venue: "+ venue +"<br>";
            message += "Looking forward to meeting you,";
            message += "Kind Regards <br>";
            message += manager + " (Manager of Tuber Recruitment Team)";

            return setHTML(message, "visit our website", SITEConstants.Base_SITE_URL);
        }

        public static string meetingCancelation(string name, string manager)
        {
            string message = "Re: Tutor Application 2020 <br><br>";

            message += "Dear " + name + "<br><br>";
            message += "We would like to inform you that your meeting with us has been canceled.<br>";
            message += "Kind Regards <br>";
            message += manager + " (Manager of Tuber Recruitment Team)";

            return setHTML(message, "visit our website", SITEConstants.Base_SITE_URL);
        }


        public static string makeRejectionEmail(string name, string manager)
        {
            string message = "Re: Tutor Application 2020 <br><br>";

            message += "Dear " + name + "<br><br>";
            message += "Further to your recent application for the position of Tutoring in our ranks, <br>we regret to inform you that you have not been short listed/ successful on this occasion.<br>";
            message += "We thank you for your interest in Tutoring position and wish you every success in your future career.<br><br>";
            message += "Kind Regards <br>";
            message +=  manager + " (Manager of Tuber Recruitment Team)";

            return setHTML(message, "visit our website", SITEConstants.Base_SITE_URL);
        }

        public static string setHTML(string message, string buttonText, string buttonLink)
        {
            string display = "<html><head></head><body>";
            display += "<div style='width:1200px; margin:0 auto; font-family: Arial, Helvetica, sans-serif;'>";
            display += "<p style='padding: 10px'>" + message + "</p><br>"; // message must be formal 
            display += "<a href='" + buttonLink + "' style='display:block; width:200px; height:25px; " +
                       "padding: 3px; border-radius: 25px; " +
                        "color:#f5f5f5; text-align:center; text-decoration:none;" +
                        "border:none;background:orangered;'>";
            display += buttonText;
            display += "</a></div></body></html>";


            return display;
        }
    }
}