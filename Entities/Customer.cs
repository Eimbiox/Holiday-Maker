class Customer
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }

    public Customer(string firstName, string lastName, string email, string phoneNumber, int id = 0)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void UpdateId(int id)
    {
        Id = id;
    }
}