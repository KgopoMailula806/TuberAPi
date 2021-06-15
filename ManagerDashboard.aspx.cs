using QuadCore_Website.HelperFunctionality;
using QuadCore_Website.models;
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
    public partial class ManagerDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setDynamicValues();
        }

        public void setDynamicValues()
        {
            int num_meetings = MeetingFunctionality.GetMeetings().Count;
            meetings.InnerText = num_meetings + " Meetings Scheduled!";

            int num_shortlists = MeetingFunctionality.GetShortList().Count;
            tutor_shortlist.InnerText = num_shortlists + " Potential Tutors in Shortlist!";

            int numOfOutst = ReportHelper.getNumberOfOutstandingPayments().Count;
            payments.InnerText = numOfOutst + " Outstanding Payments";

            List<TutorRating> ratings = new List<TutorRating>();
            List<TutorRating> lowest = new List<TutorRating>();
           
            //Section to generate top 5 worst tutors
            foreach (Tutor r in TutorFunctionality.GetTutors())
            {
                //Get average per tutor
                double aveRate = 0.0;
                double totalRates = 0;
                //Get ratings for specific tutor 
                if (!(ReportHelper.getTutorRatings(r.Id) == null))
                {
                    foreach (Rating rate in ReportHelper.getTutorRatings(r.Id))
                    {
                        totalRates += rate.Tutor_Rating;
                    }

                    //Work out average for specific tutor
                    if (ReportHelper.getTutorRatings(r.Id).Count() == 0)
                    {
                        aveRate = 5;
                    }
                    else
                    {
                        aveRate = totalRates / ReportHelper.getTutorRatings(r.Id).Count();
                    }


                }

                User tutor = UserFunctionality.GetTutorsUserTableDetails(r.Id.ToString());
                ratings.Add(new TutorRating(tutor.FullNames,tutor.Surname,aveRate));
                if (aveRate < 6)
                    lowest.Add(new TutorRating(tutor.FullNames, tutor.Surname, aveRate));
            }

            //ratings.Sort();
           

            string display = "";
            foreach (TutorRating r in lowest)
            {
                display += "<tr class='text-centre'>"; 
                display += "<td>" + r.TutorName + "</td><th>&nbsp</th>";
                display += "<td>" + r.TutorSurname + "</td><th>&nbsp</th>";
                display += "<td>" + Math.Round(r.AveRating,2) + "</td><th>&nbsp</th>";
                display += "</tr>";
            }

            worstTutors.InnerHtml = display;

        }
    }
}