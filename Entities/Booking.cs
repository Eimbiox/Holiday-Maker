class Booking
{
    public int id { get; private set; }
    public int customerID { get; private set; }
    public int roomId { get; private set; }
    public DateOnly checkInDate { get; private set; }
    public DateOnly checkOutDate { get; private set; }
    public int numberOfPeople { get; private set; }
    public Customer customer { get; private set; }
    public Room room { get; private set; }
}