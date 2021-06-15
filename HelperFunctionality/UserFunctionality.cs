using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuberAPI.models;

namespace QuadCore_Website.HelperFunctionality
{
    public class UserFunctionality
    {
        /// <summary>
        ///  give this the user table id and it returns the 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static User GetUser(string userId)
        {
            string UriPath = SITEConstants.BASE_URL+ "api/User/GetUser/" + userId;
            string userTableResponseBody = ApiComnunication.getJsonEntities(UriPath);

            User userResponseObj = JsonConvert.DeserializeObject<User>(HelperMethods.MakeDeserializable(userTableResponseBody));
            if (userResponseObj != null)
            {
                return userResponseObj;
            }
            else return null;
        }

        /// <summary>
        /// Give the method primary key of the Tutor table not that of the user 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static User GetTutorsUserTableDetails(string userId)
        {
            string UriPath = SITEConstants.BASE_URL+"api/Tutor/GetUserTableDetails/" + userId;
            string userTableResponseBody = ApiComnunication.getJsonEntities(UriPath);

            User userResponseObj = JsonConvert.DeserializeObject<User>(HelperMethods.MakeDeserializable(userTableResponseBody));
            if (userResponseObj != null)
            {
                return userResponseObj;
            }
            else return null;
        }

        /// <summary>
        ///  Give the method primary key of the manager table not that of the user 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static User GetManagersUserTableDetails(string userId)
        {
            string UriPath = SITEConstants.BASE_URL+"api/Manager/getUserTableDetails/" + userId;
            string userTableResponseBody = ApiComnunication.getJsonEntities(UriPath);

            User userResponseObj = JsonConvert.DeserializeObject<User>(HelperMethods.MakeDeserializable(userTableResponseBody));
            if (userResponseObj != null)
            {
                return userResponseObj;
            }
            else return null;
        }

        /// <summary>
        /// Give the method primary key of the Client table not that of the user 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static User GetClientsUserTableDetails(string clientTableId)
        {
            string UriPath = SITEConstants.BASE_URL+"api/Client/GetUserTableDetails/" + clientTableId;
            string userTableResponseBody = ApiComnunication.getJsonEntities(UriPath);
            
            try
            {
                User userResponseObj = JsonConvert.DeserializeObject<User>(HelperMethods.MakeDeserializable(userTableResponseBody));
                if (userResponseObj != null)
                {
                    return userResponseObj;
                }
                else return null;
            }
            catch(Newtonsoft.Json.JsonSerializationException serializationErro)
            {
                //TODO Log Erro to database                 
                Console.WriteLine( serializationErro.GetBaseException());
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tutorTableId"></param>
        /// <returns></returns>
        public static User GetTutorsUserTableDetails2(string tutorTableId)
        {
            string UriPath = SITEConstants.BASE_URL + "api/Tutor/GetUserTableDetails/" + tutorTableId;
            string userTableResponseBody = ApiComnunication.getJsonEntities(UriPath);

            try
            {
                User userResponseObj = JsonConvert.DeserializeObject<User>(HelperMethods.MakeDeserializable(userTableResponseBody));
                if (userResponseObj != null)
                {
                    return userResponseObj;
                }
                else return null;
            }
            catch (Newtonsoft.Json.JsonSerializationException serializationErro)
            {
                //TODO Log Erro to database         
                Console.WriteLine(serializationErro.GetBaseException());
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int deactivateAccount(String userID, String Reason)
        {
            //{
            //"id": 0,
            //"userID": 0,
            //"content": "string"
            string requestBody = "{"+
                                  "\"id\": 0,"+
                                  "\"userID\": "+userID  + ","+
                                  "\"content\": \" " + Reason + "\"" +
                                  "}";

            string UriPath = SITEConstants.BASE_URL + "api/User/DeactivateAccount/" + userID;

            string deactivationResponse = ApiComnunication.postJsonEntitie(UriPath, requestBody);

            switch(deactivationResponse)
            {
                case "1": //success 
                    {
                        return 1;
                    }
                case "0"://failure
                    {
                        return 0;
                    }
                    

            }
            return - 1;

        }       

        ///api/User/DeactivateAccount/{id}
    }


    }
