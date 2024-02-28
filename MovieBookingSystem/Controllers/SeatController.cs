using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBookingSystem.Models;

namespace MovieBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        readonly MovieContext movieContext;
        public SeatController(MovieContext _movieContext)
        {
            movieContext = _movieContext;
        }

        [HttpPost("addSeatNumber{theaterId}")]
        public async Task<IActionResult> AddSeatNumber(long theaterId)
        {
            var theater = await movieContext.Theaters.FindAsync((theaterId));
            if (theater == null)
            {
                throw new Exception("Invalid theater id");
            }

            int TotalSeat = theater.TotalNumberOfSeats;
            string[] alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" ,"M", "N", "O", "P", "Q", "R", "S", "T", "U", "V" };
            int numberOfRows = TotalSeat / 20; //in one row 20 seats

            var seatNumberContainer = new List<Seat>();
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    string seatNumber = alphabets[i] + j;
                    var newSeat = new Seat()
                    {
                        TheaterId = theaterId,
                        SeatNumber = seatNumber,
                        IsAvailable = true,
                    };
                    seatNumberContainer.Add(newSeat);
                }
            }
            await movieContext.Seats.AddRangeAsync(seatNumberContainer);
            await movieContext.SaveChangesAsync();
            return Ok("seat added successfully");
        }
        //----------------------------------------------------------------------------------------
//        int totalSeats = theater.TotalNumberOfSeats;
//        int seatsPerRow = 20;
//        int numberOfRows = totalSeats / seatsPerRow;

//        var seatNumberContainer = new List<Seat>();

//for (int i = 0; i<numberOfRows; i++)
//{
//    for (int j = 1; j <= seatsPerRow; j++)
//    {
//        char rowChar = (char)('A' + i);
//        string seatNumber = rowChar+j.ToString();

//        var newSeat = new Seat()
//        {
//            TheaterId = theaterId,
//            SeatNumber = seatNumber,
//            IsAvailable = true,
//        };

//        seatNumberContainer.Add(newSeat);
//    }
//}
//---------------------------------------------------------------------------------------------


[HttpGet("{theaterId}theatertotalSeat")]
        public async Task<IActionResult> GetAllSeat(long theaterId)
        {
            var theater = await movieContext.Theaters.FindAsync(theaterId);
            if (theater == null)
            {
                throw new Exception("Invalid theater id");
            }
            var totalSeat = movieContext.Seats.Select(s => s.SeatNumber);
            return Ok(totalSeat);
        }
    }
}
