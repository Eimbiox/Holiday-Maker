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
                Console.WriteLine($"{customer.FirstName}. {customer.Email}");
                Console.ReadLine();
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

    private async Task<Customer?> CheckIfCustomerAlreadyExistsAsync()
    {
       string email = _consoleUI.AskForCustomerEmail();
       Customer? customer =  await _customerRepository.GetCustomerByEmailAsync(email);
       return customer;
    }

    private async Task<Customer> CreateNewCustomerAsync()
    {

        string firstName = _consoleUI.ReadCustomString("Skriv in kundens förnamn:");
        string lastName = _consoleUI.ReadCustomString("Skriv in kundens efternamn:");
        string email = _consoleUI.ReadCustomString("Skriv in kundens email:");
        string phoneNumber = _consoleUI.ReadCustomString("Skriv in kundens telefon nummer:");

        Customer customer = new Customer(firstName, lastName, email, phoneNumber);

        int id = await _customerRepository.CreateNewCustomerAsync(customer);

        customer.UpdateId(id);

        return customer;
    }

    private void CustomerMenu()
    {
        
    }
}
