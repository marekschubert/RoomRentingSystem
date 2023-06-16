namespace RoomReservationSystem.Models.Dto
{
    public class EmailDto
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
