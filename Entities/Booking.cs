class Booking
{
    public int Id { get; private set; }
    public int CustomerId { get; private set; }
    public int RoomId { get; private set; }
    public DateOnly CheckInDate { get; private set; }
    public DateOnly CheckOutDate { get; private set; }
    public int NumberOfPeople { get; private set; }
    public Customer Customer { get; private set; }
    public Room Room { get; private set; }

    public Booking(Customer customer, Room room, DateOnly checkInDate, DateOnly checkOutDate, int numberOfPeople, int id = 0)
    {
        Id = id;
        Customer = customer;
        CustomerId = customer.Id;
        Room = room;
        RoomId = room.Id;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        NumberOfPeople = numberOfPeople;
    }

    public void UpdateId(int id)
    {
        Id = id;
    }

    public decimal GetTotalPrice()   //Number of nights times the price per night
    {
        int nights = CheckOutDate.DayNumber - CheckInDate.DayNumber;
        return nights * Room.PricePerNight;
    }
}
