using RoomReservationSystem.Models.Dto.Data;
using RoomReservationSystem.Models.Entities;

namespace RoomReservationSystem.Models.Dto
{
    public class ReservationDto
    {
        public ReservationDataDto ReservationData { get; set; }
        public List<UserDataDto> ParticipantsData { get; set; }
        public RoomDataDto RoomData { get; set; }
        public LayerDataDto LayerData { get; set; }
    }
}
