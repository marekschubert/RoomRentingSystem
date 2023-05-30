using RoomReservationSystem.Models.Entities;

namespace RoomReservationSystem.Models.Dto.CreationDto
{
    public class CreateReservationDto
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public List<User> Participants { get; set; }
    }
}