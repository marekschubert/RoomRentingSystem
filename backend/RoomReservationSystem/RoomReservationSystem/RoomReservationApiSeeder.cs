using RoomReservationSystem.Models;
using RoomReservationSystem.Models.Entities;

namespace RoomReservationSystem
{
    public class RoomReservationApiSeeder
    {
        private readonly MyDbContext _dbContext;

        private List<User> _users;
        private List<Room> _rooms;
        private List<Reservation> _reservations;
        private List<Layer> _layers;

        public RoomReservationApiSeeder(MyDbContext dbContext)
        {
            _dbContext = dbContext;
            _layers = GetLayers();
            _rooms = GetRooms();
            _users = GetUsers();
            _reservations = GetReservations();
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Layers.Any())
                {
                    _dbContext.Layers.AddRange(_layers);
                }
                if (!_dbContext.Rooms.Any())
                {
                    _dbContext.Rooms.AddRange(_rooms);
                }
                if (!_dbContext.Users.Any())
                {
                    _dbContext.Users.AddRange(_users);
                }
                if (!_dbContext.Reservations.Any())
                {
                    _dbContext.Reservations.AddRange(_reservations);
                }

                _dbContext.SaveChanges();
            }
        }

        private List<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com",
                    Password = "Pass123,./",
                    Birthday = new DateTime(1990, 1, 1),
                    AsMemberReservations = new List<Reservation>(),
                   // AsOrganizerReservations = new List<Reservation>()
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Michael",
                    LastName = "Smith",
                    Email = "michael.smith@gmail.com",
                    Password = "Pass123,./",
                    Birthday = new DateTime(1995, 5, 5),
                    AsMemberReservations = new List<Reservation>(),
                   // AsOrganizerReservations = new List<Reservation>()
                }
            };

            return users;
        }

        private List<Room> GetRooms()
        {
            var rooms = new List<Room>()
            {
                new Room()
                {
                    Id = 1,
                    Number = 101,
                    Capacity = 5,
                    LayerId = 1,
                    Reservations = new List<Reservation>()
                },
                new Room()
                {
                    Id = 2,
                    Number = 102,
                    Capacity = 10,
                    LayerId = 1,
                    Reservations = new List<Reservation>()
                },
                new Room()
                {
                    Id = 3,
                    Number = 201,
                    Capacity = 8,
                    LayerId = 2,
                    Reservations = new List<Reservation>()
                }
            };

            return rooms;
        }


        private List<Reservation> GetReservations()
        {
            var reservation1Participants = new List<User>()
            {
                _users[1]
            };

            var reservation2Participants = new List<User>()
            {
                _users[0]
            };

            var reservations = new List<Reservation>()
            {
                new Reservation()
                {
                    Id = 1,
                    StartDateTime = DateTime.Now.AddHours(1),
                    EndDateTime = DateTime.Now.AddHours(2),
                   // UserId = 1,
                   // User = _users[0],
                    RoomId = 1,
                    Room = _rooms[0],
                    Participants = reservation1Participants
                },
                new Reservation()
                {
                    Id = 2,
                    StartDateTime = DateTime.Now.AddHours(3),
                    EndDateTime = DateTime.Now.AddHours(4),
                   // UserId = 2,
                  //  User = _users[1],
                    RoomId = 2,
                    Room = _rooms[1],
                    Participants = reservation2Participants
                }
            };

            return reservations;
        }

        private List<Layer> GetLayers()
        {
            var layers = new List<Layer>()
            {
                new Layer()
                {
                    Id = 1,
                    Number = 1,
                    Rooms = new List<Room>()
                },
                new Layer()
                {
                    Id = 2,
                    Number = 2,
                    Rooms = new List<Room>()
                }
            };

            return layers;
        }
    }
}
