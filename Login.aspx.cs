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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Password & Email will be evaluated by api
           
            if(!Password.Value.Equals("") && !email.Value.Equals(""))
            {

                string hashedPassword = Password.Value; 
                string LoginURI = SITEConstants.BASE_URL +"api/User/Login/"+email.Value+ "/" + hashedPassword;
                //string JsonLoginBody = "{\"id\": 0,\"fullNames\":\"0\",\"surname\":\"0\",\"valid_Phone_Number\":\"0\",\"email_Address\":\""+email.Value +"\",\"gender\":\"0\",\"Image\":\"null\",\"passWord\" : \""+hashedPassword+ "\",\"age\":0,\"User_Discriminator\":\"0\"}";                             
                string JsonStringUserObject = ApiComnunication.getJsonEntities(LoginURI);

                User responseUserObj = JsonConvert.DeserializeObject<User>(JsonStringUserObject);

                if (responseUserObj != null)
                {

                    //Check if aaccount is active
                    if (responseUserObj.isActive < 1)
                    {
                        Response.Redirect("Confirmed.aspx?ConfirmatioType=AccountDeactivation");
                    }
                    HttpContext.Current.Session["Email"] = responseUserObj.Email_Address;
                    //HttpContext.Current.Session["Email"] = responseUserObj.Id;
                    //Session["ID"] = responseUserObj.Id;
                    HttpContext.Current.Session["ID"] = responseUserObj.Id;
                    //ID Cookie
                    HttpCookie IDcookie = new HttpCookie("ID");
                    IDcookie.Value = "" + responseUserObj.Id;
                    IDcookie.Expires = DateTime.Now.AddDays(3);
                    
                    //Email cookie
                    HttpCookie userInfo = new HttpCookie("Email");
                    userInfo.Value = responseUserObj.Email_Address;
                    userInfo.Expires = DateTime.Now.AddDays(1);
               
                    HttpContext.Current.Session["UserStatus"] = responseUserObj.User_Discriminator;
                    //Descriminator cookie
                    HttpCookie descookie = new HttpCookie("UserStatus");
                    descookie.Value = "" + responseUserObj.User_Discriminator;
                    descookie.Expires = DateTime.Now.AddDays(3);
                    //check if the user is a tutor               
                        if (responseUserObj.User_Discriminator == "Tutor")
                        {
                            //check if the Tutor is verified
                            string respons = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/isTutorAccepted/" + HttpContext.Current.Session["ID"].ToString());
                            if (respons.Equals("0"))
                                Response.Redirect("ApplicationPending.aspx");                 
                        }

                        // send to dashboard if manager
                    if (responseUserObj.User_Discriminator.Equals("Manager"))
                        Response.Redirect("ManagerDashboard.aspx");
                    Response.Redirect("Home.aspx");
                }
                else 
                {                   
                    ErrorMsgBox.Visible = true;
                    //ErrorMsgBox.InnerText = "Either Email or PassWord is incorrect";
                }                
            }
            else
            {
                ErrorMsgBox.Visible = true;
            }            

        }
    }
}