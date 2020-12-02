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
        public int Id { get; set; }

        //מס' לקוח
        [Required]
        public Client Client { get; set; }

        //מתאריך
        [Required]
        public DateTime FromDate { get; set; }

        //עד תאריך
        [Required]
        public DateTime ToDate { get; set; }

        //מס' מבוגרים
        [Required]
        public int NumOfAdults { get; set; }

        //מס' ילדים
        [Required]
        public int NumOfKids { get; set; }

        //מס' תינוקות
        [Required]
        public int NumOfInfants { get; set; }

        //רשימת החדרים בהזמנה
        [Required]
        public ICollection<RoomsOrders> Rooms { get; set; }

        //מחיר
        [Required]
        public double TotalPrice { get; set; }

    }
}
