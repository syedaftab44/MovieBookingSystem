
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBookingSystem.Models
{
    public class Booking
    {
        public long Id { get; set; }

        [ForeignKey(nameof(UserId))]

        public long UserId { get; set; }

        [ForeignKey(nameof(TheaterId))]
        public long TheaterId { get; set; }
        [ForeignKey(nameof(SeatId))]

        public int SeatId { get; set; }

        public Seat Seat { get; set; }
        
    }
}
