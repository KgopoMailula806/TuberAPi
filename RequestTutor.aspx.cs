using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QuadCore_Website.HelperFunctionality;
using System.Data;
using TuberAPI.models;

namespace QuadCore_Website
{
	public partial class RequestTutor : System.Web.UI.Page
	{
		private List<Module> modules = new List<Module>();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{

			if (Session["Email"] != null || Response.Cookies["Email"] != null)
			{
				//Do nothing
			}
			else
				Response.Redirect("Login.aspx");


			if (!IsPostBack)
			{
				//get and load The User's modules		
				if (Session["UserStatus"] != null)
				{
					if (Session["UserStatus"].ToString().Equals("Client"))
					{
						moduleList.Items.Clear();
						//ModuleDropdown.Items.Add("Choose maikhethela")
						//Modules.Items.Add("");
						modules = ModuleFunctionality.getModules(Session["ID"].ToString(), Session["UserStatus"].ToString());
						foreach (Module module in modules)
						{
							string item = module.Module_Name + " : " + module.Id;
							moduleList.Items.Add(new ListItem(item));

							//selected_item.InnerText = Modules.SelectedValue;
							//ddlBooks.Items.Add(new ListItem(sBooks, sBooks));
							if (Request.QueryString["Address"] != null)
								loc_box.Visible = true;
							address.Value = Request.QueryString["Address"];
							if (Request.QueryString["Latitude"] != null)
							{
								//TODO
							}
							if (Request.QueryString["Longitude"] != null)
							{
								//TODO
							}

							//
							if (HttpContext.Current.Session["Tutor"] != null)
							{
								TutorList.Items.Clear();
								TutorList.Items.Add(new ListItem(HttpContext.Current.Session["Tutor"].ToString()));
								tutorsFormGroup.Visible = true;
								TutorList.Visible = true;
								tutorLableTag.InnerText = "Tutors";
							}

						}
						//if (Session["Module"] != null)
						//ModuleDropdown.Value = Session["Module"].ToString();
					}
				}
				else
				{

				}

			}

		}


