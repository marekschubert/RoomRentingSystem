using RoomReservationSystem.Models.Dto;

namespace RoomReservationSystem.Repository.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailDto emailDto);
    }
}
