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
    public partial class EditProfile : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["bariId"] != null)
                {
                    string url = SITEConstants.BASE_URL + "api/User/GetUser/" + Request.QueryString["bariId"].ToString();
                    string oldUser = ApiComnunication.getJsonEntities(url);

                    if (oldUser.Length > 0)
                    {
                        User objOld = JsonConvert.DeserializeObject<User>(oldUser);
                        Full_Names.Value = objOld.FullNames;
                        Surname.Value = objOld.Surname;
                        Valid_Phone_Number.Value = objOld.Valid_Phone_Number;
                        gender.SelectedValue = objOld.Gender;

                        if (objOld.User_Discriminator.Equals("Client"))
                        {
                            string clientUrl = SITEConstants.BASE_URL + "/api/Client/GetClientByForeignKey/" + Request.QueryString["bariId"].ToString();
                            string clientJson = ApiComnunication.getJsonEntities(clientUrl);

                            if (clientJson.Length > 0)
                            {
                                Client client = JsonConvert.DeserializeObject<Client>(clientJson);
                                level.SelectedValue = client.Current_Grade;
                                institution.Value = client.Institution;

                            }
                        }

                    }
                }

            }
            

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string url = SITEConstants.BASE_URL + "api/User/GetUser/" + Request.QueryString["bariId"].ToString();
            string oldUser = ApiComnunication.getJsonEntities(url);

            if (oldUser.Length > 0)
            {
                User old = JsonConvert.DeserializeObject<User>(oldUser);

                User user = old;
                user.Valid_Phone_Number = Valid_Phone_Number.Value;
                user.FullNames = Full_Names.Value;
                user.Surname = Surname.Value;
                user.Gender = gender.SelectedValue;

                string userJson = JsonConvert.SerializeObject(user);
                string changeUserUrl = SITEConstants.BASE_URL + "api/User/editDetails/" + Request.QueryString["bariId"].ToString();
                string userResponse = ApiComnunication.PutEntity(changeUserUrl, userJson);

                if (userResponse.Length > 0)
                {
                    if (old.User_Discriminator.Equals("Client"))
                    {
                        string clientUrl = SITEConstants.BASE_URL + "/api/Client/GetClientByForeignKey/" + Request.QueryString["bariId"].ToString();
                        string clientJson = ApiComnunication.getJsonEntities(clientUrl);

                        if (clientJson.Length > 0)
                        {
                            Client client = JsonConvert.DeserializeObject<Client>(clientJson);

                            Client newClient = client;
                            newClient.Institution = institution.Value;
                            newClient.Current_Grade = level.SelectedValue;

                            string clientJson_ = JsonConvert.SerializeObject(newClient);
                            string clientResponse = ApiComnunication.PutEntity(SITEConstants.BASE_URL + "api/Client/editDetails/" + client.Id, clientJson_);

                            if (clientResponse.Length > 0)
                            {
                                if (newClient.Institution.Equals(institution.Value) && newClient.Current_Grade.Equals(level.SelectedValue))
                                {
                                    body.Visible = false;
                                    alert.Visible = true;
                                }

                            }
                        }
                    }
                }
            }
            
        }
    }
}