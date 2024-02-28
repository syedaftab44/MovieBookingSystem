using MovieBookingSystem.Models;
using System.Collections.ObjectModel;

namespace MovieBookingSystem.ResponseModel
{
    public record TheaterResponse
    {
        public long TheaterId { get; set; }
        public string TheaterName { get; set; }
        public string MovieTitle { get; set; }
        public int TotalNumberOfSeats { get; set; }
        public Collection<Seat> Seats { get; set; }
        public Collection<Booking> Bookings { get; set; }
    }
}
