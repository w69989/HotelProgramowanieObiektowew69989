namespace HotelManagement
{
    public interface IGuestRepository
    {
        void AddGuest(Guest guest);
        Guest GetGuest(int id);
        IEnumerable<Guest> GetAllGuests();
        void DeleteGuest(int id);
    }
}

