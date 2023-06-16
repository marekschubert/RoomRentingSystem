using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RoomReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRoomPositionData()
        {
            string filePath = "\\Assets\\rooms.json";

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            string jsonContent = System.IO.File.ReadAllText(filePath);

            return Content(jsonContent, "application/json"); // Odpowiedź zawierająca treść pliku JSON jako odpowiedni typ MIME

        }
    }
}
