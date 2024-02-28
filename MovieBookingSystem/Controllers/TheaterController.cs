using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBookingSystem.Models;
using MovieBookingSystem.RequestModel;

namespace MovieBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheaterController : ControllerBase
    {
        readonly MovieContext movieContext;
        public TheaterController(MovieContext _movieContext)
        {
            this.movieContext = _movieContext;
        }

        [HttpPost("addTheater")]
        public async Task<IActionResult> AddTheater([FromBody] TheaterRequest theaterRequest)
        {
            var theater = new Theater()
            {
                TheaterName = theaterRequest.TheaterName,
                MovieTitle = theaterRequest.MovieTitle,
                TotalNumberOfSeats = theaterRequest.TotalNumberOfSeats
            };
            await movieContext.AddAsync(theater);
            await movieContext.SaveChangesAsync();
            return Ok(theater);
        }

        [HttpGet("totalTheater")]
        public async Task<IActionResult> GetAllTheater()
        {
            var theaters = await movieContext.Theaters.ToListAsync();
            return Ok(theaters);
        }

        [HttpGet("{theaterId}theaterById")]
        public async Task<IActionResult> GetTheaterById(long theaterId)
        {
            var theater = await movieContext.Theaters.FindAsync(theaterId);
            if (theater == null)
            {
                throw new Exception("Invalid theater id");
            }
            return Ok(theater);
        }

        //[HttpGet("{theaterId}bookedSeat")]
        //public async Task<IActionResult> GetBookedSeats(long theaterId)
        //{
        //    var theater = await movieContext.Theaters.FindAsync(theaterId);
        //    if (theater == null)
        //    {
        //        throw new Exception("Invalid theater");
        //    }
        //    var bookedSeats = movieContext.Seats.Where(x => x.TheaterId == theaterId && !x.IsAvailable)
        //                      .Select(y => y.SeatNumber);
        //    return Ok(bookedSeats);
        //}

        //[HttpGet("{theaterId}availableSeats")]
        //public async Task<IActionResult> GetAllAvailableSeats(long theaterId)
        //{
        //    var theater = await movieContext.Theaters.FindAsync(theaterId);
        //    if (theater == null)
        //    {
        //        throw new Exception("Invalid theater id");
        //    }
        //    var availableSeats = movieContext.Seats.Where(x => x.TheaterId == theaterId && x.IsAvailable)
        //                         .Select(y => y.SeatNumber);
        //    return Ok(availableSeats);
        //}

    }
}
