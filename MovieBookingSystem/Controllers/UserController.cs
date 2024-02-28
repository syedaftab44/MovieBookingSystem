using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBookingSystem.Models;

namespace MovieBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly MovieContext movieContext;
        public UserController(MovieContext _movieContext)
        {
            movieContext = _movieContext;
        }
        [HttpPost("register{userName}")]
        public async Task<IActionResult> AddUser(string userName)
        {
            if (movieContext.Users.Any(user => user.UserName == userName))
            {
                throw new Exception("User already exists");
            }
            if (userName == null)
            {
                throw new Exception("Enter valid user");
            }
            User user = new User();
            user.UserName = userName;
            await movieContext.AddAsync(user);
            await movieContext.SaveChangesAsync();
            return Ok(user);
        }


        [HttpPost("login")]
        public IActionResult Login(long id, string username)
        {
            if (!movieContext.Users.Any(x => x.Id == id && x.UserName == username))
            {
                throw new Exception("Invalid User");
            }
            return Ok("Login suceesfully");
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await movieContext.Users.ToListAsync();
            return Ok(users);
        }


        [HttpGet("listOfBooking/{userId}/{theaterId}")]
        public async Task<IActionResult> GetAllBookings(long userId, long theaterId)
        {
            // Check if the user with the specified ID exists in the Bookings table
            //git update check
            if (!movieContext.Users.Any(u => u.Id == userId))
            {
                throw new Exception("Invalid user");
            }
            if (!movieContext.Theaters.Any(t => t.Id == theaterId))
            {
                throw new Exception("Invalid theater id");
            }
            //Retrieve bookings for the specified user ID
            //it is for all booking not based on theater

            //var bookings = await movieContext.Users.Include(x => x.Bookings)
            //               .Where(user => user.Id == userId)
            //               .Select(user => new
            //               {
            //                   UserName = user.UserName,
            //                   SeatName = movieContext.Seats.Where(x => user.Bookings.Select(booking =>
            //                             booking.SeatId).Contains(x.Id)).Select(x => x.SeatNumber).ToList(),
            //               }).ToListAsync();


            var bookings = await movieContext.Users.Include(x => x.Bookings)
               .Where(user => user.Id == userId)
               .Select(user => new
               {
                   UserName = user.UserName,
                   SeatName = movieContext.Bookings.Where(x=>x.UserId==userId)
                                                    .Select(x=>x.Seat.SeatNumber).ToList()  
                   //SeatName = movieContext.Seats.Where(x => user.Bookings.Select(booking =>
                   //          booking.SeatId).Contains(x.Id)).Where(x => x.TheaterId == theaterId).Select(x => x.SeatNumber).ToList(),
               }).ToListAsync();
            return Ok(bookings);
        }

    }
}


