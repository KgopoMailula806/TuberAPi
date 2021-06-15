using System.ComponentModel.DataAnnotations;

namespace TuberAPI.Data
{
    public class ClientBooking
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Date_Time { get; set; }
        public int isActive { get; set; }
        public int BookingDetails_BookingRequestTable_Reference { get; set; }
        public int Periods { get; set; }
        public string EndTime { get; set; }
        [Required]
        public int Tutor_Table_Reference { get; set; }
        [Required]
        public int Client_Table_Reference { get; set; }
    }
}