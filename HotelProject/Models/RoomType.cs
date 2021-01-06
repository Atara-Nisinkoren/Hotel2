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
        [Required(ErrorMessage = "שדה שם הינו שדה חובה")]
        [Display(Name = "שם")]
        public string Name { get; set; }

        //תיאור החדר
        [Required(ErrorMessage = "שדה תיאור החדר הינו שדה חובה")]
        [Display(Name = "תיאור החדר")]
        public string Desc { get; set; }

        //מספר מיטות נוספות לחדר
        [Required(ErrorMessage = "שדה זה הינו שדה חובה")]
        [Display(Name = "מספר מיטות נוספות לחדר")]
        public int ExtraBeds { get; set; }

        //נוף
        [Required(ErrorMessage = "שדה נוף הינו שדה חובה")]
        [Display(Name = "נוף")]
        public int View { get; set; }

        //האם יש מרפסת
        [Required(ErrorMessage = "שדה זה הינו שדה חובה")]
        [Display(Name = "האם יש מרפסת")]
        public bool IsBalcony { get; set; }

        //מחיר בסיסי לזוג
        [Required(ErrorMessage = "שדה מחיר בסיסי לזוג הינו שדה חובה")]
        [Display(Name = "מחיר בסיסי לזוג")]
        public double BasicPrice { get; set; }

        //תמונה
        [Required(ErrorMessage = "שדה תמונה הינו שדה חובה")]
        [Display(Name = "כתובת תמונה")]
        [DataType(DataType.ImageUrl)]
        public string ImgUrl { get; set; }

        //חדרים
        public ICollection<Room> Rooms { get; set; }
    }
}
