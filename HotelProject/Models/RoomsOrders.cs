    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class RoomsOrders
    {
        [Key]
        [Column(Order = 0)]
        public int RoomId { get; set; }

        [Required]
        public Room Room { get; set; }

        [Key]
        [Column(Order = 1)]
        public int OrderId { get; set; }

        [Required]
        public Order Order { get; set; }
    }
}
