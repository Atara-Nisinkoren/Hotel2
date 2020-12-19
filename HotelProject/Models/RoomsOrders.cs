using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class RoomsOrders
    {
        public int RoomId { get; set; }

        public Room Room { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
