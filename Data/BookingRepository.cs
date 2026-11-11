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

    public async Task<List<Booking>> GetBookingsByCustomerAsync(Customer customer)   //Returns all bookings that belong to the customer with room and accommodation
    {
        await DbConnection.connection.OpenAsync();
        try
        {
            string sql = """
                SELECT bookings.id AS booking_id, bookings.check_in_date, bookings.check_out_date, bookings.number_of_people,
                rooms.id AS room_id, rooms.room_number, rooms.maximum_people, rooms.price_per_night,
                accommodations.id AS accommodation_id, accommodations.name, accommodations.location, accommodations.pool,
                accommodations.restaurant, accommodations.rating, accommodations.distance_to_beach, accommodations.distance_to_city_center
                FROM bookings
                JOIN rooms ON rooms.id = bookings.room_id
                JOIN accommodations ON accommodations.id = rooms.accommodation_id
                WHERE bookings.customer_id = @customerId
                ORDER BY bookings.check_in_date
                """;
            using var command = new MySqlCommand(sql, DbConnection.connection);
            command.Parameters.AddWithValue("@customerId", customer.Id);
            await command.PrepareAsync();

            await using var reader = await command.ExecuteReaderAsync();

            List<Booking> bookings = [];

            while (await reader.ReadAsync())
            {
                Accommodation accommodation = new Accommodation(
                    reader.GetInt32("accommodation_id"),
                    reader.GetString("name"),
                    reader.GetString("location"),
                    reader.GetBoolean("pool"),
                    reader.GetBoolean("restaurant"),
                    reader.GetInt32("rating"),
                    reader.GetInt32("distance_to_beach"),
                    reader.GetInt32("distance_to_city_center"));

                Room room = new Room(
                    reader.GetInt32("room_id"),
                    reader.GetString("room_number"),
                    reader.GetInt32("maximum_people"),
                    reader.GetDecimal("price_per_night"),
                    accommodation);

                Booking booking = new Booking(
                    customer,
                    room,
                    reader.GetDateOnly("check_in_date"),
                    reader.GetDateOnly("check_out_date"),
                    reader.GetInt32("number_of_people"),
                    reader.GetInt32("booking_id"));

                bookings.Add(booking);
            }
            return bookings;
        }
        finally
        {
            await DbConnection.connection.CloseAsync();
        }
    }

    public async Task<bool> RemoveBookingAsync(int bookingID)    //Returns true if there was a row(booking) that was removed
    {
        await DbConnection.connection.OpenAsync();
        try
        {
            string sql = """
            DELETE FROM bookings WHERE bookings.id = @bookingID
            """;
            using var command = new MySqlCommand(sql, DbConnection.connection);
            command.Parameters.AddWithValue("@bookingID", bookingID);
            await command.PrepareAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;

        }
        finally
        {
            await DbConnection.connection.CloseAsync();
        }
    }
}