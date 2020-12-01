using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Worker
    {
        //מס' עובד
        public int Id { get; set; }

        //תעודת זהות
        public string WorkerId { get; set; }

        //שם
        public string Name { get; set; }

        //מספר פלאפון
        public string PhoneNumber { get; set; }

        //אימייל
        public string Email { get; set; }

        //תפקיד העובד
        public int WorkerType { get; set; }
    }
}
