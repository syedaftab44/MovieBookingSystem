using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBookingSystem.Models;

namespace MovieBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        readonly MovieContext movieContext;
        public MovieController(MovieContext movieContext)
        {
            this.movieContext = movieContext;
        }

        //[HttpGet("totalNumberOfSeat")]
        //public async Task<IActionResult> GetSeat()
        //{
        //    var theater = await movieContext.Theaters.Select(t => new
        //    {
        //        TheaterName = t.TheaterName,
        //        TotalNumberOfSeats = t.TotalNumberOfSeats,
        //    }).ToListAsync();
        //    return Ok(theater);
        //}


        [HttpGet("totalNumberOfSeat{theaterId}")]
        public async Task<IActionResult> GetSeat(long theaterId)
        {
            var verifyTheater = await movieContext.Theaters.FindAsync(theaterId);
            if (verifyTheater == null)
            {
                throw new Exception("Invalid theater");
            }
            var theater = await movieContext.Theaters.Where(t => t.Id == theaterId).Select(t => new
            {
                TheaterName = t.TheaterName,
                TotalNumberOfSeats = t.TotalNumberOfSeats,
            }).ToListAsync();
            return Ok(theater);
        }

        //[HttpPost("remaningSeat{theaterId}")]
        //public async Task<IActionResult> GetRemainingSeat(long theaterId)
        //{
        //    if(! movieContext.Theaters.Any(x => x.Id == theaterId))
        //    {
        //        throw new Exception("Invalid Theater Id");
        //    }

        //    var seats = 
        //}

        [HttpGet("{theaterId}seats")]
        public async Task<IActionResult> GetAllSeat(long theaterId)
        {
            var theater = await movieContext.Theaters.FindAsync(theaterId);
            if (theater == null)
            {
                throw new Exception("Invalid theater id");
            }
            var seats = await movieContext.Seats.Where(theater => theater.TheaterId == theaterId).Select(t => new
            {
                SeatId = t.Id,
                SeatNumber = t.SeatNumber,
            }).ToListAsync();
            return Ok(seats);
        }

        [HttpPost("bookSeat/{theaterId}")]
        public async Task<IActionResult> BookSeat(long userId, long theaterId, int seatId)
        {
            if (!movieContext.Users.Any(u => u.Id == userId))
            {
                throw new Exception("Invalid user");
            }
            if (!movieContext.Theaters.Any(t => t.Id == theaterId))
            {
                throw new Exception("Invalid theater id");
            }

            var seat = movieContext.Seats.FirstOrDefault(seat => seat.TheaterId == theaterId && seat.Id == seatId && seat.IsAvailable);
            if (seat == null)
            {
                throw new Exception("Seat is not available");
            }

            var booking = new Booking()
            {
                UserId = userId,
                TheaterId = theaterId,
                SeatId = seatId,
            };
            await movieContext.Bookings.AddAsync(booking);
            //await movieContext.SaveChangesAsync();
            // update the details in seat 
            seat.IsAvailable = false;
            movieContext.Seats.Update(seat);
            await movieContext.SaveChangesAsync();
            return Ok("Seat booked successfully");
        }

        [HttpPut("cancelSeat/{userId}/{theaterId}/{seatId}")]
        public async Task<IActionResult> CancelSeat(long userId, long theaterId, int seatId)
        {
            var booking = await movieContext.Bookings
                .FirstOrDefaultAsync(b => b.UserId == userId && b.TheaterId == theaterId && b.SeatId == seatId);

            if (booking == null)
            {
                throw new Exception("Invalid User, Theater or Seat");
            }

            // Remove the booking
            var seat = await movieContext.Seats.FirstOrDefaultAsync(s => s.Id == seatId);
            if (seat != null)
            {
                seat.IsAvailable = true;
            }
            movieContext.Bookings.Remove(booking);
            await movieContext.SaveChangesAsync();
            return Ok("Seat canceled successfully");
        }
    }
}
