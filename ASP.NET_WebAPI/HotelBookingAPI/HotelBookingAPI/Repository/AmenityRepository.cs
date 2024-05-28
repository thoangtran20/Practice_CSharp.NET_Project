using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.AmenityDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingAPI.Repository
{
    public class AmenityRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        public AmenityRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<AmenityFetchResultDTO> FetchAmenitiesAsync(bool? isActive)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("spFetchAmenities", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IsActive", (object)isActive ?? DBNull.Value);
            var statusCode = new SqlParameter("@Status", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            var message = new SqlParameter("@Message", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(statusCode);
            command.Parameters.Add(message);
            await connection.OpenAsync();
            var amenities = new List<AmenityDetailsDTO>();
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    amenities.Add(new AmenityDetailsDTO
                    {
                        AmenityID = reader.GetInt32(reader.GetOrdinal("AmenityID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                    });
                }
            }
            // Important: Access output parameters after closing the reader, else you will get NULL
            return new AmenityFetchResultDTO
            {
                Amenities = amenities,
                IsSuccess = Convert.ToBoolean(statusCode.Value),
                Message = message.Value.ToString()
            };
        }
        public async Task<AmenityDetailsDTO> FetchAmenityByIdAsync(int amenityID)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("spFetchAmenityByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@AmenityID", amenityID);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new AmenityDetailsDTO
                {
                    AmenityID = reader.GetInt32(reader.GetOrdinal("AmenityID")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                };
            }
            else
            {
                return null;
            }
        }
        
    }
}
