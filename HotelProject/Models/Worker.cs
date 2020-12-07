using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Worker
    {
        //מס' עובד
        [Key]
        public int Id { get; set; }

        //תעודת זהות
        [Required]
        public string WorkerId { get; set; }

        //שם
        [Required]
        public string Name { get; set; }

        //מספר פלאפון
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        //אימייל
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //תפקיד העובד
        public int WorkerType { get; set; }
    }
}
