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
}
