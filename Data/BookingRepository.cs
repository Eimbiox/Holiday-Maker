using MySqlConnector;

class BookingRepository
{
    DbConnection DbConnection = new DbConnection();

    public async Task<bool> IsRoomFreeAsync(int roomId, DateOnly checkInDate, DateOnly checkOutDate)   //Returns true if room is free
    {
        await DbConnection.connection.OpenAsync();
        try
        {
            string sql = """    
                SELECT id
                FROM bookings
                WHERE room_id = @roomId
                    AND check_in_date < @checkOutDate
                    AND check_out_date > @checkInDate
                """;    //Checks for rooms that are taken
            using var command = new MySqlCommand(sql, DbConnection.connection);
            command.Parameters.AddWithValue("@roomId", roomId);
            command.Parameters.AddWithValue("@checkInDate", checkInDate);
            command.Parameters.AddWithValue("@checkOutDate", checkOutDate);
            await command.PrepareAsync();

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())   //If there is a row that means the room is not free so we return false
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        finally
        {
            await DbConnection.connection.CloseAsync();
        }
    }

    public async Task<int> CreateBookingAsync(Booking booking)   //Saves the booking in the database and returns its id (the booking number)
    {
        await DbConnection.connection.OpenAsync();
        try
        {
            string sql = """
                INSERT INTO bookings (customer_id, room_id, check_in_date, check_out_date, number_of_people)
                VALUES (@customerId, @roomId, @checkInDate, @checkOutDate, @numberOfPeople)
                """;
            using var command = new MySqlCommand(sql, DbConnection.connection);
            command.Parameters.AddWithValue("@customerId", booking.CustomerId);
            command.Parameters.AddWithValue("@roomId", booking.RoomId);
            command.Parameters.AddWithValue("@checkInDate", booking.CheckInDate);
            command.Parameters.AddWithValue("@checkOutDate", booking.CheckOutDate);
            command.Parameters.AddWithValue("@numberOfPeople", booking.NumberOfPeople);
            await command.PrepareAsync();
            await command.ExecuteNonQueryAsync();

            return checked((int)command.LastInsertedId);
        }
        finally
        {
            await DbConnection.connection.CloseAsync();
        }
    }
}