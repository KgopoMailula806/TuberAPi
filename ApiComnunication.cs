using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace QuadCore_Website
{
    public class ApiComnunication
    {

        /// <summary>
        ///  Give it a URL path to a get metod in the api and returns the items or item
        /// </summary>
        /// <param name="UriPath">Api method url</param>
        /// <returns>The Json format of the object(s) retreived from the database</returns>
        public static string getJsonEntities(string UriPath)
        {
            string urlRequest = string.Format(UriPath);
            WebRequest requestObject = WebRequest.CreateHttp(urlRequest);
            requestObject.Method = "GET";

            //in the case that they are required
            //requestObject.Credentials = new NetworkCredential("Username", "Password");

            HttpWebResponse responseObject = null;
            responseObject = (HttpWebResponse)requestObject.GetResponse();

            string strResulttest = null;

            //Redeading the Json object 
            using (Stream stream = responseObject.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(stream);
                strResulttest = streamReader.ReadToEnd();
                streamReader.Close();

            }
            return strResulttest;
        }

        /// <summary>
        ///  get json object with a [fromBody] parameter
        /// </summary>
        /// <param name="UriPath">Api method url</param>
        /// <returns>The Json format of the object(s) retreived from the database</returns>
        public static string getJsonEntitiesWithJsonBody(string UriPath, string json)
        {
            WebRequest requestObjPost = WebRequest.CreateHttp(string.Format(UriPath));
            requestObjPost.Method = "GET";
            requestObjPost.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(requestObjPost.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)requestObjPost.GetResponse();
                var result2 = "";
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result2 = streamReader.ReadToEnd();
                    streamReader.Close();
                }

                return result2.ToString();
            }
        }


        /// <summary>
        ///  check if a object can matches one in the database 
        /// </summary>
        /// <param name="UriPath"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string CheckIfObjectExistsInDB(string UriPath, string body)
        {
            WebRequest requestObjPost = WebRequest.CreateHttp(string.Format(UriPath));
            requestObjPost.Method = "POST";
            requestObjPost.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(requestObjPost.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)requestObjPost.GetResponse();
                var result2 = "";
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result2 = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                return result2.ToString();
            }

        }


        /// <summary>
        /// This method takes whatever POST url request and parses the json body for database insertion
        /// </summary>
        /// <param name="UriPath">api method Url</param>
        /// <param name="body">Json string body</param>
        /// <returns>The Json body of the newly inserted data</returns>
        public static string postJsonEntitie(string UriPath, string body)
        {           
            WebRequest requestObjPost = WebRequest.CreateHttp(string.Format(UriPath));
            requestObjPost.Method = "POST";
            requestObjPost.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(requestObjPost.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)requestObjPost.GetResponse();
                var result2 = "";
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result2 = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                
                return result2.ToString();
            }
            
        }

        /// <summary>
        /// This method takes whatever PUT url request and parses the json body for updating what is in the database
        /// </summary>
        /// <param name="UriPath">api method Url</param>
        /// <param name="body">Json string body</param>
        /// <returns>The updated Json body from the database</returns>
        public static string PutEntity(string UriPath, string body)
        {
            WebRequest requestObjPut = WebRequest.CreateHttp(string.Format(UriPath));
            requestObjPut.Method = "PUT";
            requestObjPut.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(requestObjPut.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)requestObjPut.GetResponse();
                var result2 = "";

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result2 = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                return result2.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UriPath"></param>
        /// <param name=""></param>
        /// <returns>Entity the has be deleted in the database</returns>
        public static string DeleteEntity(string UriPath, string body)
        {
            WebRequest requestObjPut = WebRequest.CreateHttp(string.Format(UriPath));
            requestObjPut.Method = "DELETE";
            requestObjPut.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(requestObjPut.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)requestObjPut.GetResponse();
                var result2 = "";

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result2 = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                return result2.ToString();
            }
        }

        public static string DeleteByURLEntity(string UriPath)
        {      
            
            WebRequest requestObject = WebRequest.CreateHttp(UriPath);

            requestObject.Method = "DELETE";
            requestObject.ContentType = "application/json";

            HttpWebResponse responseObject = null;
            responseObject = (HttpWebResponse)requestObject.GetResponse();

            string strResulttest = null;

            //Redeading the Json object 
            using (Stream stream = responseObject.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(stream);
                strResulttest = streamReader.ReadToEnd();
                streamReader.Close();

            }
            return strResulttest;
        }
       
    }
}