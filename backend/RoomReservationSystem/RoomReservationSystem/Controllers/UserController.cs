using Microsoft.AspNetCore.Mvc;
using RoomReservationSystem.Models.Dto;
using RoomReservationSystem.Repository.Implementations;
using RoomReservationSystem.Repository.Interfaces;

namespace RoomReservationSystem.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<ReservationDto>> GetAll()
        {
            var reservations = _userService.GetAll();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public ActionResult<List<ReservationDto>> GetById([FromRoute] int id)
        {
            var reservations = _userService.GetById(id);
            return Ok(reservations);
        }

    }
}
