using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomReservationSystem.Models.Entities;

namespace RoomReservationSystem.Models.EntitiesConfiguration
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.Participants)
                .WithMany(u => u.AsMemberReservations);


            builder.HasOne(res => res.Room)
                .WithMany(room => room.Reservations);
        }
    }
}
