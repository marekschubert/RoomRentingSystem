namespace RoomReservationSystem.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }

        public IEnumerable<Reservation> AsMemberReservations { get; set; }
    }
}
