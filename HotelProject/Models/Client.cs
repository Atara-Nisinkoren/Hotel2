using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Client
    {
        //תעודת זהות
        [Key]
        [StringLength(9, ErrorMessage = "תעודת זהות שגויה")]
        public string Tz { get; set; }

        //שם
        [Required(ErrorMessage = "שם שדה חובה")]
        [StringLength(50, ErrorMessage = "יש לקצר את השם עד 50 תוים")]
        public string Name { get; set; }

        //מספר פלאפון
        [Required(ErrorMessage = "מספר טלפון שדה חובה")]
        [StringLength(10, ErrorMessage = "מספר טלפון שגוי")]
        public string PhoneNumber { get; set; }

        //כתובת
        [Required(ErrorMessage = "כתובת שדה חובה")]
        [StringLength(100, ErrorMessage = "יש לקצר את הכתובת עד 100 תוים")]
        public string Address { get; set; }

        //אימייל
        [Required(ErrorMessage = "אימייל שדה חובה")]
        [StringLength(50, ErrorMessage = "יש לקצר את האימייל עד 50 תוים")]
        public string Email { get; set; }

        //הזמנות של לקוח
        public ICollection<Order> Orders { get; set; }
    }
}
