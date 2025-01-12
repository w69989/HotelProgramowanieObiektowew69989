namespace HotelManagement
{
    public interface IReservationRepository
    {
        void AddReservation(Reservation reservation);
        Reservation GetReservation(int id);
        IEnumerable<Reservation> GetAllReservations();
        void DeleteReservation(int id);
    }
}

