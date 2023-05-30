using RoomReservationSystem.Models.Dto;
using RoomReservationSystem.Models.Dto.CreationDto;
using RoomReservationSystem.Models.Entities;

namespace RoomReservationSystem.Repository.Interfaces
{
    public interface IReservationService
    {
        List<ReservationDto> GetAll();
        ReservationDto GetById(int id);
        List<ReservationDto> GetByOrganizerId(int id);
        int AddReservation(CreateReservationDto dto);
    }
}
