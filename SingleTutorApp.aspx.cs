using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuberAPI.models;

namespace QuadCore_Website
{
    public partial class SingleTutorApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*string innerText = "";
            innerText += "<p>Name: " + "Jacob " + "Surname: " + "Muzonde ";
            innerText += "Number: " + 12345677890 + " Email: " + "jt.sliqe@gmail.com ";
            innerText += "Gender: " + "Pensexual " + "Age: " + 12 + "</p>";
            innerText += "<p><b>CV:</b><br><a href='pdfViewer.aspx?dId=" + 4 + "&type=CV' target='_blank'>open here<a/></p>";
            innerText += "<p><b>Academic Record:</b><br><a href='pdfViewer.aspx?dId=" + 4 + "&type=AR' target='_blank'>open here<a/></p>";
            innerText += "<p><b>Police Clearance:</b><br><a href='pdfViewer.aspx?dId=" + 4 + "&type=PC' target='_blank'>open here<a/></p>";
            TutorInfor.InnerHtml = innerText;*/


            string tId = Request.QueryString["tID"];
            string uId = Request.QueryString["uID"];

            //get information from database
            string UriPath = SITEConstants.BASE_URL + "api/User/GetUser/" + uId;
            string userTableResponseBody = ApiComnunication.getJsonEntities(UriPath);

            User userResponseObj = JsonConvert.DeserializeObject<User>(userTableResponseBody);

            if (userResponseObj != null)
            {
                string innerText = "";

                innerText += "<p><b>Name: </b>" + userResponseObj.FullNames + "</p>";
                innerText += "<p><b>Surname: </b>" + userResponseObj.Surname + "</p>";
                innerText += "<p><b>Number: </b> " + userResponseObj.Valid_Phone_Number + "</p>";
                innerText += "<p><b>Email: </b> " + userResponseObj.Email_Address + "</p>";
                innerText += "<p><b>Gender: </b> " + userResponseObj.Gender + "</p>";
                innerText += "<p><b>Age: </b> " + userResponseObj.Age + "</p>";
                TutorInfor.InnerHtml = innerText;

                Session["TutorName"] = userResponseObj.FullNames +  " " + userResponseObj.Surname;

                //Get image
                Document image = FileFunctionality.GetFile(userResponseObj.Image);
                TutorImage.ImageUrl = "data:image/" + image.Extension + ";base64," + image.DocumentData;

                string documentText = "";
                string clearanceID = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/TutorDocument/GetClearance/" + tId);
                string cvID = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/TutorDocument/GetCV/" + tId);
                string transcriptID = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/TutorDocument/GetTranscript/" + tId);

                documentText += "<p><b>Curriculum Vitae: </b> <br><a href='pdfViewer.aspx?dId=" + cvID + "&type=CV' target='_blank'> open here <a/></p>";
                documentText += "<p><b>Academic Record: </b> <br><a href='pdfViewer.aspx?dId=" + transcriptID + "&type=AR' target='_blank'> open here <a/></p>";
                documentText += "<p><b>Police Clearance: </b> <br><a href='pdfViewer.aspx?dId=" + clearanceID + "&type=PC' target='_blank'> open here <a/></p>";

                documents.InnerHtml = documentText;
                
            }
        }
    }
}