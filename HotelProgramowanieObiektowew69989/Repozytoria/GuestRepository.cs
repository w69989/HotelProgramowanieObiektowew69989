using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HotelManagement
{
    public class GuestRepository : IGuestRepository
    {
        private List<Guest> Guests = new List<Guest>();

        // Dodanie gościa z walidacją
        public void AddGuest(Guest guest)
        {
            if (guest == null)
            {
                throw new ArgumentNullException(nameof(guest), "Gość nie może być null.");
            }

            if (string.IsNullOrWhiteSpace(guest.FullName))
            {
                throw new ArgumentException("Imię i nazwisko gościa muszą być wypełnione.");
            }

            // Walidacja adresu email
            if (!Regex.IsMatch(guest.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Adres e-mail gościa jest nieprawidłowy.");
            }

            // Sprawdzamy, czy gość już istnieje
            if (Guests.Any(g => g.Email == guest.Email))
            {
                throw new InvalidOperationException("Gość o tym adresie e-mail już istnieje.");
            }

            guest.Id = Guests.Count > 0 ? Guests.Max(g => g.Id) + 1 : 1;
            Guests.Add(guest);
        }

        // Pobranie gościa po ID
        public Guest GetGuest(int id)
        {
            var guest = Guests.FirstOrDefault(g => g.Id == id);
            if (guest == null)
            {
                throw new KeyNotFoundException($"Gość o ID {id} nie został znaleziony.");
            }
            return guest;
        }

        // Pobranie wszystkich gości
        public IEnumerable<Guest> GetAllGuests() => Guests;

        // Aktualizacja gościa
        public void UpdateGuest(int id, Guest updatedGuest)
        {
            if (updatedGuest == null)
            {
                throw new ArgumentNullException(nameof(updatedGuest), "Zaktualizowany gość nie może być null.");
            }

            var guest = GetGuest(id); // Jeśli gość nie istnieje, rzucimy wyjątek

            // Walidacja e-mailu
            if (!Regex.IsMatch(updatedGuest.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Adres e-mail gościa jest nieprawidłowy.");
            }

            guest.FullName = updatedGuest.FullName;
            guest.Email = updatedGuest.Email;
        }

        // Usunięcie gościa
        public void DeleteGuest(int id)
        {
            var guest = GetGuest(id); // Jeśli gość nie istnieje, rzucimy wyjątek
            Guests.Remove(guest);
        }
    }
}
