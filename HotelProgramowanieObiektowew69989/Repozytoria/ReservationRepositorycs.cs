using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagement
{
    public class ReservationRepository : IReservationRepository
    {
        private List<Reservation> Reservations = new List<Reservation>();
        private readonly IRoomRepository roomRepository;
        private readonly IGuestRepository guestRepository;

        public ReservationRepository(IRoomRepository roomRepo, IGuestRepository guestRepo)
        {
            roomRepository = roomRepo;
            guestRepository = guestRepo;
        }

        // Dodanie rezerwacji z walidacją
        public void AddReservation(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException(nameof(reservation), "Rezerwacja nie może być null.");
            }

            // Sprawdzamy, czy pokój istnieje i jest dostępny
            var room = roomRepository.GetRoom(reservation.RoomId);
            if (room == null)
            {
                throw new KeyNotFoundException("Pokój o podanym ID nie istnieje.");
            }

            if (!room.IsAvailable)
            {
                throw new InvalidOperationException("Pokój nie jest dostępny.");
            }
            // Sprawdzamy, czy data rezerwacji jest w przyszłości
            if (reservation.CheckIn <= DateTime.Now)
            {
                throw new ArgumentException("Data zameldowania musi być w przyszłości.");
            }

            reservation.Id = Reservations.Count > 0 ? Reservations.Max(r => r.Id) + 1 : 1;
            Reservations.Add(reservation);

            // Zmiana dostępności pokoju po dokonaniu rezerwacji
            room.IsAvailable = false;
        }

        // Pobranie rezerwacji po ID
        public Reservation GetReservation(int id)
        {
            var reservation = Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                throw new KeyNotFoundException($"Rezerwacja o ID {id} nie została znaleziona.");
            }
            return reservation;
        }

        // Pobranie wszystkich rezerwacji
        public IEnumerable<Reservation> GetAllReservations() => Reservations;

        // Aktualizacja rezerwacji
        public void UpdateReservation(int id, Reservation updatedReservation)
        {
            if (updatedReservation == null)
            {
                throw new ArgumentNullException(nameof(updatedReservation), "Zaktualizowana rezerwacja nie może być null.");
            }

            var reservation = GetReservation(id); // Jeśli rezerwacja nie istnieje, rzucamy wyjątek

            // Sprawdzamy, czy pokój jest dostępny podczas aktualizacji
            var room = roomRepository.GetRoom(updatedReservation.RoomId);
            if (room == null)
            {
                throw new KeyNotFoundException("Pokój o podanym ID nie istnieje.");
            }

            if (!room.IsAvailable)
            {
                throw new InvalidOperationException("Pokój nie jest dostępny.");
            }

            // Sprawdzamy, czy data rezerwacji jest w przyszłości
            if (updatedReservation.CheckIn <= DateTime.Now)
            {
                throw new ArgumentException("Data zameldowania musi być w przyszłości.");
            }

            reservation.GuestName = updatedReservation.GuestName;
            reservation.RoomId = updatedReservation.RoomId;
            reservation.CheckIn = updatedReservation.CheckIn;
            reservation.CheckOut = updatedReservation.CheckOut;
        }

        // Usunięcie rezerwacji
        public void DeleteReservation(int id)
        {
            var reservation = GetReservation(id); // Jeśli rezerwacja nie istnieje, rzucamy wyjątek
            Reservations.Remove(reservation);

            // Zmiana dostępności pokoju po usunięciu rezerwacji
            var room = roomRepository.GetRoom(reservation.RoomId);
            room.IsAvailable = true;
        }
    }
}
