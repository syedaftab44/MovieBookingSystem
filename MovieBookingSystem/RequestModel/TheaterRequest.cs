namespace MovieBookingSystem.RequestModel
{
    public record TheaterRequest
    {
        public string TheaterName { get; set; }
        public string MovieTitle { get; set; }
        public int TotalNumberOfSeats { get; set; }
    }
}
