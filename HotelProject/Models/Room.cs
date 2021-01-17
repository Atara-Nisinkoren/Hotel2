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
        [Display(Name = "מס' חדר")]
        public int Id { get; set; }

        //סוג חדר
        [Required]
        [Display(Name = "סוג חדר")]
        public RoomType Type { get; set; }

        //האם מיטה זוגית ו 2 מיטות יחיד
        [Required]
        [Display(Name = "מיטה זוגית/2 מיטות יחיד")]
        public bool IsTwinBed { get; set; }

        //קומה
        [Required]
        [Display(Name = "קומה")]
        public int Floor { get; set; }

       /* //האם פנוי
        [Required]
        public bool IsAvailable { get; set; }*/

        //מס' הזמנה פעילה לחדר
        [Required]
        [Display(Name = "מס' הזמנה")]
        [JsonIgnore]
        public ICollection<RoomsOrders> Orders { get; set; }


      
    }
}
