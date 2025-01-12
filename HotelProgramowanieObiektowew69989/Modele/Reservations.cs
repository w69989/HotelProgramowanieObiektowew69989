namespace HotelManagement
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public required string GuestName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}