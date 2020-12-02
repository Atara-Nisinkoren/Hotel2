    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class RoomsOrders
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        public Room Room { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public Order Order { get; set; }
    }
}
