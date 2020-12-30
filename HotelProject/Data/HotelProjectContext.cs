using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;

namespace HotelProject.Data
{
    public class HotelProjectContext : DbContext
    {
        public HotelProjectContext (DbContextOptions<HotelProjectContext> options)
            : base(options)
        {
        }

        public DbSet<HotelProject.Models.Client> Client { get; set; }

        public DbSet<HotelProject.Models.Order> Order { get; set; }

        public DbSet<HotelProject.Models.Room> Room { get; set; }

        public DbSet<HotelProject.Models.RoomsOrders> RoomsOrders { get; set; }

        public DbSet<HotelProject.Models.RoomType> RoomType { get; set; }

        public DbSet<HotelProject.Models.Worker> Worker { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomsOrders>()
                .HasKey(t => new { t.RoomId, t.OrderId });

            modelBuilder.Entity<RoomsOrders>()
                .HasOne(pt => pt.Room)
                .WithMany(p => p.Orders)
                .HasForeignKey(pt => pt.RoomId);

            modelBuilder.Entity<RoomsOrders>()
                .HasOne(pt => pt.Order)
                .WithMany(t => t.Rooms)
                .HasForeignKey(pt => pt.OrderId);
        }

    }
}
