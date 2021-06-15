using Newtonsoft.Json;
using QuadCore_Website.HelperFunctionality;
using QuadCore_Website.models.NonDatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuberAPI.models;

namespace QuadCore_Website
{
    public partial class TutorReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tutorId = Request.QueryString["rID"];
            string userpath = SITEConstants.BASE_URL + "api/Tutor/GetUserTableIdByPK/" + tutorId;
            string userID = ApiComnunication.getJsonEntities(userpath);

            string sesspath = SITEConstants.BASE_URL + "api/Report/getNoOfCompletedSessionForTutor/" + tutorId;
            string noOfsessionsCompleted = ApiComnunication.getJsonEntities(sesspath);
            double totalRates = 0;
            double aveRate = 0;

          
                foreach (Rating rate in ReportHelper.getTutorRatings(Int32.Parse(tutorId)))
                {
                    totalRates += rate.Tutor_Rating;
                }

                //Work out average for specific tutor
                if (ReportHelper.getTutorRatings(Int32.Parse(tutorId)).Count() == 0)
                {
                    aveRate = 5;
                }
                else
                {
                    aveRate = totalRates / ReportHelper.getTutorRatings(Int32.Parse(tutorId)).Count();
                }

                if (noOfsessionsCompleted != null)
            {
                TutorReports tutRep = JsonConvert.DeserializeObject<TutorReports>(noOfsessionsCompleted);



                if (userID != null)
                {

                    //get user information from database
                    string UriPath = SITEConstants.BASE_URL + "api/User/GetUser/" + userID;
                    string userTableResponseBody = ApiComnunication.getJsonEntities(UriPath);

                    User userResponseObj = JsonConvert.DeserializeObject<User>(userTableResponseBody);

                    if (userResponseObj != null)
                    {
                        string innerText = "";

                        innerText += "<h4><b>Your Details</b></h4>";
                        innerText += "<p><b>Name: </b>" + userResponseObj.FullNames + "</p>";
                        innerText += "<p><b>Surname: </b>" + userResponseObj.Surname + "</p>";
                        innerText += "<p><b>Number: </b> " + userResponseObj.Valid_Phone_Number + "</p>";
                        innerText += "<p><b>Email: </b> " + userResponseObj.Email_Address + "</p>";
                        innerText += "<p><b>Gender: </b> " + userResponseObj.Gender + "</p>";
                        innerText += "<p><b>Age: </b> " + userResponseObj.Age + "</p>";
                        innerText += "<p><b>Average Rating: </b> " +" "+ aveRate + "</p>";
                        innerText += "<p><b>No. of sessions completed: </b> " +" " +tutRep.noOfsess + "</p>";


                        UserDetailsDiv.InnerHtml = innerText;

                        //Get image
                        Document image = FileFunctionality.GetFile(userResponseObj.Image);
                        if (image != null)
                            profilePic.ImageUrl = "data:image/" + image.Extension + ";base64," + image.DocumentData;
                        //profileImage.InnerHtml = "<img src=\""+ userResponseObj.Image + "\" width=\"400\" height=\"450\" />";

                    }

                }
            }





            //Invoice Corner

            //Payment Info Corner

        }

    }
    
}