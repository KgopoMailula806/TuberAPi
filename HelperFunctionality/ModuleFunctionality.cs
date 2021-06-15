using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using TuberAPI.models;


namespace QuadCore_Website.HelperFunctionality
{
    public class ModuleFunctionality
    {		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
        public static List<Module> getClientsModules(String ID)
        {
			List<Module> clientModules = new List<Module>();
			//Note Session["ID"] is a foreign key to the client table because it's the ID (Pk) of the User Table
			string clientTableId = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/Client/GetTutorId/" + ID);
			
			if (!clientTableId.Equals("0"))
			{
				string ClientModuleBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/Client_Module/getClientModulesByForeignKey/" + clientTableId);
				//Get The client PK(ID)
				//Session["ClientTableID"] = client.Id;

				List<Client_Module> client_Modules = JsonConvert.DeserializeObject<List<Client_Module>>(ClientModuleBody);

				if (client_Modules != null)
				{
					foreach (Client_Module cm in client_Modules)
					{
						string jsonModuleBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/Modules/GetModules/" + cm.ModuleId);
						jsonModuleBody = HelperMethods.MakeDeserializable(jsonModuleBody);
						Module module = JsonConvert.DeserializeObject<Module>(jsonModuleBody);

						if (module != null)
							clientModules.Add(module);

					}
				}
			}
			return clientModules;
        }

