using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TuberAPI.models;

namespace QuadCore_Website.HelperFunctionality
{
    public class FileFunctionality
    {
        public static int upload(HttpPostedFile file)
        {
            if(file != null)
            {
                //Post method must bring the files id

                BinaryReader br = new BinaryReader(file.InputStream);
                byte[] fileInsides = br.ReadBytes(file.ContentLength);

                string fileBody = "{\"Id\": 0," +
                                   "\"size\": "+ file.ContentLength +"," +
                                   "\"extension\": \" " + Path.GetExtension(file.FileName) + "\"," +
                                   "\"documentData\": \"" + Convert.ToBase64String(fileInsides) +"\"}";

                string url = SITEConstants.BASE_URL+"api/Document/UploadDocument/";

                string response = ApiComnunication.postJsonEntitie(url, fileBody);
                return Convert.ToInt32(response);
            }

            return -1;
        }

        public static int TutorUpload(string userID, string type, HttpPostedFile file)
        {
            //1: file save
            //0: file updated
            //-1: fail

            int fileID = upload(file);
            int tutorID = ModuleFunctionality.getTutor(userID).Id;

            if (fileID != -1)
            {
                string url = SITEConstants.BASE_URL+"api/TutorDocument/UploadTutorDocument/";

                string fileBody = "{\"Id\": 0," +
                                   "\"DocumentType\": \"" + type + "\"," +
                                   "\"TutorID\": " + tutorID + "," +
                                   "\"DocID\": " + fileID + "}"; 

                string response = ApiComnunication.postJsonEntitie(url,fileBody);
                return Convert.ToInt32(response);
            }

            return -1;
        }
        

        public static Document GetFile(int fileID)
        {
            string url = SITEConstants.BASE_URL+"api/Document/GetDocument/" + fileID;
            string response = ApiComnunication.getJsonEntities(url);

            if (response != null)
            {
                Document file = JsonConvert.DeserializeObject<Document>(response);
                return file;
            }

            return null;
        }
    }
}