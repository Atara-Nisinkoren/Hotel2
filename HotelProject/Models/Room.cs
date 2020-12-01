using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Room
    {
        //מס' חדר
        public int Id { get; set; }

        //סוג חדר
        public int Type { get; set; }

        //מס' מיטות נוספות
        public int ExtraBeds { get; set; }

        //האם מיטה זוגית ו 2 מיטות יחיד
        public bool IsTwinBed { get; set; }

        //קומה
        public int Floor { get; set; }

        //נוף
        public int View { get; set; }

        //האם יש מרפסת
        public bool IsBalcony { get; set; }

        //מחיר בסיסי לזוג
        public double BasicPrice { get; set; }

        //האם פנוי
        public bool IsAvailable { get; set; }

        //מס' הזמנה פעילה לחדר
        public Order OrderId { get; set; }

    }
}
