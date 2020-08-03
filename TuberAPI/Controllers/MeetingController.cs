using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuberAPI.Data;
using TuberAPI.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TuberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private TuberDbContext dbContext;

        public MeetingController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("[action]/{id}")]
        public IActionResult getMeeting(int id)
        {
            var Meet = (from m in dbContext.Meetings
                         where m.TutorID.Equals(id)
                         select m).FirstOrDefault();

          if(Meet != null)
          {
            return Ok(Meet);
          }

            return Ok(null);
        }

        [HttpGet("[action]")]
        public IActionResult getMeetings()
        {
            var meetings = (from m in dbContext.Meetings
                            where m.Attended == 0 && m.Type == "Meeting"
                            select m);

            return Ok(meetings);
        }

        [HttpGet("[action]")]
        public IActionResult getShortlist()
        {
            var meetings = (from m in dbContext.Meetings
                            where m.Attended == 0 && m.Type == "Shortlist"
                            select m);

            return Ok(meetings);
        }

        [HttpGet("[action]")]
        public IActionResult getVerdictObjs()
        {
            List<Meeting> meetings = new List<Meeting>();

            foreach(Meeting m in getAttended())
            {
                if (notAccepted(m.TutorID))
                    meetings.Add(m);
            }

            return Ok(meetings);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name=Module></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult AddMeeting([FromBody] Meeting meeting)
        {
            dbContext.Meetings.Add(meeting);
            dbContext.SaveChanges();
            return Ok(1);
        }

        [HttpPut("[action]/{id}")]
        public IActionResult editMeetingDetails(int id, [FromBody] Meeting meeting)
        {
            var dbMeeting = dbContext.Meetings.Find(id);

            try
            {
                dbMeeting.Attended = meeting.Attended;
                dbMeeting.Date = dbMeeting.Date;
                dbMeeting.Time = dbMeeting.Time;
                dbMeeting.Venue = dbMeeting.Venue;
                dbMeeting.Type = dbMeeting.Type;
                dbMeeting.Minutes = dbMeeting.Minutes;
                dbContext.SaveChanges();
                return Ok(1);
            }
            catch(Exception e)
            {
                string ying = e.Message;                
                return Ok(-1);
            }
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteMeeting(int id)
        {

            var meeting = dbContext.Meetings.Find(id);
            if (meeting != null)
            {
                var tutor = dbContext.Tutors.Find(meeting.TutorID);
                if(tutor != null)
                    dbContext.Tutors.Remove(tutor);

                dbContext.Meetings.Remove(meeting);
                dbContext.SaveChanges();

                return Ok(1);
            }
            else
            {
                return Ok(-1);
            }

        }

        private List<Meeting> getAttended()
        {
            var meetings = (from m in dbContext.Meetings
                            where m.Attended == 1
                            select m).ToList();

            return meetings;
        }

        private bool notAccepted(int id)
        {
            var tutor = dbContext.Tutors.Find(id);
            if(tutor.Is_Accepted == 0)
            {
                return true;
            }
            return false;
        }
    }
}
