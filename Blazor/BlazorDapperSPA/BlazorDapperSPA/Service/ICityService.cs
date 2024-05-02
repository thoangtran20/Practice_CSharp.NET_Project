using BlazorDapperSPA.Models;

namespace BlazorDapperSPA.Service
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetCities();
        Task<bool> CreateCity(City city);
        Task<bool> EditCity(int id, City city);
        Task<City> SingleCity(int id);
        Task<bool> DeleteCity(int id);
    }
}
