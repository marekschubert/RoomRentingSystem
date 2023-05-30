using Microsoft.EntityFrameworkCore;
using RoomReservationSystem.Models;
using RoomReservationSystem.Models.Dto.CreationDto;
using RoomReservationSystem.Models.Entities;
using RoomReservationSystem.Repository.Interfaces;

namespace RoomReservationSystem.Repository.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly MyDbContext _dbContext;
        public ReservationService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddReservation(CreateReservationDto dto)
        {
            throw new NotImplementedException();
        }

        public ICollection<Reservation> GetAll()
        {
            return _dbContext.Reservations.ToList();
        }

        public Reservation GetById(int id)
        {
            return _dbContext.Reservations.FirstOrDefault(r => r.Id == id);
        }

        public ICollection<Reservation> GetByOrganizerId(int id)
        {
            var reservationsByOrganizerId = _dbContext.Reservations.Include(r => r.Participants).Where(r => r.Participants[0].Id == id).ToList();
            return reservationsByOrganizerId;
        }
    }
}