		/// <summary>
		/// redirect for the user to select a location
		/// </summary>
		protected void locationaSelect_Click(object sender, EventArgs e)
		{
			string selectedModule = moduleList.SelectedItem.Text;
			string[] selectedModuleTokens = HelperMethods.separateString(selectedModule);
			string alue1 = selectedModuleTokens[0];

			if (moduleList.SelectedValue.Equals("Choose maikhethela"))
				HttpContext.Current.Session["Module"] = moduleList.SelectedItem.Value;
			if (date.Value.Length > 0)
				HttpContext.Current.Session["Date"] = date.Value;
			if (time.Value.Length > 0)
				HttpContext.Current.Session["Time"] = time.Value;

			if (TutorList.SelectedIndex > 0)
			{
				HttpContext.Current.Session["Tutor"] = TutorList.SelectedItem.Text;
			}
			Response.Redirect("LocationSelectionPage.aspx");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btn_GetReleventTutors_Click(object sender, EventArgs e)
		{
			string selectedModule = moduleList.SelectedItem.Text;
			string[] selectedModuleTokens = HelperMethods.separateString(selectedModule);
			string alue1 = selectedModuleTokens[0];
			//string alue2 = ModuleDropdown.InnerText;
			if (moduleList.SelectedValue.Equals("Choose maikhethela"))
				HttpContext.Current.Session["Module"] = moduleList.SelectedItem.Value;
			if (date.Value.Length > 0)
				HttpContext.Current.Session["Date"] = date.Value;
			if (time.Value.Length > 0)
				HttpContext.Current.Session["Time"] = time.Value;

			if (moduleList.SelectedValue == "Choose maikhethela")
			{
				//Alert client to choose a module
			}
			else
			{
				string selectedV = selectedModuleTokens[0];
				string listOfUserDetails = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetTutorsByTheRespectiveModuleTheyTutor/" + selectedV);// ModuleDropdown.Items[ModuleDropdown.SelectedIndex].Text)
				List<User> Tutors = JsonConvert.DeserializeObject<List<User>>(listOfUserDetails);

				if (Tutors.Count > 0)
				{
					TutorList.Items.Clear();
					//TutorList.Items.Add(new ListItem("Any : 0"));

					foreach (User tutor in Tutors)
					{
						if (tutor.Id != Int32.Parse(HttpContext.Current.Session["ID"].ToString()))
						{
							//Get tutor ID
							string tutorID = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetTutorId/" + tutor.Id);
							string item = tutor.FullNames + " " + tutor.Surname + " : " + tutorID;
							TutorList.Items.Add(new ListItem(item));
						}
						//else
						//do nothing
					}
					tutorsFormGroup.Visible = true;
					TutorList.Visible = true;
					tutorLableTag.InnerText = "Tutors";
				}
				else
				{
					//Alert the client that the chosen module doesn't have Tutors yet
					tutorLableTag.InnerText = "Sorry :(There are no tutors available for the chosen module";
					tutorsFormGroup.Visible = true;
					TutorList.Visible = false;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btn_previousTutors_Click(object sender, EventArgs e)
		{
			string selectedModule = moduleList.SelectedItem.Text;
			string[] selectedModuleTokens = HelperMethods.separateString(selectedModule);

			if (moduleList.SelectedValue.Equals("Choose maikhethela"))
				Session["Module"] = moduleList.SelectedValue;
			if (date.Value.Length > 0)
				Session["Date"] = date.Value;
			if (time.Value.Length > 0)
				Session["Time"] = time.Value;
			//retrieve the tutors that where in privious sessions
			if (moduleList.SelectedValue == "Choose maikhethela")
			{
				//Alert client to choose a module
			}
			else
			{
				string selectedV = selectedModuleTokens[0];
				string id = Session["ID"].ToString();
				string listOfUserDetails = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/ClientBooking/GetTutorsByTheRespectiveModuleTheyTutorFromPreviousSession/" + id + "/" + selectedV);

				List<User> Tutors = JsonConvert.DeserializeObject<List<User>>(listOfUserDetails);

				if (Tutors.Count > 0)
				{
					TutorList.Items.Clear();
					//TutorList.Items.Add(new ListItem("Any : 0"));
					foreach (User tutor in Tutors)
					{
						if (tutor.Id != Int32.Parse(HttpContext.Current.Session["ID"].ToString()))
						{
							string tutorID = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetTutorId/" + tutor.Id);
							string item = tutor.FullNames + " " + tutor.Surname + " : " + tutorID;
							TutorList.Items.Add(new ListItem(item));
						}
						tutorsFormGroup.Visible = true;
					}
					//isPreviousTutor = true;
				}
				else
				{
					//Alert the client that the chosen module doesn't have Tutors yet
					tutorLableTag.InnerText = "You have no previous tutors for the chosen module";
					tutorsFormGroup.Visible = true;
					TutorList.Visible = false;
				}

			}
		}

		/// <summary>
		/// Upon submition of the booking request a notification should be sent to the the relevent parties
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			//TODO front end validation

			//
			if (modules == null)
			{
				//Set Alert
				string strMsg = "Please Register for some module";
				string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
				Response.Write(script);
				return;

			}

			//Get the module ID
			string selectedModule = moduleList.SelectedItem.Text;
			if ((selectedModule == null) || selectedModule.Equals(""))
			{
				//Set Alert
				string strMsg = "Please Register for some module";
				string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
				Response.Write(script);
				return;
			}

			string[] selectedModuleTokens = HelperMethods.separateString(selectedModule);
			int moduleID = 0;
			try
			{
				moduleID = Int32.Parse(selectedModuleTokens[1]);
			}
			catch (Exception ex)
			{
				ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Something went wrong');", true);
				Console.WriteLine(ex.GetBaseException());
			}

			//Get The data from the input tags
			//string proposedLocation = location.Value;
			if ((date.Value == null) || date.Value.Equals(""))
			{
				//Set Alert
				string strMsg = "Please please select the date";
				string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
				Response.Write(script);
				return;
			}

			if ((time.Value == null) || time.Value.Equals(""))
			{
				//Set Alert
				string strMsg = "Please enter the time";
				string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
				Response.Write(script);
				return;
			}

			if ((sessionPeriods.Value == null) || sessionPeriods.Value.Equals(""))
			{
				//Set Alert
				string strMsg = "Please enter the number of periods";
				string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
				Response.Write(script);
				return;
			}
			DateTime.Parse(date.Value);
			string proposedDate = (DateTime.Parse(date.Value + " " + time.Value)).ToString();
			string propsedTime = time.Value;
			string endTime = (DateTime.Parse(date.Value + " " + time.Value)).ToString();

			int periods = Int32.Parse(sessionPeriods.Value);
			for (int i = 0; i < periods; i++)
			{
				//calculate the endTime
				endTime = "" + DateTime.Parse(endTime).AddHours(1);
			}

			//for when the notification is sent
			int tutorNotificationReference = 0;

			string[] chosenTutorDetails = HelperMethods.separateString(TutorList.SelectedItem.Text);
			//
			tutorNotificationReference = Int32.Parse(chosenTutorDetails[1]);
tutorNotificationReference = Int32.Parse(ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetUserTableIdByPK/" + Int32.Parse(chosenTutorDetails[1])));
			string tutorID = chosenTutorDetails[1];
			if ((Request.QueryString["Latitude"].Length > 0) && (Request.QueryString["Longitude"].Length > 0))
			{
				string proposedLocation = address.Value + "_" + Request.QueryString["Latitude"] + "_" + Request.QueryString["Longitude"];
				string clientID = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Client/GetTutorId/" + Session["ID"].ToString());
				string JsonBookingRequestBody = "{\"id\": 0,\"requestDate\": \"" + proposedDate + "\",\"requestTime\": \"" + propsedTime + "\",\"periods\": " + periods + ",\"endTime\": \" " + endTime + "\",\"is_Accepted\": 0,\"moduleID1\": " + moduleID + ",\"isRespondedTo\": 0,\"clientProposedLocation\": \"" + proposedLocation + "\",\"tutorProposedLocation\": \"None\",\"subject\": \"" + selectedModule + "\",\"tutor_Reference\": " + tutorID + ",\"client_Reference\":" + clientID + "}";

				//Insert the Data into booking request table 
				string postRequestBookingUri = SITEConstants.BASE_URL + "api/bookingRequest/AddBookingRequest";

				string requestBookingResponseBody = ApiComnunication.postJsonEntitie(postRequestBookingUri, JsonBookingRequestBody);

				BookingRequest bookingRequestObj = JsonConvert.DeserializeObject<BookingRequest>(HelperMethods.MakeDeserializable(requestBookingResponseBody));
				//If the request is successfully recorded
				if (bookingRequestObj != null)
				{
					//set up notification system
					Event bookingEvent = new Event();

					bookingEvent.EventType = "BookingRequest";

					char[] seperator = { ':' };
					string[] moduleParts = moduleList.SelectedValue.Split(seperator);

					//decription 				
					bookingEvent.Description = "Client just requested a Tutorial session_" + moduleParts[0] + "_" + moduleParts[1] + "_" + bookingRequestObj.Id + "_" + Session["ID"].ToString();

					// set the booking request event and return its primary key
					int eventId = Notification_Functionality.setEvent(bookingEvent);
					if (eventId > 0)
					{
						Notification notification = new Notification();
						notification.DatePosted = DateTime.Today.ToString();
						notification.Time = DateTime.Now.TimeOfDay.ToString();
						notification.Seen = 0;
						notification.PersonTheNotificationConcerns = "Tutor";
						notification.User_Table_Reference = tutorNotificationReference;
						notification.Event_Table_Reference = eventId;
						if (Notification_Functionality.setNotification(notification) > 0)
						{
							Response.Redirect("Confirmed.aspx?ConfirmatioType=BookingRequest");
						}
					}
				}
			}
			else
			{
				//Alert user that problem has occured and must fill in the location again
				string strMsg = "Please select a location";
				string script = "<script language=\"javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
				Response.Write(script);
				return;
			}
		}

	}
}