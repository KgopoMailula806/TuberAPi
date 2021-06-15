using Newtonsoft.Json;
using QuadCore_Website.HelperFunctionality;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuberAPI.models;

namespace QuadCore_Website
{
    public partial class UserRegistration : System.Web.UI.Page
    {
      
        private static string UserStatus = "";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
                if (Request.QueryString["As"] != null)
                {
                    UserStatus = Request.QueryString["As"];

                    if (Session["Email"] != null)
                    {
                        UserCrederntialTable1.Visible = false;
                        UserCrederntialTable2.Visible = false;
                        switch (UserStatus)
                        {
                            case "Manager":
                                ManagerCredentialTable.Visible = true;
                                SignUph1Tag.InnerText += " As Manager";
                                break;
                            case "Client":
                                ClientCredentialTable.Visible = true;
                                SignUph1Tag.InnerText += " As Client";
                                break;
                            case "Tutor":
                                TutorCredentialTable.Visible = true;
                                SignUph1Tag.InnerText += " As Tutor";
                                break;
                        }
                    }
                    else
                    {
                        UserCrederntialTable1.Visible = true;
                        UserCrederntialTable2.Visible = true;
                        switch (UserStatus)
                        {
                            case "Manager":
                                ManagerCredentialTable.Visible = true;
                                SignUph1Tag.InnerText += " As Manager";
                                break;
                            case "Client":
                                ClientCredentialTable.Visible = true;
                                SignUph1Tag.InnerText += " As Client";
                                break;
                            case "Tutor":
                                TutorCredentialTable.Visible = true;
                                SignUph1Tag.InnerText += " As Tutor";
                                break;
                        }
                    }
                }
                else
                {
                    UserCrederntialTable1.Visible = false;
                    UserCrederntialTable2.Visible = false;
                    ManagerCredentialTable.Visible = false;
                    ClientCredentialTable.Visible = false;
                    TutorCredentialTable.Visible = false;

                }
            
            
            
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Register_Click(object sender, EventArgs e)
        {
            switch (UserStatus)
            {
                case "Manager":
                    
                    if (RegisterManager()) // if registering the manager was succesful
                    {
                        Session["UserStatus"] = "Manager";
                        Response.Redirect("Home.aspx");
                    }
                    else //Or if not
                    {

                    }
                    break;
                case "Client":
                    //display  tutor concerned credentials
                    if (RegisterClient())// if registering the Tutor was succesful
                    {
                        // if the person registering is a client
                        Session["UserStatus"] = "Tutor";
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {

                    }
                    break;
                case "Tutor":
                    //display  client concerned credentials
                    if (RegisterTutor())
                    {
                        // if the person registering is a Tutor
                        Session["UserStatus"] = "Client";
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {

                    }
                    break;
            }
        }

        /// <summary>
        ///  Register the manager
        /// </summary>
        /// <returns>boolean if registration was successful</returns>
        protected bool RegisterManager()
        {
            //TODO
            // get user data 
            if (Session["Email"] != null) //if user is logged in (i.e. already has an account)
            {
                //get account details
                string Url = SITEConstants.BASE_URL + "api/User/GetUser/" + Session["ID"];
                //get their user credentials 
                string responseBody = ApiComnunication.getJsonEntities(Url);
                //Get client specific data from the page
                User responseUserObj = JsonConvert.DeserializeObject<User>(responseBody);

                if (responseUserObj != null)
                {
                    //reading in the manager's CV pdf 
                    HttpPostedFile Manager_CV = CV.PostedFile;
                    int cvID = FileFunctionality.upload(Manager_CV);

                    string postManagerURL = SITEConstants.BASE_URL + "api/Manager/AddManager";
                    string managerBody = "{\"Id\":0,\"CVID\": " + cvID + " \"user_Table_Reference\":" + responseUserObj.Id + "}";
                    string responseManagerString = ApiComnunication.postJsonEntitie(postManagerURL, managerBody);

                    Manager responseManagerObj = JsonConvert.DeserializeObject<Manager>(responseManagerString);
                    if (responseManagerObj != null)
                    {

                        
                        Session["Email"] = responseUserObj.Email_Address;
                        Session["ID"] = responseUserObj.Id;
                        Session["UserStatus"] = responseUserObj.User_Discriminator;
                        Response.Redirect("Home.aspx");
                        return true;
                    }
                    else
                    {
                        Response.Redirect("ErrorPage.aspx?Error=CouldNotRegisterASManager");
                        return false;
                    }

                }
                else { return false; }
            }
            else
            {

                string reponseBody = insertUserTableCredentials();
                //Convert Json body to User object
                User responseUserObj = JsonConvert.DeserializeObject<User>(reponseBody);
                
                if (responseUserObj != null)
                {
                    //reading in the manager's CV pdf 
                    HttpPostedFile Manager_CV = CV.PostedFile;

                    Stream ManagerCV_Stream = Manager_CV.InputStream;
                    BinaryReader ManagerCvBinReader = new BinaryReader(ManagerCV_Stream);
                    byte[] FileInBytes = ManagerCvBinReader.ReadBytes((Int32)ManagerCV_Stream.Length);
                    string ManagerCv_StringFormat = Convert.ToBase64String(FileInBytes);


                    string postManagerURL = SITEConstants.BASE_URL + "api/Manager/AddManager";
                    string managerBody = "{\"CV\":\" "+ManagerCv_StringFormat+" \"\"user_Table_Reference\":" + responseUserObj.Id + "}";
                    string responseManagerString = ApiComnunication.postJsonEntitie(postManagerURL, managerBody);

                    Manager responseManagerObj = JsonConvert.DeserializeObject<Manager>(responseManagerString);
                    if (responseManagerObj != null)
                    {

                        Session["Email"] = responseUserObj.Email_Address;
                        Session["ID"] = responseUserObj.Id;
                        Session["UserStatus"] = responseUserObj.User_Discriminator;
                        Response.Redirect("Home.aspx");
                        return true;
                    }
                    else
                    {
                        Response.Redirect("ErrorPage.aspx?Error=CouldNotRegisterASManager");
                        return false;
                    }
                    
                }
                else { return false; }
            }     
        }

        /// <summary>
        ///  Register Usera as a client
        /// </summary>
        /// <returns>boolean if registration was successful</returns>
        protected bool RegisterClient()
        {
            if (Session["Email"] != null ) //if user is logged in (i.e. already has an account)
            {
                //get account details
                string Url = SITEConstants.BASE_URL+"api/User/GetUser/" +Session["ID"];
                //get their user credentials
                string responseBody = ApiComnunication.getJsonEntities(Url);
                //Get client specific data from the page
                User responseUserObj = JsonConvert.DeserializeObject<User>(responseBody);

                if (responseUserObj != null)
                {
                    string clientCurentGrade = level.SelectedValue;
                    string institution = school.Value;

                    string ClientUrl = SITEConstants.BASE_URL + "api/Client/AddClient";
                    string JsonClientString = "{\"Current_Grade\" : \"" + clientCurentGrade + "\",\"Institution\" : \"" + institution + "\",\"User_Table_Reference\" : " + responseUserObj.Id + "}";

                    string responseClientString = ApiComnunication.postJsonEntitie(ClientUrl, JsonClientString);
                    Manager responsClientObj = JsonConvert.DeserializeObject<Manager>(responseClientString);

                    if (responsClientObj != null)
                    {
                        
                        Session["Email"] = responseUserObj.Email_Address;
                        Session["ID"] = responseUserObj.Id;
                        Session["UserStatus"] = responseUserObj.User_Discriminator;
                        Response.Redirect("Home.aspx");
                        return true;
                    }
                    else
                    {
                        Response.Redirect("ErrorPage.aspx?Error=CouldNotRegisterASClient");
                        return false;
                    }
                }
                else { return false; }

            }
            else //if this is a new User
            {
                string reponseBody = insertUserTableCredentials();
                //Convert Json body to User object
                User responseUserObj = JsonConvert.DeserializeObject<User>(reponseBody);

                if (responseUserObj != null)
                {
                    string clientCurentGrade = level.SelectedValue;
                    string institution = school.Value;

                    string ClientUrl = SITEConstants.BASE_URL + "api/Client/AddClient";
                    string JsonClientString = "{\"Current_Grade\" : \"" + clientCurentGrade + "\",\"Institution\" : \"" + institution + "\",\"User_Table_Reference\" : " + responseUserObj.Id + "}";

                    string responseClientString = ApiComnunication.postJsonEntitie(ClientUrl, JsonClientString);
                    Manager responsManagerObj = JsonConvert.DeserializeObject<Manager>(responseClientString);

                    if (responsManagerObj != null)
                    {
                        
                        Session["Email"] = responseUserObj.Email_Address;
                        Session["ID"] = responseUserObj.Id;
                        Session["UserStatus"] = responseUserObj.User_Discriminator;
                        Response.Redirect("Home.aspx");
                        return true;
                    }
                    else
                    {
                        Response.Redirect("ErrorPage.aspx?Error=CouldNotRegisterASClient");
                        return false;
                    }
                }
                else { return false; }
                
            }
        }

        /// <summary>
        ///  Takes the data in 
        /// </summary>
        /// <returns>boolean if registration was successful</returns>
        protected bool RegisterTutor()
        {
            if (Session["Email"] != null) //if user is logged in (i.e. already has an account)
            {
                //get account details
                string Url = SITEConstants.BASE_URL + "api/User/GetUser/" + Session["ID"];
                //get their user credentials 
                string responseBody = ApiComnunication.getJsonEntities(Url);
                
                User responseUserObj = JsonConvert.DeserializeObject<User>(responseBody);

                if (responseUserObj != null)
                {

                    //Send the Json body
                    string AddTutorUrl = SITEConstants.BASE_URL + "api/Tutor/AddTutor";
                    string Jsonbody = "{\"Id\":0,\"is_Accepted\": 0,\"user_Table_Reference\":" + responseUserObj.Id + "}";
                    string responseTutorBody = ApiComnunication.postJsonEntitie(AddTutorUrl, Jsonbody);

                    Tutor responseTutorObj = JsonConvert.DeserializeObject<Tutor>(responseTutorBody);

                    if (responseTutorObj != null)
                    {
                        

                        Session["Email"] = responseUserObj.Email_Address;
                        Session["ID"] = responseUserObj.Id;
                        sendTutorDoccies();
                        Session["UserStatus"] = responseUserObj.User_Discriminator;
                        Response.Redirect("Home.aspx");
                        return true;
                    }
                    else
                    {
                        Response.Redirect("ErrorPage.aspx?Error=CouldNotRegisterASTutor");
                        return false;
                    }


                }
                else { return false; }

            }
            else //if this is a new User
            {               
                string reponseBody = insertUserTableCredentials();
                //Convert Json body to User object
                User responseUserObj = JsonConvert.DeserializeObject<User>(reponseBody);
                
                if (responseUserObj != null )
                {
                    //sendTutorDoccies();
                    //Send the Json body
                    string AddTutorUrl = SITEConstants.BASE_URL + "api/Tutor/AddTutor";
                    string Jsonbody = "{\"Id\":0,\"is_Accepted\": 0,\"user_Table_Reference\":" + responseUserObj.Id + "}";
                    string responseTutorBody = ApiComnunication.postJsonEntitie(AddTutorUrl, Jsonbody);

                    Tutor responseTutorObj = JsonConvert.DeserializeObject<Tutor>(responseTutorBody);

                    if (responseTutorObj != null)
                    {
                        
                        Session["Email"] = responseUserObj.Email_Address;
                        Session["ID"] = responseUserObj.Id;
                        sendTutorDoccies();
                        Session["UserStatus"] = responseUserObj.User_Discriminator;
                        Response.Redirect("Home.aspx");
                        return true;
                    }
                    else
                    {
                        Response.Redirect("ErrorPage.aspx?Error=CouldNotRegisterASTutor");
                        return false;
                    }

                    
                }
                else { return false; }

            }       
        }

        /// <summary>
        ///     inserts the details into the user table
        /// </summary>
        /// <returns>The newly inserted data</returns>
        protected string insertUserTableCredentials()
        {
            //Api address
            string responseBody = "";
            if (Password.Value.Equals(confirm_password.Value))
            {
                // check is the password match
                // check if email and password already exists
                //record Data for the Users table

                //*FillName(s)*
                string full_names = Full_Names.Value;
                //*Surname*
                string surname = Surname.Value;
                //*Phone Number*
                string valid_number = Valid_Phone_Number.Value;
                //*email*
                string email = Email.Value;

                //***** check if password is taken
                //string JsonEmailBody = "{\"id\": 0,\"fullNames\":\"null\",\"surname\":\"null\",\"valid_Phone_Number\":\"null\",\"email_Address\":\""+ email + "\",\"gender\":\"null\",\"Image\":\"null\",\"password\" : \"null\",\"age\":21,\"user_Discriminator\":\"Manager\"}";
              
                string JsonStringUserObject = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/CheckIfEmailExists/" + email);
                
                
                if (JsonStringUserObject.Equals("1")) //if there email already exists
                {

                    string DivText = "<p class=\"font-weight-bold\" style=\"color:darkorange\">Email Taken :[</p>";                    
                    EmailDivTage.InnerHtml = DivText;
           
                    return "";
                }
                else
                {
                    string DivText = "<p class=\"font-weight-bold\">Email</p>";
                    EmailDivTage.InnerHtml = DivText;
                }

                //*Gender*
                string gender = GenderDropDown.SelectedValue;

                //*Image*
                ///string profileImageString = " ";
                int imgID = 0;
                HttpPostedFile profileImage = profileImagefile.PostedFile;
                if (profileImage != null)
                {
                    if (profileImage.FileName != "")
                    {
                        imgID = FileFunctionality.upload(profileImage);
                        if (imgID == -1)
                            imgID = 0;

                    }
                    else
                    {
                        profileImageLableTag.InnerHtml = "<span style=\"color:darkorange\">No image chosen</span>";
                        return "";
                    }
                }                

                string age = Age.Value;
                string password = Password.Value;
                
                //***** check if password is taken
               // string JsonPasswordBody = "{\"id\": 0,\"fullNames\":\"null\",\"surname\":\"null\",\"valid_Phone_Number\":\"null\",\"email_Address\":\"null\",\"gender\":\"null\",\"Image\":\"null\",\"password\" : \"" + password + "\",\"age\":21,\"user_Discriminator\":\"Manager\"}";
                                
                JsonStringUserObject = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/User/CheckIfPasswordExists/" + password);              

                if (JsonStringUserObject.Equals("1"))//if there email already exists
                {
                    string DivText = "<p class=\"font-weight-bold\" style=\"color:darkorange\">Password Taken :[</p>";
                    passwordDivTage.InnerHtml = DivText;
                    return "";
                }
                else
                {
                    string DivText = "<p class=\"font-weight-bold\">Password</p>";
                    passwordDivTage.InnerHtml = DivText;
                }

                //*** submit the JsonBody to the database
                string JsonBody = " {\"Id\":0,\"FullNames\": \"" + full_names + "\",\"Surname\": \"" + surname + "\",\"Valid_Phone_Number\": \"" + valid_number + "\",\"Email_Address\": \"" + email + "\",\"Gender\": \"" + gender + "\",\"age\": " + age + ",\"Image\":" + imgID + ",\"PassWord\": \"" + password + "\",\"user_Discriminator\":\"" + UserStatus + "\",\"isActive\": 1}";
               
                string urlPath = SITEConstants.BASE_URL + "api/User/AddUser";
                responseBody = ApiComnunication.postJsonEntitie(urlPath, JsonBody); //This method is not from an external library it is within our solution files (Author: Kgopo)                                               
            }
            else
            {                
                passDiv.Visible = true;
            }  
            return responseBody;
        }      
    
        private void sendTutorDoccies()
        {
            //reading in the academic record pdf and converti it to string format 
            HttpPostedFile httpacademic_transcript = Academic_Transcript.PostedFile;
            int arID = FileFunctionality.TutorUpload(Session["ID"].ToString(), "Academic Record", httpacademic_transcript);

            //reading in the police clearance pdf and coverting it to string format
            HttpPostedFile policeClearance = PoliceClearance.PostedFile;
            int pcID = FileFunctionality.TutorUpload(Session["ID"].ToString(), "Police Clearance", policeClearance);

            HttpPostedFile CV = TutorCv.PostedFile;
            int cvID = 0;

            if (CV != null)
                cvID = FileFunctionality.TutorUpload(Session["ID"].ToString(), "Tutor CV", CV);
        }
    }
}