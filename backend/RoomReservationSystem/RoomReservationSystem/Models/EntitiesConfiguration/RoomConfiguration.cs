using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomReservationSystem.Models.Entities;

namespace RoomReservationSystem.Models.EntitiesConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(r => r.Layer)
                .WithMany(l => l.Rooms)
                .HasForeignKey(r => r.LayerId);
        }
    }
}
