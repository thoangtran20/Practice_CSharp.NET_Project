using BlazorDapperSPA.Data;
using BlazorDapperSPA.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BlazorDapperSPA.Service
{
    public class CityService : ICityService
    {
        private readonly SqlConnectionConfiguration _configuration;
        public CityService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateCity(City city)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"insert into dbo.City (Name,State) values (@Name, @State)";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(query, new { city.Name, city.State }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return true;
        }

        public async Task<bool> DeleteCity(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"delete from dbo.City where Id = @Id";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(query, new { id }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex; 
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return true;
        }

        public async Task<bool> EditCity(int id, City city)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"update dbo.City set Name = @Name, State = @State where Id = @Id";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(query, new { city.Name, city.State, id }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return true;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            IEnumerable<City> cities;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"select * from dbo.City";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    cities = await conn.QueryAsync<City>(query);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return cities;
        }

        public async Task<City> SingleCity(int id)
        {
            City city = new City();
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"select * from dbo.City where Id = @Id";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    city = await conn.QueryFirstOrDefaultAsync<City>(query, new { id }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return city;
        }
    }
}
