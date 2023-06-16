using RoomReservationSystem.Models.Dto;
using RoomReservationSystem.Models.Dto.CreationDto;

namespace RoomReservationSystem.Repository.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(CreateReservationDto emailDto);
    }
}
