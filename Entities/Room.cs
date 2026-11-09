class Room
{
    public int Id { get; private set; }
    public string RoomNumber { get; private set; }
    public int MaximumPeople { get; private set; }
    public decimal PricePerNight { get; private set; }
    public int AccommodationId { get; private set; }
    public Accommodation Accommodation { get; private set; }
}
