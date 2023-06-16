using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomReservationSystem.Repository.Interfaces;

namespace RoomReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult GetRoomPositionData()
        {
            string jsonContent = _roomService.GetRoomPositionData();
            return Content(jsonContent, "application/json");
        }
    }
}
