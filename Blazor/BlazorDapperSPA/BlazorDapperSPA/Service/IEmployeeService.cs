using BlazorDapperSPA.Models;

namespace BlazorDapperSPA.Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> GetEmployees();
        Task<bool> CreateEmployee(Employee employee);
        Task<bool> EditEmployee(int id, Employee employee);
        Task<EmployeeModel> SingleEmployee(int id);
        Task<bool> DeleteEmployee(int id);
    }
}
