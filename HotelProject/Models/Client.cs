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
        [Display(Name = "תעודת זהות")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "תעודת זהות שגויה"), StringLength(9)]

        //[DataType(DataType.)]
        public string ID { get; set; }

        //שם
        [Required(ErrorMessage = "שם שדה חובה")]
        [Display(Name = "שם")]
        [StringLength(50, ErrorMessage = "יש לקצר את השם עד 50 תוים")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        //מספר פלאפון
        [Required(ErrorMessage = "מספר טלפון שדה חובה")]
        [Display(Name = "מספר טלפון")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "מספר טלפון שגוי"), StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        //כתובת
        [Required(ErrorMessage = "כתובת שדה חובה")]
        [Display(Name = "כתובת")]
        [StringLength(100, ErrorMessage = "יש לקצר את הכתובת עד 100 תוים")]
        public string Address { get; set; }

        //אימייל
        [Required(ErrorMessage = "אימייל שדה חובה")]
        [Display(Name = "אימייל")]
        [StringLength(50, ErrorMessage = "יש לקצר את האימייל עד 50 תוים")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //הזמנות של לקוח
        public ICollection<Order> Orders { get; set; }
    }
}
