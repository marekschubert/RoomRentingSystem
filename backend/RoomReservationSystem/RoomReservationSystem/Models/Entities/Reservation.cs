namespace RoomReservationSystem.Models.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public List<User> Participants { get; set; }
    }
}
