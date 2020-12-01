using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Client
    {
        //תעודת זהות
        public string Id { get; set; }

        //שם
        public string Name { get; set; }

        //מספר פלאפון
        public string PhoneNumber { get; set; }

        //כתובת
        public string Address { get; set; }

        //אימייל
        public string Email { get; set; }
    }
}
