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
    public partial class ExitingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["bariId"] != null)
            {
                string userUrl = SITEConstants.BASE_URL + "api/User/GetUser/" + Request.QueryString["bariId"].ToString();
                string userJson = ApiComnunication.getJsonEntities(userUrl);

                if(userJson.Length > 0)
                {
                    User user = JsonConvert.DeserializeObject<User>(userJson);
                    if(user.isActive != 0)
                    {
                        string deactivateURL = SITEConstants.BASE_URL + "api/User/DeactivateAccount/" + Request.QueryString["bariId"].ToString();

                        string Jsonbody = "{\"id\":0,\"userID\": " + Request.QueryString["bariId"].ToString() + ",\"content\":\"" + Request.QueryString["resContent"].ToString() + "\"}";
                        string response = ApiComnunication.postJsonEntitie(deactivateURL, Jsonbody);

                        if (response.Equals("1"))
                        {
                            if (Request.Cookies["Email"] != null)
                            {
                                Response.Cookies["Email"].Expires = DateTime.Now.AddDays(-1);

                            }

                        }
                    }
                    else
                    {
                        Response.Redirect("ErrorPage.aspx");
                    }
                }
                
            }
            
            
        }
    }
}