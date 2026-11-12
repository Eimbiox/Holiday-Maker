class SearchFilter
{
    public string Location { get; private set; }
    public DateOnly CheckInDate { get; private set; }
    public DateOnly CheckOutDate { get; private set; }
    public int NumberOfPeople { get; private set; }
    public bool Pool { get; private set; }
    public bool Restaurant { get; private set; }
    public int? MaxDistanceToSea { get; private set; }
    public int? MaxDistanceToCityCenter { get; private set; }
    public SortOption SortOption { get; private set; }

    public SearchFilter(string location, DateOnly checkInDate, DateOnly checkOutDate, int numberOfPeople)   //Constructor
    {
        Location = location;
        CheckInDate = checkInDate;
        if (checkOutDate <= checkInDate)
        {
            throw new ArgumentOutOfRangeException(nameof(checkOutDate), "Utcheckningsdatum måste vara efter incheckningsdatum.");
        }
        else
        {
            CheckOutDate = checkOutDate;
        }
        
        if (numberOfPeople <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfPeople), "Kan inte vara 0 eller under");
        }
        else
        {
            NumberOfPeople = numberOfPeople;
        }
    }

    public void TogglePool()
    {
        Pool = !Pool;
    }

    public void ToggleRestaurant()
    {
        Restaurant = !Restaurant;
    }

    public void SetNumberOfPeople(int number)
    {
        if (number <= 0)
        {
            return;
        }
        NumberOfPeople = number;
    }

    public void SetMaxDistanceToSea(int distance)
    {
        if (distance < 0)
        {
            return;
        }
        else if (distance == 0)
        {
            MaxDistanceToSea = null;
            return;
        }
        MaxDistanceToSea = distance;
    }

    public void SetMaxDistanceToCityCenter(int distance)
    {
        if (distance < 0)
        {
            return;
        }
        else if (distance == 0)
        {
            MaxDistanceToCityCenter = null;
            return;
        }
        MaxDistanceToCityCenter = distance;
    }

    public void SetSortOption(SortOption sortOption)
    {
        SortOption = sortOption;
    }
}