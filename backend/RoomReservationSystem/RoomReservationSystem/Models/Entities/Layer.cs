namespace RoomReservationSystem.Models.Entities
{
    public class Layer
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public List<Room> Rooms { get; set; }

    }
}
