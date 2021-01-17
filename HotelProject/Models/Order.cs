using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Order
    {
        //מס' הזמנה
        [Key]
        [Display(Name = "מספר הזמנה")]
        public int Id { get; set; }

        //מס' לקוח
        [Required]
        [Display(Name = "מס' לקוח")]
        public Client Client { get; set; }

        //מתאריך
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "תאריך הגעה")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        //עד תאריך
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "תאריך עזיבה")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        //מס' מבוגרים
        [Required]
        [Display(Name = "מס' מבוגרים")]
        public int NumOfAdults { get; set; }

        //מס' ילדים
        [Required]
        [Display(Name = "מס' ילדים")]
        public int NumOfKids { get; set; }

        //מס' תינוקות
        [Required]
        [Display(Name = "מס' תינוקות")]
        //[DataType(DataType.)]
        public int NumOfInfants { get; set; }

        //רשימת החדרים בהזמנה
        [Required]
        [Display(Name = "חדרי הזמנה")]
        public ICollection<RoomsOrders> Rooms { get; set; }

        //מחיר
        [Required]
        [Display(Name = "מחיר")]
        public double TotalPrice { get; set; }


     

    }
}
