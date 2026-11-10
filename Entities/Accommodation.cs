class Accommodation
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public bool Pool { get; private set; }
    public bool Restaurant { get; private set; }
    public int Rating { get; private set; }
    public int DistanceToBeach { get; private set; }
    public int DistanceToCityCenter { get; private set; }

    public Accommodation(int id, string name, string location, bool pool, bool restaurant, int rating, int distanceToBeach, int distanceToCityCenter)
    {
        Id = id;
        Name = name;
        Location = location;
        Pool = pool;
        Restaurant = restaurant;
        Rating = rating;
        DistanceToBeach = distanceToBeach;
        DistanceToCityCenter = distanceToCityCenter;
    }
}
