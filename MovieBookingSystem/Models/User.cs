using System.Collections.ObjectModel;

namespace MovieBookingSystem.Models
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public Collection<Booking> Bookings { get; set; }
    }
}
