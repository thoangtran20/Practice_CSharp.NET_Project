using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.RoomTypeDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingAPI.Repository
{
    public class RoomTypeRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        public RoomTypeRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<List<RoomTypeDTO>> RetrieveAllRoomTypeAsync(bool? IsActive)
        {
            using var connection = _connectionFactory.CreateConnection();
            var command = new SqlCommand("spGetAllRoomTypes", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@IsActive", (object)IsActive ?? DBNull.Value);
            await connection.OpenAsync();   
            using var reader = await command.ExecuteReaderAsync();
            var roomTypes = new List<RoomTypeDTO>();
            while(reader.Read())
            {
                roomTypes.Add(new RoomTypeDTO()
                {
                    RoomTypeID = reader.GetInt32("RoomTypeID"),
                    TypeName = reader.GetString("TypeName"),
                    AccessibilityFeatures = reader.GetString("AccessibilityFeatures"),
                    Description = reader.GetString("Description"),
                    IsActive = reader.GetBoolean("IsActive")
                });
            }
            return roomTypes;
        }
        public async Task<RoomTypeDTO> RetrieveRoomTypeByIdAsync(int RoomTypeID)
        {
            using var connection = _connectionFactory.CreateConnection();
            var command = new SqlCommand("spGetRoomTypeById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@RoomTypeID", RoomTypeID);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (!reader.Read())
            {
                return null;
            }
            var roomType = new RoomTypeDTO
            {
                RoomTypeID = RoomTypeID,
                TypeName = reader.GetString("TypeName"),
                AccessibilityFeatures = reader.GetString("AccessibilityFeatures"),
                Description = reader.GetString("Description"),
                IsActive = reader.GetBoolean("IsActive")
            };
            return roomType;
        }

    }
}
