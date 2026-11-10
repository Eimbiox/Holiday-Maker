class Room
{
    public int Id { get; private set; }
    public string RoomNumber { get; private set; }
    public int MaximumPeople { get; private set; }
    public decimal PricePerNight { get; private set; }
    public int AccommodationId { get; private set; }
    public Accommodation Accommodation { get; private set; }

    public Room(int id, string roomNumber, int maximumPeople, decimal pricePerNight, Accommodation accommodation)
    {
        Id = id;
        RoomNumber = roomNumber;
        MaximumPeople = maximumPeople;
        PricePerNight = pricePerNight;
        Accommodation = accommodation;
        AccommodationId = accommodation.Id;
    }
}
