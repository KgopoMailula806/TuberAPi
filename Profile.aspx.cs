using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuadCore_Website.HelperFunctionality;
using TuberAPI.models;

namespace QuadCore_Website
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] != null)
            {
                displayUserInfoDynamically();

                //get user information from database
                string UriPath = SITEConstants.BASE_URL + "api/User/GetUser/" + Session["ID"]; ;
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

                    if(userResponseObj.User_Discriminator.Equals("Client"))
                    {
                        UriPath = SITEConstants.BASE_URL + "api/Client/GetClientByForeignKey/" + Session["ID"];
                        string ClientTableResponseBody = ApiComnunication.getJsonEntities(UriPath);
                        Client clientResponseObj = JsonConvert.DeserializeObject<Client>(ClientTableResponseBody);

                        if (clientResponseObj != null)
                        {
                            innerText += "<p><b>Grade/year of study: </b> " + clientResponseObj.Current_Grade + "</p>";
                            innerText += "<p><b>Educational Institute: </b> " + clientResponseObj.Institution + "</p>";
                        }
                    }

                    
                    innerText += "<a class=\"btn btn-primary\" href='EditProfile.aspx?bariId="+ userResponseObj.Id +"'>edit</a>";
                    UserDetailsDiv.InnerHtml = innerText;

                    //Get image
                    Document image = FileFunctionality.GetFile(userResponseObj.Image);
                    if (image != null)
                        profilePic.ImageUrl = "data:image/" + image.Extension + ";base64," + image.DocumentData;
                    //profileImage.InnerHtml = "<img src=\""+ userResponseObj.Image + "\" width=\"400\" height=\"450\" />";

                }
                SetUpInvoices();
            }
        }

        public void displayUserInfoDynamically()
        {
            //Module Corner
            List<Module> modules = ModuleFunctionality.getModules(Session["ID"].ToString(), Session["UserStatus"].ToString());

            if (modules != null)
            {
                string moduleInfo = "<p><b> Modules </b></p>";
                foreach (Module module in modules)
                {
                    moduleInfo += "<p>" + module.Module_Name + "</p>";
                }
                moduleInfo += "<a href='UserModules.aspx'><p class='btn btn-primary'>edit</p></a>";
                module_overview.InnerHtml = moduleInfo;
            }

            //Invoice Corner

            //Payment Info Corner

        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            //An id must be passed onto this page so that we retrieve the users payment if from api
            Response.Redirect("PaymentInfo.aspx");
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SetUpInvoices(){

            string clientId = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Client/GetClientTablereferenceByForeignKey/" + HttpContext.Current.Session["ID"].ToString());

            //Get the invoices
            if (!clientId.Equals("0"))
            {
                string invoiceURl = SITEConstants.BASE_URL + "api/Invoice/GetUserInvoicesUnPaidInvoices/" + clientId;

                string invoiceJsonCollection = ApiComnunication.getJsonEntities(invoiceURl);

                if (!invoiceJsonCollection.Equals("0"))
                {
                    List<Invoice> Invoices = JsonConvert.DeserializeObject<List<Invoice>>(invoiceJsonCollection);
                    int count = 0;
                    string invoiceTXT = "";
                    foreach (Invoice invoice in Invoices)
                    {
                        if (count > 3)
                             break;
                        
                        invoiceTXT += "<p><a href='Invoices.aspx?InvoiceID=" + invoice.Id + "'>Invoice date: "+invoice.Date_Issued + "</a></p>";
                        count++;
                    }
                    invoices.InnerHtml = invoiceTXT;
                }
            }
            else
            {

            }
                
        }
    }
}