using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Order
    {
        //מס' הזמנה
        public int Id { get; set; }

        //מס' לקוח
        public int ClientId { get; set; }

        //רשימת החדרים בהזמנה
        public ICollection<Room> Rooms { get; set; }

        //מתאריך
        public DateTime FromDate { get; set; }

        //עד תאריך
        public DateTime ToDate { get; set; }

        //מס' מבוגרים
        public int NumOfAdults { get; set; }

        //מס' ילדים
        public int NumOfKids { get; set; }

        //מס' תינוקות
        public int NumOfInfants { get; set; }

        //מחיר
        public double TotalPrice { get; set; }

    }
}
