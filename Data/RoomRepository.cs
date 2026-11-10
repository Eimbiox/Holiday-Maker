using MySqlConnector;

class RoomRepository
{
    DbConnection DbConnection = new DbConnection();

    public async Task<List<Room>> GetFreeRoomsAsync(SearchFilter searchFilter)
    {
        await DbConnection.connection.OpenAsync();
        try
        {
            string sql = """
            SELECT accommodations.id AS accommodation_id, accommodations.name, accommodations.location, accommodations.pool, 
            accommodations.restaurant, accommodations.rating, accommodations.distance_to_beach, 
            accommodations.distance_to_city_center,
            rooms.id AS room_id, rooms.room_number, rooms.maximum_people, rooms.price_per_night
            FROM accommodations
            JOIN rooms ON rooms.accommodation_id = accommodations.id
            WHERE accommodations.location = @location
                AND rooms.maximum_people >= @numberOfPeople
                AND rooms.id NOT IN (
            SELECT room_id
            FROM bookings
            WHERE check_in_date < @checkOutDate
                AND check_out_date > @checkInDate)
            """;
            using var command = new MySqlCommand(sql, DbConnection.connection);
            command.Parameters.AddWithValue("@location", searchFilter.Location);
            command.Parameters.AddWithValue("@numberOfPeople", searchFilter.NumberOfPeople);
            command.Parameters.AddWithValue("@checkOutDate", searchFilter.CheckOutDate);
            command.Parameters.AddWithValue("@checkInDate", searchFilter.CheckInDate);

            List<Room> rooms = [];

            await command.PrepareAsync();
            await using var reader = await command.ExecuteReaderAsync();

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
                
                rooms.Add(room);
            }
            return rooms;
        }
        finally
        {
            await DbConnection.connection.CloseAsync();
        }
    }
}