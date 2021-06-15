using Microsoft.SqlServer.Server;
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
    public partial class TutorRankings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadRatings();
        }


        public void loadRatings()
        {

            string display = "";
            int counter = 0;
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
                        
                   

                    User cli = UserFunctionality.GetTutorsUserTableDetails(r.Id.ToString());
                    //Display In table format
                    display += "<tr class='text-centre'><td>" + ++counter + "</td>";
                    display += "<td>" + cli.FullNames + "</td><th>&nbsp</th>";
                    display += "<td>" + cli.Surname + "</td><th>&nbsp</th>";
                    display += "<td>" + Math.Round(aveRate,2) + "</td>";
                    display += "<td><div class='form-group row justify-content-center'> ";
                    display += "<a href='TutorReport.aspx?rID=" + r.Id+"' class='btn btn-primary'>View Summary</a></div></td></tr>";
                  
                   
                }
                

            }

            tutorRatings.InnerHtml = display;
        }
    }
}