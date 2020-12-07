using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }

        //שם
        [Required]
        public string Name { get; set; }

        //תיאור החדר
        [Required]
        public string Desc { get; set; }

        //מספר מיטות נוספות לחדר
        [Required]
        public int ExtraBeds { get; set; }

        //נוף
        [Required]
        public int View { get; set; }

        //האם יש מרפסת
        [Required]
        public bool IsBalcony { get; set; }

        //מחיר בסיסי לזוג
        [Required]
        public double BasicPrice { get; set; }

        //תמונה
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImgUrl { get; set; }

        //חדרים
        [Required]
        public ICollection<Room> Rooms { get; set; }
    }
}
