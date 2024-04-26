using DapperMVC.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperMVC.Data.Repository
{
    public interface IPersonRepository
    {
        Task<bool> AddAsync(Person person);
        Task<bool> UpdateAsync(Person person);
        Task<bool> DeleteAsync(int id);
        Task<Person?> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllPersonAsync();
    }
}
