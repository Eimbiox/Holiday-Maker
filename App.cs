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

            if (choice == 1)    //Book a customer
            {
                Customer? customer = await CheckIfCustomerAlreadyExistsAsync();
                if (customer == null)
                {
                    Console.WriteLine("customer dosnt exist");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"Found the email! {customer.Email}");
                    Console.ReadKey();
                }
            }
            else if (choice == 2)   //Show customer bookings
            {

            }
            else if (choice == 3)   //Change customer booking
            {

            }
            else if (choice == 4)   //Remove booking
            {

            }
            else if (choice == 5)   //Search for empty rooms
            {

            }
            else if (choice == 6)   //Exit
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
}
