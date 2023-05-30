using RoomReservationSystem.Models.Entities;

namespace RoomReservationSystem.Models.Dto.Data
{
    public class ReservationDataDto
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
