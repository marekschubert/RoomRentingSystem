namespace RoomReservationSystem.Models.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        
        public int LayerId { get; set; }
        public Layer Layer { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
