using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBookingSystem.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }

        [ForeignKey(nameof(TheaterId))]
        public long TheaterId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
