using RoomReservationSystem.Models.Entities;

namespace RoomReservationSystem.Repository.Interfaces
{
    public interface IUserService
    {
        ICollection<User> GetAll();
        User GetById(int id);
    }
}
