using HotelProgramowanieObiektowew69989.Modele;
using System.Collections.Generic;

namespace HotelManagement
{
    // Interfejs IRoomRepository
    public interface IRoomRepository
    {
        void AddRoom(Room room);
        Room GetRoom(int id);
        IEnumerable<Room> GetAllRooms();
        void UpdateRoom(int id, Room updatedRoom);
        void DeleteRoom(int id);
    }
}