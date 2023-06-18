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
                _dbContext.Database.EnsureDeleted();
                _dbContext.Database.EnsureCreated();
                _dbContext.SaveChanges();
                
                if (!_dbContext.Layers.Any())
                {
                    _dbContext.Layers.AddRange(_layers);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Rooms.Any())
                {
                    _dbContext.Rooms.AddRange(_rooms);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Users.Any())
                {
                    _dbContext.Users.AddRange(_users);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Reservations.Any())
                {
                    _dbContext.Reservations.AddRange(_reservations);
                    _dbContext.SaveChanges();
                }
            }
        }

        private List<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com",
                    Password = "Pass123,./",
                    Birthday = new DateTime(1990, 1, 1),
                    AsMemberReservations = new List<Reservation>(),
                },
                new User()
                {
                    FirstName = "Michael",
                    LastName = "Smith",
                    Email = "michael.smith@gmail.com",
                    Password = "Pass123,./",
                    Birthday = new DateTime(1995, 5, 5),
                    AsMemberReservations = new List<Reservation>(),
                }
            };

            return users;
        }

        private List<Room> GetRooms()
        {
            var rooms = new List<Room>()
            {
                new Room() { Number = 101, Capacity = 30, LayerId = 1, Reservations = new List<Reservation>() },
                new Room() { Number = 102, Capacity = 15, LayerId = 1, Reservations = new List<Reservation>() },
                new Room() { Number = 103, Capacity = 15, LayerId = 1, Reservations = new List<Reservation>() },
                new Room() { Number = 104, Capacity = 18, LayerId = 1, Reservations = new List<Reservation>() },
                new Room() { Number = 105, Capacity = 30, LayerId = 1, Reservations = new List<Reservation>() },
                new Room() { Number = 106, Capacity = 0, LayerId = 1, Reservations = new List<Reservation>() },
                new Room() { Number = 107, Capacity = 0, LayerId = 1, Reservations = new List<Reservation>() },
                new Room() { Number = 108, Capacity = 0, LayerId = 1, Reservations = new List<Reservation>() },
                new Room() { Number = 201, Capacity = 40, LayerId = 2, Reservations = new List<Reservation>() },
                new Room() { Number = 202, Capacity = 15, LayerId = 2, Reservations = new List<Reservation>() },
                new Room() { Number = 203, Capacity = 15, LayerId = 2, Reservations = new List<Reservation>() },
                new Room() { Number = 204, Capacity = 10, LayerId = 2, Reservations = new List<Reservation>() },
                new Room() { Number = 205, Capacity = 10, LayerId = 2, Reservations = new List<Reservation>() },
                new Room() { Number = 206, Capacity = 10, LayerId = 2, Reservations = new List<Reservation>() },
                new Room() { Number = 207, Capacity = 10, LayerId = 2, Reservations = new List<Reservation>() },
                new Room() { Number = 208, Capacity = 0, LayerId = 2, Reservations = new List<Reservation>() },
                new Room() { Number = 301, Capacity = 60, LayerId = 3, Reservations = new List<Reservation>() },
                new Room() { Number = 302, Capacity = 10, LayerId = 3, Reservations = new List<Reservation>() },
                new Room() { Number = 303, Capacity = 10, LayerId = 3, Reservations = new List<Reservation>() },
                new Room() { Number = 304, Capacity = 25, LayerId = 3, Reservations = new List<Reservation>() },
                new Room() { Number = 305, Capacity = 10, LayerId = 3, Reservations = new List<Reservation>() },
                new Room() { Number = 306, Capacity = 10, LayerId = 3, Reservations = new List<Reservation>() },
                new Room() { Number = 307, Capacity = 0, LayerId = 3, Reservations = new List<Reservation>() },
                new Room() { Number = 308, Capacity = 0, LayerId = 3, Reservations = new List<Reservation>() }
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
                    // Id = 1,
                    StartDateTime = DateTime.Now.AddHours(1),
                    EndDateTime = DateTime.Now.AddHours(2),
                    Room = _rooms[0],
                    Participants = reservation1Participants
                },
                new Reservation()
                {
                    // Id = 2,
                    StartDateTime = DateTime.Now.AddHours(3),
                    EndDateTime = DateTime.Now.AddHours(4),
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
                    Number = 1,
                    Rooms = new List<Room>()
                },
                new Layer()
                {
                    Number = 2,
                    Rooms = new List<Room>()
                },
                new Layer()
                {
                    Number = 3,
                    Rooms = new List<Room>()
                }
            };

            return layers;
        }
    }
}
