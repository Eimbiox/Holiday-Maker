using MySqlConnector;
class Connection
{ 
    public MySqlConnection connection { get; } = new MySqlConnection("Server=localhost;User ID=root;Password=kurslosenord;Database=holiday_maker"); //Should be hidden in a real program

    public async Task OpenConnection()
    {
        await connection.OpenAsync();
    }
    public async Task CloseConnection()
    {
        await connection.CloseAsync();
    }
}