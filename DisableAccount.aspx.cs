using Newtonsoft.Json;
using QuadCore_Website.HelperFunctionality;
using QuadCore_Website.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuberAPI.models;

namespace QuadCore_Website
{
    public partial class DisableAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


                string userBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/getActiveUsers");
                string display = "";


                if (userBody != "")
                {
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(userBody);

                    foreach (User us in users)
                    {
                        display += "<tr>";
                        display += "<td>" + us.FullNames + "</td>";
                        display += "<td>" + us.Surname + "</td>";
                        display += "<td>" + us.Email_Address + "</td>";
                        display += "<td>" + us.User_Discriminator + "</td>";
                        display += "</tr>";

                    }

                    tableContent.InnerHtml = display;
                }

            

        }

        protected void btnDisableAcc_Click(object sender, EventArgs e)
        {
            if (Session["Email"] != null || Response.Cookies["Email"] != null)
            {
                if (!email.Value.Equals(""))
                {
                    string checkUser = SITEConstants.BASE_URL + "api/User/getUserByEmail/" + email.Value;
                    string JsonStringUserObject = ApiComnunication.getJsonEntities(checkUser);

                    User existUser = JsonConvert.DeserializeObject<User>(JsonStringUserObject);

                    if (existUser != null)
                    {
                        //GetUserID and user reason
                        Reason res = new Reason();
                        res.Id = 0;
                        res.userID = existUser.Id;
                        res.content = Reason.Value;

                        string Jsonbody = "{\"id\":0,\"userID\": " + res.userID + ",\"content\":\"" + res.content + "\"}";


                        string deactivateURL = SITEConstants.BASE_URL + "api/User/DeactivateAccount/" + existUser.Id;

                        string response = ApiComnunication.postJsonEntitie(deactivateURL, Jsonbody);

                        if (response != null)
                        {
                            success.Visible = true;
                            Reason.Value = "";
                            email.Value = "";

                            string userBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/getActiveUsers");
                            string display = "";

                            if (userBody != "")
                            {
                                List<User> users = JsonConvert.DeserializeObject<List<User>>(userBody);

                                foreach (User us in users)
                                {
                                    display += "<tr>";
                                    display += "<td>" + us.FullNames + "</td>";
                                    display += "<td>" + us.Surname + "</td>";
                                    display += "<td>" + us.Email_Address + "</td>";
                                    display += "<td>" + us.User_Discriminator + "</td>";
                                    display += "</tr>";

                                }

                                tableContent.InnerHtml = display;
                            }

                        }
                    }
                    
                }
            }
        }
    }

}
