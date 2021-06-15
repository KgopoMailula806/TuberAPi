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
    public partial class TutorApplications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Display();

        }

        private void Display()
        {
            string display = "";
            
            string tResponse = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+ "api/Tutor/getAllUnAcceptedTutorsUserDetails");
            List<User> tutorDetails = JsonConvert.DeserializeObject<List<User>>(tResponse);
            int counter = 0;

            foreach(User u in tutorDetails)
            {
                display += "<tr class='text-center'>";
                display += "<td>" + (++counter) +"</td>";
                display += "<td>"+ u.FullNames + " " + u.Surname +"</td>";
                display += "<td><div class='form-group row justify-content-center'> ";
                display += "<a href='ApplicationView.aspx?uID="+ u.Id +"' class='btn btn-primary'>view application</a></div></td></tr>";
            }

            tutors.InnerHtml = display;
        }
    }
}