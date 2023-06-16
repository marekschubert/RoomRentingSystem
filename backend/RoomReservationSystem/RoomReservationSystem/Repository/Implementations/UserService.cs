using RoomReservationSystem.Models;
using RoomReservationSystem.Models.Dto;
using RoomReservationSystem.Models.Entities;
using RoomReservationSystem.Repository.Interfaces;

namespace RoomReservationSystem.Repository.Implementations
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _dbContext;
        public UserService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ICollection<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public void LoginOrRegister(LoginRegisterDto dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
            {
                var newUser = new User()
                {
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Password = "",
                    Birthday = DateTime.MinValue,
                    AsMemberReservations = new List<Reservation>()
                };
                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();
            }
        }
    }
}
