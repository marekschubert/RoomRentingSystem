using Microsoft.EntityFrameworkCore;
using RoomReservationSystem.Models.Entities;

namespace RoomReservationSystem.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Layer> Layers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

    }
}
