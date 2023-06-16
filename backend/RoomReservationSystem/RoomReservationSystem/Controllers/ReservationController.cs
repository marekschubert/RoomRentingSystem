using Microsoft.AspNetCore.Mvc;
using RoomReservationSystem.Models.Dto;
using RoomReservationSystem.Models.Dto.CreationDto;
using RoomReservationSystem.Models.Entities;
using RoomReservationSystem.Repository.Interfaces;

namespace RoomReservationSystem.Controllers
{
    [ApiController]
    [Route("/api/reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IEmailService _emailService;

        public ReservationController(IReservationService reservationService, IEmailService emailService)
        {
            _reservationService = reservationService;
            _emailService = emailService;
        }

        [HttpGet]
        public ActionResult<List<ReservationDto>> GetAll()
        {
            var reservations = _reservationService.GetAll();
            return Ok(reservations);
        }

        [HttpGet("organizer/{id}")]
        public ActionResult<List<ReservationDto>> GetReservationByOrganizerId([FromRoute] int id)
        {
            var reservations = _reservationService.GetByOrganizerId(id);
            return Ok(reservations);
        }

        [HttpPost]
        public ActionResult<int> AddReservation([FromBody] CreateReservationDto dto)
        {
            var createdId = _reservationService.AddReservation(dto);
            _emailService.SendEmail(dto);
            return Created($"/api/reservation/{createdId}", null);
        }
    }
}
