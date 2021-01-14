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
        [Required(ErrorMessage = "שדה תעודת זהות הינו שדה חובה")]
        [Display(Name = "תעודת זהות")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "תעודת זהות שגויה"), StringLength(9)]
        public string WorkerId { get; set; }

        //שם
        [Required(ErrorMessage = "שדה שם הינו שדה חובה")]
        [Display(Name = "שם")]
        public string Name { get; set; }

        //מספר פלאפון
        [Required(ErrorMessage = "שדה מספר טלפון הינו שדה חובה")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "מספר טלפון שגוי"), StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "מספר טלפון")]
        public string PhoneNumber { get; set; }

        //אימייל
        [Required(ErrorMessage = "שדה אימייל הינו שדה חובה")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "אימייל")]
        [EmailAddress]
        public string Email { get; set; }

        //תפקיד העובד
        [Display(Name = "תפקיד")]
        public int WorkerType { get; set; }
    }
}
