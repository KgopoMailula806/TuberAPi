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
    public partial class pdfViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string docID = Request.QueryString["dId"];
            string type = Request.QueryString["type"];
            if (docID != null && type != null)
            {
                if (type == "PC")
                {

                    Page.Title = "Police Clearance of " + Session["TutorName"].ToString();
                    head.InnerText = "Police Clearance of:";
                }
                else if (type == "CV")
                {

                    Page.Title = "Curriculum Vitae of " + Session["TutorName"].ToString();
                    head.InnerText = "Curriculum Vitae of:";
                }
                else if (type == "AR")
                {

                    Page.Title = "Academic Record of " + Session["TutorName"].ToString();
                    head.InnerText = "Academic Record of:";
                }

                name.InnerText = Session["TutorName"].ToString();
                Document document = FileFunctionality.GetFile(Convert.ToInt32(docID));

                if (document != null)
                {
                    string display = "<object data='data:application/pdf;base64, " + document.DocumentData + "' type='application/pdf' width=\"650\" height=\"842\">";
                    display += "<iframe src=\"https://docs.google.com/viewer?&embedded=true\"></iframe>";
                    display += "</object>";

                    pdfFrame.InnerHtml = display;
                }
            }
            

        }
    }
}