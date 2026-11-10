class App
{
    private ConsoleUI _consoleUI = new ConsoleUI();
    private AccommodationRepository _accommodationRepository = new AccommodationRepository();
    private BookingRepository _bookingRepository = new BookingRepository();
    private CustomerRepository _customerRepository = new CustomerRepository();
    private RoomRepository _roomRepository = new RoomRepository();

    public async Task RunAsync()
    {
        while (true)
        {
            int choice = _consoleUI.MainMenu();

            if (choice == 1)
            {
                Customer? customer = await CheckIfCustomerAlreadyExistsAsync();

                if (customer == null)
                {
                    bool createCustomer = _consoleUI.WantToCreateCustomer();
                    if (!createCustomer)
                    {
                        continue;
                    }
                    customer = await CreateNewCustomerAsync();
                }
                CustomerMenu(customer);
            }
            else if (choice == 2)
            {

            }
            else if (choice == 3)
            {
                return;
            }
        }
    }

    private async Task<Customer?> CheckIfCustomerAlreadyExistsAsync()   //Returns a customer based on their email
    {
        string email = _consoleUI.AskForCustomerEmail();
        Customer? customer = await _customerRepository.GetCustomerByEmailAsync(email);
        return customer;
    }

    private async Task<Customer> CreateNewCustomerAsync()   //Creates a new customer and adds them to database
    {

        string firstName = _consoleUI.ReadCustomString("Skriv in kundens förnamn:");    //TODO: Add saftey
        string lastName = _consoleUI.ReadCustomString("Skriv in kundens efternamn:");   //TODO: Add saftey
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

    private void CustomerMenu(Customer customer)
    {
        int choice = _consoleUI.CustomerMenu(customer);

        if (choice == 1)
        {

        }
        else if (choice == 2)
        {

        }
        else if (choice == 3)
        {

        }
        else if (choice == 4)
        {

        }
        else if (choice == 5)
        {

        }
        else if (choice == 6)
        {
            return;
        }
    }

    private async Task SearchMenu()
    {
        bool success = false;
        SearchFilter searchFilter;

        while (!success)
        {
            string location = _consoleUI.ReadCustomString("Skriv in orten: ");
            int checkInDayDate = _consoleUI.ReadDateDay("Skriv in inchecknings datum som dag i december månad: ");
            int checkOutDayDate = _consoleUI.ReadDateDay("Skriv in utchecknings datum som dag i december månad: ");
            DateOnly checkInDate = new DateOnly(2026, 12, checkInDayDate);
            DateOnly checkOutDate = new DateOnly(2026, 12, checkOutDayDate);
            int peopleAmount = _consoleUI.ReadNotNegativeInt("Skriv in antalet personer");

            try
            {
                searchFilter = new SearchFilter(location, checkInDate, checkOutDate, peopleAmount);
                success = true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _consoleUI.PrintMessageWithReadKey(ex.Message);
            }
        }

    }
}
