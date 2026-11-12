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

    public string ReadName(string promt)   //Only letters
    {
        Console.Write(promt);

        while (true)
        {
            string? input = Console.ReadLine()?.Trim();
            bool isValid = !string.IsNullOrWhiteSpace(input) && input.Length <= 255;

            if (isValid)
            {
                foreach (char c in input!)
                {
                    if (!char.IsLetter(c) && c != '-' && c != ' ')   //A character that is not a letter, hyphen or space is not allowed
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (isValid)
            {
                return input!;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Namnet får bara innehålla bokstäver, bindestreck och mellanslag: ");
            Console.ResetColor();
        }
    }

    public string ReadEmail(string promt)   //Must look like name@domain.se
    {
        Console.Write(promt);

        while (true)
        {
            string? input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input) && input.Length <= 255 && !input.Contains(' '))
            {
                int atIndex = input.IndexOf('@');          //-1 if there is no @
                int lastDotIndex = input.LastIndexOf('.');

                bool somethingBeforeAt = atIndex > 0;
                bool onlyOneAt = atIndex == input.LastIndexOf('@');
                bool dotAfterAt = lastDotIndex > atIndex + 1;              //There must be something between @ and the dot
                bool somethingAfterDot = lastDotIndex < input.Length - 1;

                if (somethingBeforeAt && onlyOneAt && dotAfterAt && somethingAfterDot)
                {
                    return input;
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Skriv en giltig email, t.ex. anna@exempel.se: ");
            Console.ResetColor();
        }
    }

    public string ReadPhoneNumber(string promt)   //Swedish phonenumber starts with 07 and has exactly 10 digits
    {
        Console.Write(promt);

        while (true)
        {
            string? input = Console.ReadLine()?.Trim();
            bool isValid = !string.IsNullOrEmpty(input) && input.Length == 10 && input.StartsWith("0");

            if (isValid)
            {
                foreach (char c in input!)
                {
                    if (!char.IsDigit(c))   //Only digits are allowed
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (isValid)
            {
                return input!;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Telefonnumret måste börja på 0 och ha 10 siffror, t.ex. 0701234567:");
            Console.ResetColor();
        }
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

        Console.WriteLine($"1. Boka\n2. Avboka\n3. Ändra bokning\n4. Visa bokningar\n5. Tillbaka");
        
        int input;
        Console.Write("\nVälj ett av alternativen: ");
        while (!int.TryParse(Console.ReadLine(),out input) || input < 1 || input > 5 )
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

    public void PrintBookings(Customer customer, List<Booking> bookings)
    {
        Console.Clear();
        Logo();
        Console.WriteLine($"BOKNINGAR FÖR: {customer.FirstName} {customer.LastName}\n");

        if (bookings.Count == 0)
        {
            Console.WriteLine("Kunden har inga bokningar.\n");
            return;
        }

        Console.WriteLine($"{"Nr",-4}{"Boknr",-7}{"Boende",-25}{"Ort",-8}{"Rum",-6}{"Datum",-25}{"Pers",-6}{"Totalpris",12}");
        for (int i = 0; i < bookings.Count; i++)
        {
            Booking booking = bookings[i];
            string dates = $"{booking.CheckInDate} - {booking.CheckOutDate}";
            Console.WriteLine($"{i + 1,-4}{booking.Id,-7}{booking.Room.Accommodation.Name,-25}{booking.Room.Accommodation.Location,-8}{booking.Room.RoomNumber,-6}{dates,-25}{booking.NumberOfPeople,-6}{booking.GetTotalPrice(),12:0.00}");
        }
        Console.WriteLine();
    }

    public int ReadBookingChoice(Customer customer, List<Booking> bookings, string action)   //Shows the bookings and returns the Nr the user picked, 0 means cancel. action is e.g. "avboka" or "ändra"
    {
        PrintBookings(customer, bookings);

        if (bookings.Count == 0)
        {
            PrintMessageWithReadKey("");   //So the user has time to read "Kunden har inga bokningar"
            return 0;
        }

        int input;
        Console.Write($"Välj bokning att {action} (Nr 1-{bookings.Count}), 0 för att avbryta: ");
        while (!int.TryParse(Console.ReadLine(), out input) || input < 0 || input > bookings.Count)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Välj ett nummer mellan 0 och {bookings.Count}: ");
            Console.ResetColor();
        }
        return input;
    }

    public int ReadNewNumberOfPeople(int maximumPeople)   //Only allows 1 up to the room's maximum
    {
        int input;
        Console.Write($"Nytt antal personer (1-{maximumPeople}): ");
        while (!int.TryParse(Console.ReadLine(), out input) || input < 1 || input > maximumPeople)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Rummet tar 1-{maximumPeople} personer, försök igen: ");
            Console.ResetColor();
        }
        return input;
    }

    public int ReadRoomChoice(int numberOfRooms)   //Returns the Nr the user picked from the list, 0 means cancel
    {
        int input;
        Console.Write($"Välj rum (Nr 1-{numberOfRooms}), 0 för att avbryta: ");
        while (!int.TryParse(Console.ReadLine(), out input) || input < 0 || input > numberOfRooms)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Välj ett nummer mellan 0 och {numberOfRooms}: ");
            Console.ResetColor();
        }
        return input;
    }

    public void PrintBookingConfirmation(Booking booking)
    {
        Console.Clear();
        Logo();
        Console.WriteLine("BOKNINGSBEKRÄFTELSE\n");
        Console.WriteLine($"Bokningsnummer: {booking.Id}");
        Console.WriteLine($"Kund:           {booking.Customer.FirstName} {booking.Customer.LastName}");
        Console.WriteLine($"Boende:         {booking.Room.Accommodation.Name}, {booking.Room.Accommodation.Location}");
        Console.WriteLine($"Rum:            {booking.Room.RoomNumber}");
        Console.WriteLine($"Datum:          {booking.CheckInDate} - {booking.CheckOutDate}");
        Console.WriteLine($"Antal nätter:   {booking.GetNumberOfNights()}");
        Console.WriteLine($"Antal personer: {booking.NumberOfPeople}");
        Console.WriteLine($"Totalpris:      {booking.GetTotalPrice():0.00} kr");
        Console.WriteLine();
        PrintMessageWithReadKey("Bokningen är sparad.");
    }
}