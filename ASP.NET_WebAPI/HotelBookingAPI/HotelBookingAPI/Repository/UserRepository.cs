using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.UserDTOs;
using HotelBookingAPI.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingAPI.Repository
{
    public class UserRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        public UserRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<CreateUserResponseDTO> AddUserAsync(CreateUserDTO user)
        {
            CreateUserResponseDTO createUserResponseDTO = new CreateUserResponseDTO();
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("spAddUser", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Email", user.Email);
            // Convert password to hash here
            command.Parameters.AddWithValue("@PasswordHash", user.Password);
            command.Parameters.AddWithValue("@CreatedBy", "System");
            var userIdParam = new SqlParameter("@UserID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(errorMessageParam);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            var UserId = (int)userIdParam.Value;
            if (UserId != -1)
            {
                createUserResponseDTO.UserId = UserId;
                createUserResponseDTO.IsCreated = true;
                createUserResponseDTO.Message = "User Created Successfully";
                return createUserResponseDTO;
            }
            var message = errorMessageParam.Value?.ToString();
            createUserResponseDTO.IsCreated = false;
            createUserResponseDTO.Message = message ?? "An unknown error occurred while creating the user.";
            return createUserResponseDTO;      
        }
        public async Task<UserRoleResponseDTO> AssignRoleToUserAsync(UserRoleDTO userRole)
        {
            UserRoleResponseDTO userRoleResponseDTO = new UserRoleResponseDTO();
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("spAssignUserRole", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@UserID", userRole.UserID);
            command.Parameters.AddWithValue("@RoleID", userRole.RoleID);
            var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(errorMessageParam);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            var message = errorMessageParam.Value?.ToString();
            if (!string.IsNullOrEmpty(message)) 
            {
                userRoleResponseDTO.Message = message;
                userRoleResponseDTO.IsAssigned = false;
            }
            else
            {
                userRoleResponseDTO.Message = "User Role Assigned";
                userRoleResponseDTO.IsAssigned = true;
            }
            return userRoleResponseDTO; 
        }
        public async Task<List<UserResponseDTO>> ListAllUsersAsync(bool? isActive)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("spListAllUsers", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@IsActive", (object)isActive ?? DBNull.Value);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            var users = new List<UserResponseDTO>();
            while (reader.Read())
            {
                users.Add(new UserResponseDTO
                {
                    UserID = reader.GetInt32("UserID"),
                    Email = reader.GetString("Email"),
                    IsActive = reader.GetBoolean("IsActive"),
                    RoleID = reader.GetInt32("RoleID"),
                    LastLogin = reader.GetValueByColumn<DateTime?>("LastLogin"),
                });
            }
            return users;
        }
        public async Task<UserResponseDTO> GetUserByIdAsync(int userId)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("spGetUserByID", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@UserID", userId);
            var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(errorMessageParam);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (!reader.Read())
            {
                return null;
            }
            var user = new UserResponseDTO
            {
                UserID = reader.GetInt32("UserID"),
                Email = reader.GetString("Email"),
                IsActive = reader.GetBoolean("IsActive"),
                RoleID = reader.GetInt32("RoleID"),
                LastLogin = reader.GetValueByColumn<DateTime?>("LastLogin"),
            };
            return user;
        }
    }
}
