using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagement
{
    // Modele danych
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string GuestName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }

    public class Guest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    // Klasa bazy danych
    public class HotelDatabase
    {
        private List<Room> Rooms = new List<Room>();
        private List<Reservation> Reservations = new List<Reservation>();
        private List<Guest> Guests = new List<Guest>();

        // Metody zarządzania danymi
        public void AddRoom(Room room)
        {
            room.Id = Rooms.Count > 0 ? Rooms.Max(r => r.Id) + 1 : 1;
            Rooms.Add(room);
        }

        public void AddReservation(Reservation reservation)
        {
            reservation.Id = Reservations.Count > 0 ? Reservations.Max(r => r.Id) + 1 : 1;
            Reservations.Add(reservation);
        }

        public void AddGuest(Guest guest)
        {
            guest.Id = Guests.Count > 0 ? Guests.Max(g => g.Id) + 1 : 1;
            Guests.Add(guest);
        }

        public Room GetRoom(int id) => Rooms.FirstOrDefault(r => r.Id == id);

        public IEnumerable<Room> GetAllRooms() => Rooms;

        public Reservation GetReservation(int id) => Reservations.FirstOrDefault(r => r.Id == id);

        public IEnumerable<Reservation> GetAllReservations() => Reservations;

        public Guest GetGuest(int id) => Guests.FirstOrDefault(g => g.Id == id);

        public IEnumerable<Guest> GetAllGuests() => Guests;

        public void UpdateRoom(int id, Room updatedRoom)
        {
            var room = GetRoom(id);
            if (room != null)
            {
                room.RoomNumber = updatedRoom.RoomNumber;
                room.IsAvailable = updatedRoom.IsAvailable;
            }
        }

        public void DeleteRoom(int id) => Rooms.RemoveAll(r => r.Id == id);

        public void DeleteReservation(int id) => Reservations.RemoveAll(r => r.Id == id);

        public void DeleteGuest(int id) => Guests.RemoveAll(g => g.Id == id);
    }

    // Główna aplikacja
    class Program
    {
        static void Main(string[] args)
        {
            var database = new HotelDatabase();

            // Dodawanie danych
            database.AddRoom(new Room { RoomNumber = "101", IsAvailable = true });
            database.AddRoom(new Room { RoomNumber = "102", IsAvailable = false });

            database.AddGuest(new Guest { FullName = "John Doe", Email = "john.doe@example.com" });

            database.AddReservation(new Reservation
            {
                RoomId = 1,
                GuestName = "John Doe",
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(2)
            });

            // Wyświetlanie danych
            Console.WriteLine("Lista pokoi:");
            foreach (var room in database.GetAllRooms())
            {
                Console.WriteLine($"ID: {room.Id}, Numer: {room.RoomNumber}, Dostępny: {room.IsAvailable}");
            }

            Console.WriteLine("\nLista rezerwacji:");
            foreach (var reservation in database.GetAllReservations())
            {
                Console.WriteLine($"ID: {reservation.Id}, Pokój ID: {reservation.RoomId}, Gość: {reservation.GuestName}, Od: {reservation.CheckIn}, Do: {reservation.CheckOut}");
            }

            Console.WriteLine("\nLista gości:");
            foreach (var guest in database.GetAllGuests())
            {
                Console.WriteLine($"ID: {guest.Id}, Imię i nazwisko: {guest.FullName}, Email: {guest.Email}");
            }
        }
    }
}
