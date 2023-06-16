using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoomReservationSystem.Models;
using RoomReservationSystem.Models.Dto;
using RoomReservationSystem.Models.Dto.CreationDto;
using RoomReservationSystem.Models.Entities;
using RoomReservationSystem.Repository.Interfaces;

namespace RoomReservationSystem.Repository.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly MyDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReservationService(MyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int AddReservation(CreateReservationDto dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);

            var room = _dbContext.Rooms.FirstOrDefault(r => r.Id == dto.RoomId);
            var participants = new List<User>();

            foreach (var participantEmail in dto.ParticipantsEmailAddresses)
            {
                participants.Add(_dbContext.Users.FirstOrDefault(u => u.Email == participantEmail));
            }

            reservation.Room = room;
            reservation.Participants = participants;

            _dbContext.Reservations.Add(reservation);
            _dbContext.SaveChanges();

            return reservation.Id;
        }

        public List<ReservationDto> GetAll()
        {
            var reservations = _dbContext.Reservations
                                .Include(r => r.Room.Layer)
                                .Include(r => r.Participants)
                                .ToList();
            var dto = _mapper.Map<List<ReservationDto>>(reservations);
            return dto;
        }

        public ReservationDto GetById(int id)
        {
            var reservation = _dbContext.Reservations
                                .Include(r => r.Room.Layer)
                                .Include(r => r.Participants)
                                .FirstOrDefault(r => r.Id == id);
            var dto = _mapper.Map<ReservationDto>(reservation);
            return dto;
        }

        public List<ReservationDto> GetByOrganizerId(int id)
        {
            var reservations = _dbContext.Reservations
                                .Include(r => r.Participants)
                                .Where(r => r.Participants[0].Id == id)
                                .Include(r => r.Room.Layer)
                                .ToList();

            var dto = _mapper.Map<List<ReservationDto>>(reservations);
            return dto;
        }
    }
}
