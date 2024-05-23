using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.RoomDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingAPI.Repository
{
    public class RoomRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        public RoomRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<CreateRoomResponseDTO> CreateRoomAsync(CreateRoomRequestDTO request)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("spCreateRoom", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@RoomNumber", request.RoomNumber);
            command.Parameters.AddWithValue("@RoomTypeID", request.RoomTypeID);
            command.Parameters.AddWithValue("@Price", request.Price);
            command.Parameters.AddWithValue("@BedType", request.BedType);
            command.Parameters.AddWithValue("@ViewType", request.ViewType);
            command.Parameters.AddWithValue("@Status", request.Status);
            command.Parameters.AddWithValue("@IsActive", request.IsActive);
            command.Parameters.AddWithValue("@CreatedBy", "System");
            command.Parameters.Add("@NewRoomID", SqlDbType.Int).Direction = 
                ParameterDirection.Output;
            command.Parameters.Add("@StatusCode", SqlDbType.Int).Direction =
                ParameterDirection.Output;
            command.Parameters.Add("@Message", SqlDbType.NVarChar, 255).Direction =
                ParameterDirection.Output;
            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                var outputRoomID = command.Parameters["@NewRoomID"].Value;
                var newRoomID = outputRoomID != DBNull.Value ? Convert.ToInt32(outputRoomID) : 0; // Safely handle potential DBNull values.
                return new CreateRoomResponseDTO
                {
                    RoomID = newRoomID,
                    IsCreated = (int)command.Parameters["@StatusCode"].Value == 0,
                    Message = (string)command.Parameters["@Message"].Value
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating room: {ex.Message}", ex);
            }
        }
        
        public async Task<UpdateRoomResponseDTO> UpdateRoomAsync(UpdateRoomRequestDTO request)
        {

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("spUpdateRoom", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@RoomID", request.RoomID);
            command.Parameters.AddWithValue("@RoomNumber", request.RoomNumber);
            command.Parameters.AddWithValue("@RoomTypeID", request.RoomTypeID);
            command.Parameters.AddWithValue("@Price", request.Price);
            command.Parameters.AddWithValue("@BedType", request.BedType);
            command.Parameters.AddWithValue("@ViewType", request.ViewType);
            command.Parameters.AddWithValue("@Status", request.Status);
            command.Parameters.AddWithValue("@IsActive", request.IsActive);
            command.Parameters.AddWithValue("@ModifiedBy", "System");
            command.Parameters.Add("@StatusCode", SqlDbType.Int).Direction =
                ParameterDirection.Output;
            command.Parameters.Add("@Message", SqlDbType.NVarChar, 255).Direction =
                ParameterDirection.Output;
            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                return new UpdateRoomResponseDTO
                {
                    RoomID = request.RoomID,
                    IsUpdated = (int)command.Parameters["@StatusCode"].Value == 0,
                    Message = (string)command.Parameters["@Message"].Value
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating room: {ex.Message}", ex);
            }
        }
    }
}
