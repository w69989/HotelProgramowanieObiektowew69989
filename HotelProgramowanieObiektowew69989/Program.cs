using HotelProgramowanieObiektowew69989.Modele;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HotelManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tworzenie kontenera DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IHotelDatabase, HotelDatabase>() // Rejestracja interfejsu IHotelDatabase
                .AddSingleton<IRoomRepository, RoomRepository>()
                .AddSingleton<IReservationRepository, ReservationRepository>()
                .AddSingleton<IGuestRepository, GuestRepository>()
                .BuildServiceProvider();

            // Uzyskiwanie dostępu do instancji IHotelDatabase
            var hotelDatabase = serviceProvider.GetService<IHotelDatabase>();

            // Przykład użycia
            var room = new Room { RoomNumber = "101", IsAvailable = true };
            hotelDatabase.AddRoom(room);

            var reservation = new Reservation { GuestName = "Jan Kowalski", RoomId = room.Id, CheckIn = DateTime.Now, CheckOut = DateTime.Now.AddDays(2) };
            hotelDatabase.AddReservation(reservation);

            var guest = new Guest { FullName = "Jan Kowalski", Email = "jan@kowalski.com" };
            hotelDatabase.AddGuest(guest);

            Console.WriteLine($"Zarezerwowano pokój dla {guest.FullName}.");
        }
    }
}
