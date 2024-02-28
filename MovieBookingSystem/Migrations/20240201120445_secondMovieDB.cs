using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class secondMovieDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Bookings_BookingId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_BookingId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Seats");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SeatId",
                table: "Bookings",
                column: "SeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Seats_SeatId",
                table: "Bookings",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Seats_SeatId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_SeatId",
                table: "Bookings");

            migrationBuilder.AddColumn<long>(
                name: "BookingId",
                table: "Seats",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_BookingId",
                table: "Seats",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Bookings_BookingId",
                table: "Seats",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }
    }
}