		/// <summary>
		///  git this method the UserTableID and the user discriminator
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public static List<Module> getModules(string ID, string userStatus)
		{
			
			if (userStatus.ToString().Equals("Client"))
			{
				//Note Session["ID"] is a foreign key to the client table because it's the ID (Pk) of the User Table
				string clientModules = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/Modules/GetClientModulesByUserTableID/" + ID);
				if (!clientModules.Equals("0"))
				{
					List<Module> modules = JsonConvert.DeserializeObject<List<Module>>(clientModules);
					return modules;
				}


				/*if (!clientTableId.Equals("0"))
				{
					string ClientModuleBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+ "api/Client_Module/getClientModulesByForeignKey/" + clientTableId);
				//Get The client PK(ID)
				//Session["ClientTableID"] = client.Id;
				
					List<Client_Module> client_Modules = JsonConvert.DeserializeObject<List<Client_Module>>(ClientModuleBody);

					if (client_Modules != null)
					{
						foreach (Client_Module cm in client_Modules)
						{
							string jsonModuleBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL +"api/Modules/GetModules/" + cm.ModuleId);
							jsonModuleBody = HelperMethods.MakeDeserializable(jsonModuleBody);
							Module module = JsonConvert.DeserializeObject<Module>(jsonModuleBody);

							if (module != null)
								clientModules.Add(module);

						}
					}
				}
				*/
			}
			else if (userStatus.ToString().Equals("Tutor"))
			{
				//get the tutor table primary key
				string tutorModules = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Modules/GetTutorModulesByUserTableID/" + ID);
				if (!tutorModules.Equals("0"))
				{
					List<Module> modules = JsonConvert.DeserializeObject<List<Module>>(tutorModules);
					return modules;
				}

				/*if (!tutorTableId.Equals("0"))
				{
					string ClientModuleBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL +"api/Tutor_Module/getTutorModulesEntriesByTutor_Reference/" + tutorTableId);
					//Get The client PK(ID)
					//Session["ClientTableID"] = client.Id;

					List<Tutor_Module> tutor_Modules = JsonConvert.DeserializeObject<List<Tutor_Module>>(ClientModuleBody);

					if (tutor_Modules != null)
					{
						foreach (Tutor_Module cm in tutor_Modules)
						{
							string jsonModuleBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Modules/GetModules/" + cm.Module_Reference);
							jsonModuleBody = HelperMethods.MakeDeserializable(jsonModuleBody);
							Module module = JsonConvert.DeserializeObject<Module>(jsonModuleBody);

							if (module != null)
								clientModules.Add(module);

						}
					}
				} */
			}

			return null;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private static Client getClient(string id)
		{
			string clientJsonBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/Client/GetClientByForeignKey/" + id);
			Client client = JsonConvert.DeserializeObject<Client>(clientJsonBody);
			return client;
		}

		public static Tutor getTutor(string id) 
		{
			string tutorJBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/Tutor/GetTutorByForeignKey/" + id);
			Tutor tutor = JsonConvert.DeserializeObject<Tutor>(tutorJBody);
			return tutor;
		}

		public static int addNewModule(string userId, int ModuleID, string userType)
        {
			string url = "";
			string body = "";
			// Both of the 'IsActive' fields are broken when you add for the first time

			if (userType == "Client")
            {
				// check if also I'm a tutor                 
				//string result = GenericHelperFunctionality.checkIfUserHasSecondaryAccount(HttpContext.Current.Session["ID"].ToString(), HttpContext.Current.Session["UserStatus"].ToString());
				string result = GenericHelperFunctionality.checkIfUserHasSecondaryAccount(userId, userType);
				if (result.Equals("1")) // the client is also a tutor
				{
					url = SITEConstants.BASE_URL + "api/Client_Module/AddNewClientModuleBridge_CollisionChecking/" + userId +"/" +userType;
					body = "{\"Id\":0 ,\"DateAssigned\":\"" + DateTime.Today.Date.ToString() + "\",\"isActive\": 1,\"ModuleID\":" + ModuleID + ",\"clientRef\":" + getClient(userId).Id + "}";
				}
				else //Just added the module with  out checking for the other profile
				{
					url = SITEConstants.BASE_URL + "api/Client_Module/AddNewClientModuleBridge/";
					body = "{\"Id\":0 ,\"DateAssigned\":\"" + DateTime.Today.Date.ToString() + "\",\"isActive\": 1,\"ModuleID\":" + ModuleID + ",\"clientRef\":" + getClient(userId).Id + "}";
				}
					
			}
			else if(userType == "Tutor")
            {
				string result = GenericHelperFunctionality.checkIfUserHasSecondaryAccount(userId, userType);
				if (result.Equals("1")) // the tutor is also a client
				{
					url = SITEConstants.BASE_URL + "api/Tutor_Module/AddNewTutorModuleBridge_CollisionChecking/" + userId + "/" + userType;
					body = "{\"Id\":0 ,\"DateAssigned\":\"" + DateTime.Today.Date.ToString() + "\",\"isActive\":1,\"Module_Reference\":" + ModuleID + ",\"Tutor_Reference\":" + getTutor(userId).Id + "}";
				}
				else //Just added the module with  out checking for the other profile
				{
					url = SITEConstants.BASE_URL + "api/Tutor_Module/AddNewTutorModuleBridge/";
					body = "{\"Id\":0 ,\"DateAssigned\":\"" + DateTime.Today.Date.ToString() + "\",\"isActive\":1,\"Module_Reference\":" + ModuleID + ",\"Tutor_Reference\":" + getTutor(userId).Id + "}";
				}
				
			}

			//Added the new UserModule
			string additionResponseBody = ApiComnunication.postJsonEntitie(url,body);
			if (Convert.ToInt32(additionResponseBody) == 1)//if the module has successfully been registered for
				return 1;
			else if (Convert.ToInt32(additionResponseBody) == -1) //if there was some problem
				return -1;
			else if (Convert.ToInt32(additionResponseBody).Equals(0))//if the user user is already registered to the module as a Tutor
				return 0;
			return -2;
		}
		/// <summary>
		/// Remove the modules of the User
		/// </summary>
		/// <param name="userId"> User table Id Of either the Tutor Or Client</param>
		/// <param name="ModuleID"> module Ide</param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static int removeModule(string userId, int ModuleID, string type)
        {
			string url = "";
			string response = "";

			if (type == "Client")
            {
				url = SITEConstants.BASE_URL+"api/Client_Module/removeModule/" + getClient(userId).Id + "_" + ModuleID;
				response = ApiComnunication.getJsonEntities(url);
			}
			else if(type == "Tutor")
            {
				url = SITEConstants.BASE_URL+"api/Tutor_Module/removeTutorModule/" + getTutor(userId).Id + "_" + ModuleID;
				response = ApiComnunication.getJsonEntities(url);
			}

			return Convert.ToInt32(response);
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
        public static List<Module> getTutorModules(String ID)
        {
            return null;
        }

		/// <summary>
		/// Checks if a tutor is register for a certain module
		/// </summary>
		/// <param name="v"></param>
		/// <param name="tutorID"></param>
		/// <returns></returns>
        internal static bool IsTutorRegisteredForThisModule(string moduleBooked, string userID)
        {

			//get the module references that the tutor is regestered for
			//SITEConstants.BASE_URL+ "api/Tutor/GetAllTutorModuleIdsWithUserID/1018"
			string moduleReferences = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/Tutor/GetAllTutorModuleIdsWithUserID/" + userID);
			int[] moduleIds = JsonConvert.DeserializeObject<int[]>(moduleReferences);
			 foreach(int moduleReference in moduleIds)
			{
				if (moduleBooked.Equals(ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/Modules/GetModuleName/" + moduleReference)))
				{
					return true;					
				}
			}			

			return false;
            
        }

		public static List<Module> getModules()
        {
			string url = SITEConstants.BASE_URL+"api/Modules/GetModules/";
			string moduleResponse = ApiComnunication.getJsonEntities(url);

			if(moduleResponse != null)
            {
				return JsonConvert.DeserializeObject<List<Module>>(moduleResponse);
            }

			return null;
        }

		public static int addManagerModule(string name, string type, string code)
        {
			string url = SITEConstants.BASE_URL+"api/Modules/AddModule/";
			string body = "{\"Id\":0 ," +
						  "\"Module_Name\":\""+ name +"\"," +
						  "\"Module_Code\":\""+ code +"\"," +
						  "\"Module_Type\":\""+ type +"\"," +
						  "\"Client_Modules\":null," +
						  "\"Tutor_Modules\":null}";

			string moduleResponse = ApiComnunication.postJsonEntitie(url,body);

			if (moduleResponse != null)
			{
				return JsonConvert.DeserializeObject<Module>(moduleResponse).Id;
			}

			return -1;
        }

		public static int removeModule(string moduleID)
		{
			string url = SITEConstants.BASE_URL+"api/Modules/Delete/" + moduleID;
			string moduleResponse = ApiComnunication.getJsonEntities(url);

			if (moduleResponse != null)
			{
				return JsonConvert.DeserializeObject<Module>(moduleResponse).Id;
			}

			return -1;
		}

		public static int editModule(string id, string name, string type, string code)
        {
			string url = SITEConstants.BASE_URL+"api/Modules/editDetails/" + id;
			string body = "{\"Id\":"+ id +" ," +
						  "\"Module_Name\":\"" + name + "\"," +
						  "\"Module_Code\":\"" + code + "\"," +
						  "\"Module_Type\":\"" + type + "\"," +
						  "\"Client_Modules\":null," +
						  "\"Tutor_Modules\":null}";

			string response = ApiComnunication.PutEntity(url, body);
			if (response != null)
			{
				return JsonConvert.DeserializeObject<Module>(response).Id;
			}
			

			return -1;
        }

		public static Module getModule(int id)
		{
			string url = SITEConstants.BASE_URL + "api/Modules/GetModules/" + id;
			string moduleResponse = ApiComnunication.getJsonEntities(url);

			if (moduleResponse != null)
			{
				return JsonConvert.DeserializeObject<Module>(moduleResponse);
			}
			else
			{
				return null;
			}
		}
	}
}