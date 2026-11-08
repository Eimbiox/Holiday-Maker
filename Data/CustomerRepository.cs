using MySqlConnector;

class CustomerRepository
{
    DbConnection dbConnection = new DbConnection();

    public async Task<Customer?> GetCustomerByEmailAsync(string email)  //Sends back a customer if the inserted string(email) matches one in the database
    {
        await dbConnection.connection.OpenAsync();
        try
        {
            using var command = new MySqlCommand("SELECT * FROM customers WHERE email = @email", dbConnection.connection);
            command.Parameters.AddWithValue("@email", email);

            await command.PrepareAsync();
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Customer(
                    reader.GetString("first_name"),
                    reader.GetString("last_name"),
                    reader.GetString("email"),
                    reader.GetString("phone_number"),
                    reader.GetInt32("id")
                );
            }
            else
            {
                return null;
            }
        }
        finally
        {
            await dbConnection.connection.CloseAsync();
        }
    }

    public async Task<int> CreateNewCustomerAsync(Customer customer)    //Creates a new customer in the database
    {
        await dbConnection.connection.OpenAsync();

        try
        {
            using var command = new MySqlCommand(
                "INSERT INTO customers (first_name, last_name, email, phone_number) VALUES (@firstName, @lastName, @email, @phoneNumber)", dbConnection.connection);
            command.Parameters.AddWithValue("@firstName", customer.FirstName);
            command.Parameters.AddWithValue("@lastName", customer.LastName);
            command.Parameters.AddWithValue("@email", customer.Email);
            command.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
            await command.PrepareAsync();
            await command.ExecuteNonQueryAsync();

            return checked((int)command.LastInsertedId);
        }
        finally
        {
            await dbConnection.connection.CloseAsync();
        }
    }
}