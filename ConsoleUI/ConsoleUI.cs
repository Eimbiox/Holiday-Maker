class ConsoleUI
{
    public int MainMenu()
    {
        Console.Clear();
        Logo();
        Console.WriteLine($"1. Kund meny\n2. Sök efter lediga rum\n3. Avsluta");
        
        int input;
        Console.Write("\nVälj ett av alternativen: ");
        while (!int.TryParse(Console.ReadLine(),out input) || input < 1 || input > 3 )
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Välj ett av alternativen: ");
            Console.ResetColor();
        }
        return input;
    }

    private void Logo()
    {
        Console.WriteLine(new string('=', 50));
        Console.WriteLine("TURBOBOKA 1997 EDITION PRO".PadLeft(38));
        Console.WriteLine(new string('=', 50));
    }

    public string AskForCustomerEmail()
    {
        Console.Write("Skriv in kundens email: ");
        string? input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input))
        {
            Console.Write("Skriv ett giltigt email");
            input = Console.ReadLine();
        }
        return input;

    }

    public string ReadCustomString(string promt)
    {
        Console.Write(promt);
        string? input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input))
        {
            Console.Write("Det får inte vara tomt");
            input = Console.ReadLine();
        }
        return input;
    }

    public int ReadNotNegativeInt(string promt)
    {
        Console.Write(promt);
        
        int input;
        while (!int.TryParse(Console.ReadLine(), out input) || input < 0)
        {
            Console.Write("Nummrett får inte vara negativt");
        }
        return input;
    }

    public int ReadDateDay(string promt)
    {
        Console.Write(promt);
        int input;
        while (!int.TryParse(Console.ReadLine(), out input) || input < 1 || input > 31)
        {
            Console.Write("Välj ett datum mellan 1 och 31");
        }
        return input;
    }

    public bool WantToCreateCustomer()
    {
        Console.Write("Kunden kunde inte hittas i systemet. Vill du skapa en ny kund? (Y/N): ");

        string? input = Console.ReadLine()?.ToUpper();

        while (input != "Y" && input != "N")
        {
            Console.WriteLine("Svara med Y eller N");
            input = Console.ReadLine()?.ToUpper();
        }
        if (input == "Y")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PrintMessageWithReadKey(string promt)
    {
        Console.WriteLine(promt);
        Console.WriteLine("Tryck en tangent för att fortsätta...");
        Console.ReadKey();
    }

    public int CustomerMenu(Customer customer)
    {
        Console.Clear();
        Console.WriteLine($"AKTIV KUND: {customer.FirstName} {customer.LastName}");

        Console.WriteLine($"1. Boka\n2. Avboka\n3. Ändra bokning\n4. Visa bokningar\n5. Ta bort kund\n6. ...");
        
        int input;
        Console.Write("\nVälj ett av alternativen: ");
        while (!int.TryParse(Console.ReadLine(),out input) || input < 1 || input > 6 )
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Välj ett av alternativen: ");
            Console.ResetColor();
        }
        return input;
    }

    public int SearchMenu(SearchFilter searchFilter, List<Room> roomsList)
    {
        Console.Clear();
        Logo();

        Console.WriteLine($"Sökning: {searchFilter.Location}, {searchFilter.CheckInDate} - {searchFilter.CheckOutDate}, {searchFilter.NumberOfPeople} personer\n");

        PrintFreeRooms(roomsList);

        string pool = searchFilter.Pool ? "PÅ" : "AV";
        string restaurant = searchFilter.Restaurant ? "PÅ" : "AV";
        string maxSea = searchFilter.MaxDistanceToSea == null ? "Inget" : $"{searchFilter.MaxDistanceToSea} m";
        string maxCityCenter = searchFilter.MaxDistanceToCityCenter == null ? "Inget" : $"{searchFilter.MaxDistanceToCityCenter} m";

        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"{$"1. Pool: {pool}",-40}2. Restaurang: {restaurant}");
        Console.WriteLine($"{$"3. Max avstånd till strand: {maxSea}",-40}4. Max avstånd till centrum: {maxCityCenter}");
        Console.WriteLine($"{"5. Sortera på pris (lägst först)",-40}6. Sortera på betyg (högst först)");
        Console.WriteLine($"{"7. Boka ett rum",-40}0. Tillbaka");
        Console.WriteLine($"\nAktiv sortering: {searchFilter.SortOption}");

        int input;
        Console.Write("\nVälj ett av alternativen: ");
        while (!int.TryParse(Console.ReadLine(), out input) || input < 0 || input > 7)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Välj ett av alternativen: ");
            Console.ResetColor();
        }
        return input;
    }

    private void PrintFreeRooms(List<Room> roomsList)
    {
        if (roomsList.Count == 0)
        {
            Console.WriteLine("Inga lediga rum hittades.\n");
            return;
        }

        Console.WriteLine($"{"Nr",-4}{"Boende",-25}{"Ort",-8}{"Rum",-6}{"Pers",-6}{"Betyg",-7}{"Pris/natt",10}");
        for (int i = 0; i < roomsList.Count; i++)
        {
            Room room = roomsList[i];
            Console.WriteLine($"{i + 1,-4}{room.Accommodation.Name,-25}{room.Accommodation.Location,-8}{room.RoomNumber,-6}{room.MaximumPeople,-6}{room.Accommodation.Rating,-7}{room.PricePerNight,10:0.00}");
        }
        Console.WriteLine();
    }
}