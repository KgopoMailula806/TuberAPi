using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuadCore_Website
{
    public class HelperMethods
    {
		/// <summary>
		/// this method removes the surrounding [] bracket from the json string 
		/// in preperation for deserialization
		/// </summary>
		/// <param name="jsonBody"></param>
		/// <param name="Uri"></param>
		/// <returns></returns>
		public static string MakeDeserializable(string jsonBody)
		{
			if (jsonBody.Length >0)
			{
				if (jsonBody[0] == '[')
				{
					jsonBody = jsonBody.Remove(0, 1);
					jsonBody = jsonBody.Remove(jsonBody.Length - 1, 1);
				}
				else return jsonBody; //If the string doesn't need to be broken down 
			}

			

			return jsonBody;
		}
		public static string MakeDeserializable(string jsonBody, char c)
		{
			if (jsonBody.Length > 0)
			{
				if (jsonBody[0] == c)
				{
					jsonBody = jsonBody.Remove(0, 1);
					jsonBody = jsonBody.Remove(jsonBody.Length - 1, 1);
				}
				else return jsonBody; //If the string doesn't need to be broken down 
			}


			return jsonBody;
		}
		public static string[] separateString(string text)
		{
			
			char[] seperator = { '-', ',', '_',':' };
			string[] parts = text.Split(seperator);

			return parts;
		}

		public static string[] separateString(string text,char char_)
		{

			char[] seperator = { char_ };
			string[] parts = text.Split(seperator);

			return parts;
		}

	}
}