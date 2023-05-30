using Microsoft.AspNetCore.Mvc;
using RoomReservationSystem.Models.Entities;
using RoomReservationSystem.Repository.Interfaces;

namespace RoomReservationSystem.Controllers
{
    [ApiController]
    [Route("/api/reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public ActionResult<ICollection<Reservation>> GetAll()
        {
            var reservations = _reservationService.GetAll();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservationByOrganizerId([FromRoute] int id)
        {
            var reservations = _reservationService.GetByOrganizerId(id);
            return Ok(reservations);
        }

        [HttpPost]
        public ActionResult<int> AddReservation(Reservation reservation)
        {

        }
    }
}
