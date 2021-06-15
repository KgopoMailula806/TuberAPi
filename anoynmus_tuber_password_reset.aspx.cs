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
    public partial class anoynmus_tuber_password_reset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["timeStamp"] != null)
            {
                DateTime linkTime = Convert.ToDateTime(Request.QueryString["timeStamp"].ToString());
                DateTime currentTime = DateTime.Now;

                int difference = Math.Abs((linkTime - currentTime).Minutes);
                if (difference > 10)
                {
                    Response.Redirect("ExpiredLink.aspx"); // send to this page if the link expired

                }
                else
                {
                    if (Request.QueryString["email"] != null)
                    {
                        email.Value = Request.QueryString["email"].ToString().ToLower();
                    }
                }

            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (new_password.Value.Equals(confirm_new_password.Value))
            {
                string userUrl = SITEConstants.BASE_URL + "api/User/getUserByEmail/" + Request.QueryString["email"].ToString().ToLower();
                string emailResponse = ApiComnunication.getJsonEntities(userUrl);

                if (emailResponse.Length > 0) // get user obj start changes
                {
                    User currentUser = JsonConvert.DeserializeObject<User>(emailResponse);
                    User newUser = new User();

                    newUser = currentUser;
                    newUser.PassWord = new_password.Value; // new password

                    //newUser.Age = currentUser.Age;
                    //newUser.FullNames = currentUser.FullNames + " Change";
                    //newUser.Surname = currentUser.Surname;
                    //newUser.User_Discriminator = currentUser.User_Discriminator;
                    //newUser.Valid_Phone_Number = currentUser.Valid_Phone_Number;
                    //newUser.isActive = currentUser.isActive;
                    //newUser.Id = currentUser.Id;
                    //newUser.Image = currentUser.Image;
                    //newUser.Email_Address = currentUser.Email_Address;
                    //newUser.Gender = currentUser.Gender;

                    string newJsonUser = JsonConvert.SerializeObject(currentUser); // make json object

                    string url = SITEConstants.BASE_URL + "api/User/editDetails/" + currentUser.Id;
                    string changeResponse = ApiComnunication.PutEntity(url, newJsonUser);

                    if (changeResponse.Length > 0)
                    {
                        // new user from db
                        User changedUser = JsonConvert.DeserializeObject<User>(changeResponse);
                        if (changedUser.PassWord.Equals(new_password.Value)) // checking if password has changed
                        {
                            body.Visible = false; // hide body
                            alert.Visible = true; // show msg
                        }

                    }

                }

            }

        }
    }
}