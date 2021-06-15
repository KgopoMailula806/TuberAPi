using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuberAPI.models;

namespace QuadCore_Website
{
    public partial class FileTesting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            HttpPostedFile toSend = userFile.PostedFile;
            /*int flag = FileFunctionality.upload(toSend);

            if(flag != -1)
            {
                Document fromDB = FileFunctionality.GetFile(flag);

                if(fromDB != null)
                {
                    string display = "<object data='data:application/pdf;base64, "+ imageString + "' type='application/pdf' width=\"650\" height=\"842\">";
                    display += "<iframe src=\"https://docs.google.com/viewer?&embedded=true\"></iframe>";
                    display += "</object>";

                    imageFrame.InnerHtml = display;
                }
            }*/

            BinaryReader br = new BinaryReader(toSend.InputStream);
            int size = toSend.ContentLength;
            byte[] imageData = br.ReadBytes(size);
            string fileExt = Path.GetExtension(toSend.FileName);
            string imageString = Convert.ToBase64String(imageData);

            string display = "<object data='data:application/pdf;base64, "+ imageString + "' type='application/pdf' width=\"650\" height=\"842\">Document";
            //display += "<iframe src=\"https://docs.google.com/viewer?&embedded=true\"></iframe>";
            display += "</object>";

            imageFrame.InnerHtml = display;

            //imageDisplay.ImageUrl = "data:image/"+ fileExt +";base64," + imageString;

            /**
             * <object data='data:application/pdf;base64, your_base64_data' type='application/pdf'>
                <iframe src="https://docs.google.com/viewer?&embedded=true"></iframe>
               </object>
             * 
             * **/

        }

    }
}