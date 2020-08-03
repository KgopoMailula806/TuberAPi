using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class Tutorial_Session
    {
        [Required]
        public int Id { get; set; }
        public string Session_Date { get; set; }
        public string Session_Start_Time { get; set; }
        public string Session_End_Time { get; set; }
        public int Tutors_Client_Rating { get; set; }
        public int Clients_tutor_Rating { get; set; }
        public string Tutors_Session_FeedBack { get; set; }
        public string Clients_Session_FeedBack { get; set; }
        public string Geographic_Location { get; set; }
        public int IsCompleted { get; set; }
        public string Tutors_Paths { get; set; }
        public string Clients_Paths { get; set; }

        //Foreign Key references     
        [Required]
        [ForeignKey("Client")]
        public int Client_Reference { get; set; }
        [Required]
        [ForeignKey("Module")]
        public int Tutor_Id { get; set; }
        /**/
    }
}
