using Microsoft.EntityFrameworkCore;
using MovieBookingSystem.Models;
using System;

namespace MovieBookingSystem
{
    public class MovieContext:DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

      //  protected override void OnModelCreating(ModelBuilder modelBuilder)
      //  {
            //modelbuilder.entity<seat>().hasdata(
            //    new seat() { id = 1, seatnumber = "a1", isavailable = true, theaterid = 1 },
            //    new seat() { id = 2, seatnumber = "a2", isavailable = true, theaterid = 1 },
            //    new seat() { id = 3, seatnumber = "a3", isavailable = true, theaterid = 1 },
            //    new seat() { id = 4, seatnumber = "a4", isavailable = true, theaterid = 1 },
            //    new seat() { id = 5, seatnumber = "a5", isavailable = true, theaterid = 1 }
            //    );

            //modelBuilder.Entity<Theater>().HasData(
            //    new Theater() { Id = 1, TheaterName = "Cinepolis", TotalNumberOfSeats = 5, MovieTitle = "Captain America" }
            //    );
       // }

        public DbSet<User> Users { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Owner> Owner { get; set; }
    }
}
