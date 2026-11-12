class App
{
    private ConsoleUI _consoleUI = new ConsoleUI();
    private AccommodationRepository _accommodationRepository = new AccommodationRepository();
    private BookingRepository _bookingRepository = new BookingRepository();
    private CustomerRepository _customerRepository = new CustomerRepository();
    private RoomRepository _roomRepository = new RoomRepository();

    public async Task RunAsync()    //Runs the program and calls the methods needed
    {
        while (true)
        {
            int choice = _consoleUI.MainMenu();

            if (choice == 1)
            {
                Customer? customer = await GetOrCreateCustomerAsync();

                if (customer == null)
                {
                    continue;
                }
                await CustomerMenuAsync(customer);
            }
            else if (choice == 2)
            {
                await SearchMenuAsync(null);
            }
            else if (choice == 3)
            {
                return;
            }
        }
    }

    private async Task<Customer?> GetOrCreateCustomerAsync()   //Finds the customer by email, or offers to create one. Returns null if the user declines
    {
        Customer? customer = await CheckIfCustomerAlreadyExistsAsync();

        if (customer == null)
        {
            bool createCustomer = _consoleUI.WantToCreateCustomer();
            if (!createCustomer)
            {
                return null;
            }
            customer = await CreateNewCustomerAsync();
        }
        return customer;
    }

    private async Task<Customer?> CheckIfCustomerAlreadyExistsAsync()   //Returns a customer based on their email
    {
        string email = _consoleUI.AskForCustomerEmail();
        Customer? customer = await _customerRepository.GetCustomerByEmailAsync(email);
        return customer;
    }

    private async Task<Customer> CreateNewCustomerAsync()   //Creates a new customer and adds them to database
    {

        string firstName = _consoleUI.ReadName("Skriv in kundens förnamn");  
        string lastName = _consoleUI.ReadName("Skriv in kundens efternamn");
        string email = _consoleUI.ReadCustomString("Skriv in kundens email:");  //TODO: Add saftey
        Customer? existingCustomer = await _customerRepository.GetCustomerByEmailAsync(email);

        if (existingCustomer != null)  //If customer email already exists we return it and dont create a new one
        {
            _consoleUI.PrintMessageWithReadKey($"Kunden finns redan, använder befintlig kund");
            return existingCustomer;
        }

        string phoneNumber = _consoleUI.ReadCustomString("Skriv in kundens telefon nummer:");

        Customer customer = new Customer(firstName, lastName, email, phoneNumber);

        int id = await _customerRepository.CreateNewCustomerAsync(customer);

        customer.UpdateId(id);

        return customer;
    }

    private async Task CustomerMenuAsync(Customer customer)
    {
        while (true)   //Stays in the customer menu until the user picks 5
        {
            int choice = _consoleUI.CustomerMenu(customer);

            if (choice == 1)
            {
                await SearchMenuAsync(customer);
            }
            else if (choice == 2)
            {
                List<Booking> bookings = await _bookingRepository.GetBookingsByCustomerAsync(customer);
                int bookingChoice = _consoleUI.ReadBookingChoice(customer, bookings, "avboka");   //The Nr shown on screen, not the real id

                if (bookingChoice == 0)   //0 means cancel 
                {
                    continue;
                }

                Booking booking = bookings[bookingChoice - 1];   //Nr 1 is in slot [0] in the list
                bool removedBooking = await _bookingRepository.RemoveBookingAsync(booking.Id);

                if (removedBooking)
                {
                    _consoleUI.PrintMessageWithReadKey($"Bokning {booking.Id} har tagits bort från {customer.FirstName}");
                }
                else
                {
                    _consoleUI.PrintMessageWithReadKey("Något gick fel, testa igen senare");
                }

            }
            else if (choice == 3)
            {
                List<Booking> bookings = await _bookingRepository.GetBookingsByCustomerAsync(customer);
                int bookingChoice = _consoleUI.ReadBookingChoice(customer, bookings, "ändra");   //The Nr shown on screen, not the real id

                if (bookingChoice == 0)   //0 means cancel
                {
                    continue;
                }

                Booking booking = bookings[bookingChoice - 1];   //Nr 1 is in slot [0] in the list
                int newNumberOfPeople = _consoleUI.ReadNewNumberOfPeople(booking.Room.MaximumPeople);   //Can never be more than the room allows
                bool updatedBooking = await _bookingRepository.UpdateAmountOfPeopleInBookingAsync(booking.Id, newNumberOfPeople);

                if (updatedBooking)
                {
                    _consoleUI.PrintMessageWithReadKey($"Bokning {booking.Id} har nu {newNumberOfPeople} personer");
                }
                else
                {
                    _consoleUI.PrintMessageWithReadKey("Bokningen fanns inte längre, den kan ha avbokats");
                }
            }
            else if (choice == 4)
            {
                List<Booking> bookings = await _bookingRepository.GetBookingsByCustomerAsync(customer);
                _consoleUI.PrintBookings(customer, bookings);
                _consoleUI.PrintMessageWithReadKey("");
            }
            else if (choice == 5)
            {
                return;
            }
        }
    }

    private async Task SearchMenuAsync(Customer? customer)   //customer is null when searching from the main menu
    {
        SearchFilter searchFilter;

        while (true)
        {
            string location = _consoleUI.ReadCustomString("Skriv in orten: ");
            int checkInDayDate = _consoleUI.ReadDateDay("Skriv in inchecknings datum som dag i december månad: ");
            int checkOutDayDate = _consoleUI.ReadDateDay("Skriv in utchecknings datum som dag i december månad: ");
            DateOnly checkInDate = new DateOnly(2026, 12, checkInDayDate);
            DateOnly checkOutDate = new DateOnly(2026, 12, checkOutDayDate);
            int peopleAmount = _consoleUI.ReadNotNegativeInt("Skriv in antalet personer: ");

            try
            {
                searchFilter = new SearchFilter(location, checkInDate, checkOutDate, peopleAmount);
                break;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _consoleUI.PrintMessageWithReadKey(ex.Message);
            }
        }

        while (true)
        {
            List<Room> rooms = await _roomRepository.GetFreeRoomsAsync(searchFilter);

            int choice = _consoleUI.SearchMenu(searchFilter, rooms);
            if (choice == 1)
            {
                searchFilter.TogglePool();
            }
            else if (choice == 2)
            {
                searchFilter.ToggleRestaurant();
            }
            else if (choice == 3)
            {
                int distance = _consoleUI.ReadNotNegativeInt("Max avstånd till strand i meter (0 = inget filter): ");
                searchFilter.SetMaxDistanceToSea(distance);
            }
            else if (choice == 4)
            {
                int distance = _consoleUI.ReadNotNegativeInt("Max avstånd till centrum i meter (0 = inget filter): ");
                searchFilter.SetMaxDistanceToCityCenter(distance);
            }
            else if (choice == 5)
            {
                searchFilter.SetSortOption(SortOption.Price);
            }
            else if (choice == 6)
            {
                searchFilter.SetSortOption(SortOption.Rating);
            }
            else if (choice == 7)
            {
                if (rooms.Count == 0)
                {
                    _consoleUI.PrintMessageWithReadKey("Det finns inga lediga rum att boka.");
                    continue;
                }

                int roomChoice = _consoleUI.ReadRoomChoice(rooms.Count);
                if (roomChoice == 0)
                {
                    continue;
                }
                Room room = rooms[roomChoice - 1];   //The list starts at 0 but the Nr shown starts at 1

                if (customer == null)   //No active customer yet so we need one before booking
                {
                    customer = await GetOrCreateCustomerAsync();
                    if (customer == null)
                    {
                        continue;
                    }
                }

                bool isFree = await _bookingRepository.IsRoomFreeAsync(room.Id, searchFilter.CheckInDate, searchFilter.CheckOutDate);
                if (!isFree)   //Someone else booked the room after the search was made
                {
                    _consoleUI.PrintMessageWithReadKey("Rummet har precis blivit bokat av någon annan. Välj ett annat rum.");
                    continue;
                }

                Booking booking = new Booking(customer, room, searchFilter.CheckInDate, searchFilter.CheckOutDate, searchFilter.NumberOfPeople);
                int bookingId = await _bookingRepository.CreateBookingAsync(booking);
                booking.UpdateId(bookingId);

                _consoleUI.PrintBookingConfirmation(booking);
                return;
            }
            else if (choice == 0)
            {
                return;
            }
        }
    }
}
