// Projekt implementacji bazy danych hotelu w technologii .NET

using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagement
{
    // Interfejs bazowy CRUD z metodami HTTP
    public interface IRepository<T>
    {
        void Post(T entity); // Dodawanie (Create)
        T Get(int id); // Odczyt pojedynczego elementu
        IEnumerable<T> GetAll(); // Odczyt wszystkich elementów
        void Put(int id, T entity); // Aktualizacja (Update)
        void Delete(int id); // Usunięcie
    }

    // Klasa reprezentująca Pokój w hotelu
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public bool IsAvailable { get; set; }
    }

    // Klasa reprezentująca Rezerwację
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string GuestName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }

    // Klasa reprezentująca Gościa
    public class Guest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    // Implementacja bazowego repozytorium w pamięci
    public class InMemoryRepository<T> : IRepository<T>
    {
        private readonly List<T> _items = new List<T>();
        private int _currentId = 1;

        public void Post(T entity)
        {
            var property = typeof(T).GetProperty("Id");
            if (property != null)
            {
                property.SetValue(entity, _currentId++);
                _items.Add(entity);
            }
        }

        public T Get(int id)
        {
            return _items.FirstOrDefault(item => (int)typeof(T).GetProperty("Id")?.GetValue(item) == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _items;
        }

        public void Put(int id, T entity)
        {
            var index = _items.FindIndex(item => (int)typeof(T).GetProperty("Id")?.GetValue(item) == id);
            if (index >= 0)
            {
                _items[index] = entity;
                typeof(T).GetProperty("Id")?.SetValue(entity, id);
            }
            else
            {
                throw new Exception("Item not found");
            }
        }

        public void Delete(int id)
        {
            var item = Get(id);
            if (item != null)
            {
                _items.Remove(item);
            }
            else
            {
                throw new Exception("Item not found");
            }
        }
    }

    // Symulacja kontrolera HTTP dla zarządzania pokojami
    public class RoomController
    {
        private readonly IRepository<Room> _repository;

        public RoomController(IRepository<Room> repository)
        {
            _repository = repository;
        }

        public void HttpPost(Room room)
        {
            _repository.Post(room);
        }

        public Room HttpGet(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Room> HttpGetAll()
        {
            return _repository.GetAll();
        }

        public void HttpPut(int id, Room room)
        {
            _repository.Put(id, room);
        }

        public void HttpDelete(int id)
        {
            _repository.Delete(id);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Dependency Injection
            IRepository<Room> roomRepository = new InMemoryRepository<Room>();
            var roomController = new RoomController(roomRepository);

            try
            {
                // Dodawanie pokoju
                var room = new Room { RoomNumber = "101", IsAvailable = true };
                roomController.HttpPost(room);

                // Odczyt pokoju
                var readRoom = roomController.HttpGet(1);
                Console.WriteLine($"Room: {readRoom.RoomNumber}, Available: {readRoom.IsAvailable}");

                // Aktualizacja pokoju
                readRoom.IsAvailable = false;
                roomController.HttpPut(1, readRoom);

                // Usunięcie pokoju
                roomController.HttpDelete(1);

                // Odczyt wszystkich pokoi
                var allRooms = roomController.HttpGetAll();
                Console.WriteLine("All Rooms:");
                foreach (var r in allRooms)
                {
                    Console.WriteLine($"Room: {r.RoomNumber}, Available: {r.IsAvailable}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

