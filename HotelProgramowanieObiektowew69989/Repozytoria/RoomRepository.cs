using HotelProgramowanieObiektowew69989.Modele;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagement
{
    public class RoomRepository : IRoomRepository
    {
        private List<Room> Rooms = new List<Room>();

        // Dodanie pokoju z walidacją
        public void AddRoom(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room), "Pokój nie może być null.");
            }

            // Walidacja unikalności numeru pokoju
            if (Rooms.Any(r => r.RoomNumber == room.RoomNumber))
            {
                throw new InvalidOperationException("Pokój o tym numerze już istnieje.");
            }

            // Dodanie ID pokoju
            room.Id = Rooms.Count > 0 ? Rooms.Max(r => r.Id) + 1 : 1;

            // Walidacja dostępności pokoju
            if (room.IsAvailable != true && room.IsAvailable != false)
            {
                throw new ArgumentException("Stan dostępności pokoju musi być wartością typu bool.");
            }

            Rooms.Add(room);
        }

        // Pobranie pokoju po ID z obsługą wyjątków
        public Room GetRoom(int id)
        {
            var room = Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                throw new KeyNotFoundException($"Pokój o ID {id} nie został znaleziony.");
            }
            return room;
        }

        // Pobranie wszystkich pokoi
        public IEnumerable<Room> GetAllRooms() => Rooms;

        // Aktualizacja pokoju
        public void UpdateRoom(int id, Room updatedRoom)
        {
            if (updatedRoom == null)
            {
                throw new ArgumentNullException(nameof(updatedRoom), "Zaktualizowany pokój nie może być null.");
            }

            var room = GetRoom(id); // Jeśli pokój nie istnieje, rzuci wyjątek

            // Walidacja unikalności numeru pokoju podczas aktualizacji
            if (Rooms.Any(r => r.RoomNumber == updatedRoom.RoomNumber && r.Id != id))
            {
                throw new InvalidOperationException("Pokój o tym numerze już istnieje.");
            }

            // Walidacja dostępności
            if (updatedRoom.IsAvailable != true && updatedRoom.IsAvailable != false)
            {
                throw new ArgumentException("Stan dostępności pokoju musi być wartością typu bool.");
            }

            room.RoomNumber = updatedRoom.RoomNumber;
            room.IsAvailable = updatedRoom.IsAvailable;
        }

        // Usunięcie pokoju
        public void DeleteRoom(int id)
        {
            var room = GetRoom(id); // Jeśli pokój nie istnieje, rzuci wyjątek
            Rooms.Remove(room);
        }
    }
}
