﻿using RoomReservationSystem.Models.Dto.CreationDto;
using RoomReservationSystem.Models.Entities;

namespace RoomReservationSystem.Repository.Interfaces
{
    public interface IReservationService
    {
        ICollection<Reservation> GetAll();
        Reservation GetById(int id);
        ICollection<Reservation> GetByOrganizerId(int id);
        int AddReservation(CreateReservationDto dto);
    }
}