using MySqlConnector;

class CustomerRepository
{
    DbConnection dbConnection = new DbConnection();

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        await dbConnection.connection.OpenAsync();
        using var command = new MySqlCommand("SELECT * FROM customers WHERE email = @email", dbConnection.connection);
        command.Parameters.AddWithValue("@email", email);

        await command.PrepareAsync();
        await using var reader = await command.ExecuteReaderAsync();

        try
        {
            if (await reader.ReadAsync())
            {
                return new Customer(
                    reader.GetInt32("id"),
                    reader.GetString("first_name"),
                    reader.GetString("last_name"),
                    reader.GetString("email"),
                    reader.GetString("phone_number")
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
}