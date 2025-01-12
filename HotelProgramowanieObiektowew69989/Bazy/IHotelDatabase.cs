using HotelProgramowanieObiektowew69989.Modele;

namespace HotelManagement
{
    public interface IHotelDatabase
    {
        void AddRoom(Room room);
        Room GetRoom(int id);
        void UpdateRoom(int id, Room updatedRoom);
        void DeleteRoom(int id);

        void AddReservation(Reservation reservation);
        Reservation GetReservation(int id);
        void DeleteReservation(int id);

        void AddGuest(Guest guest);
        Guest GetGuest(int id);
        void DeleteGuest(int id);
    }
}
