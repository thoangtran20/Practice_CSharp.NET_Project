using BlazorDapperSPA.Data;
using BlazorDapperSPA.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BlazorDapperSPA.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly SqlConnectionConfiguration _configuration;
        public EmployeeService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateEmployee(Employee employee)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("Department", employee.Department, DbType.String);
            parameters.Add("Designation", employee.Designation, DbType.String);
            parameters.Add("Company", employee.Company, DbType.String);
            parameters.Add("CityId", employee.CityId, DbType.String);

            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync("AddEmployee", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<bool> DeleteEmployee(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync("DeleteEmployee", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<bool> EditEmployee(int id, Employee employee)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("Department", employee.Department, DbType.String);
            parameters.Add("Designation", employee.Designation, DbType.String);
            parameters.Add("Company", employee.Company, DbType.String);
            parameters.Add("CityId", employee.CityId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync("UpdateEmployee", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<EmployeeModel>> GetEmployees()
        {
            IEnumerable<EmployeeModel> employees;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    employees = await conn.QueryAsync<EmployeeModel>("GetAllEmployees", commandType: CommandType.StoredProcedure);
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
            return employees;
        }

        public async Task<EmployeeModel> SingleEmployee(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            EmployeeModel employee = new EmployeeModel();
            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    employee = await conn.QueryFirstOrDefaultAsync<EmployeeModel>("GetSingleEmployee", parameters, commandType: CommandType.StoredProcedure);
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
            return employee;
        }
    }
}
