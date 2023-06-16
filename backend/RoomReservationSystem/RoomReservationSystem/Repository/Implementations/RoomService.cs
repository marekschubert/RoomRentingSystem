using RoomReservationSystem.Repository.Interfaces;

namespace RoomReservationSystem.Repository.Implementations
{
    public class RoomService : IRoomService
    {
        public string GetRoomPositionData()
        {
            string filePath = "\\Assets\\rooms.json";

            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }

            string jsonContent = System.IO.File.ReadAllText(filePath);

            return jsonContent;

        }
    }
}
