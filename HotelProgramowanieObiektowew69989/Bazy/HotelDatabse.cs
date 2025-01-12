using HotelProgramowanieObiektowew69989.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement
{
    public class HotelDatabase : IHotelDatabase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IGuestRepository _guestRepository;

        public HotelDatabase(IRoomRepository roomRepository, IReservationRepository reservationRepository, IGuestRepository guestRepository)
        {
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
            _guestRepository = guestRepository;
        }

        public void AddRoom(Room room) => _roomRepository.AddRoom(room);
        public Room GetRoom(int id) => _roomRepository.GetRoom(id);
        public void UpdateRoom(int id, Room updatedRoom) => _roomRepository.UpdateRoom(id, updatedRoom);
        public void DeleteRoom(int id) => _roomRepository.DeleteRoom(id);

        public void AddReservation(Reservation reservation) => _reservationRepository.AddReservation(reservation);
        public Reservation GetReservation(int id) => _reservationRepository.GetReservation(id);
        public void DeleteReservation(int id) => _reservationRepository.DeleteReservation(id);

        public void AddGuest(Guest guest) => _guestRepository.AddGuest(guest);
        public Guest GetGuest(int id) => _guestRepository.GetGuest(id);
        public void DeleteGuest(int id) => _guestRepository.DeleteGuest(id);
    }
}


