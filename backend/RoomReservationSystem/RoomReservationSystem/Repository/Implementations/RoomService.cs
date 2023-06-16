using RoomReservationSystem.Repository.Interfaces;

namespace RoomReservationSystem.Repository.Implementations
{
    public class RoomService : IRoomService
    {
        public string GetRoomPositionData()
        {
            //string filePath = "../Assets/rooms.json";
            string fileName = "rooms.json";
            string filePath = Path.Combine(Environment.CurrentDirectory, @"Assets\", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }

            string jsonContent = System.IO.File.ReadAllText(filePath);

            return jsonContent;

        }
    }
}
