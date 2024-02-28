using System.Collections.ObjectModel;

namespace MovieBookingSystem.Models
{
    public class Theater
    {
        public long Id { get; set; }
        public string TheaterName { get; set; }
        public string MovieTitle { get; set; }
        public int TotalNumberOfSeats { get; set; }
        public Collection<Seat> Seats { get; set; }

    }
}
