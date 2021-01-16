using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Room
    {
        //מס' חדר
        [Key]
        [Required]
        public int Id { get; set; }

        //סוג חדר
        [Required]
        public RoomType Type { get; set; }

        //האם מיטה זוגית ו 2 מיטות יחיד
        [Required]
        public bool IsTwinBed { get; set; }

        //קומה
        [Required]
        public int Floor { get; set; }

       /* //האם פנוי
        [Required]
        public bool IsAvailable { get; set; }*/

        //מס' הזמנה פעילה לחדר
        [Required]
        [JsonIgnore]
        public ICollection<RoomsOrders> Orders { get; set; }


      
    }
}
