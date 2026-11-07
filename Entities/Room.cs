class Room
{
    public int id { get; private set; }
    public string roomNumber { get; private set; }
    public int maximumPeople { get; private set; }
    public decimal pricePerNight { get; private set; }
    public Accommodation accommodation { get; private set; }
}