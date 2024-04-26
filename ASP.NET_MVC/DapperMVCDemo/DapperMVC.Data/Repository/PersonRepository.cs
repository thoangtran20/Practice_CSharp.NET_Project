using DapperMVC.Data.DataAccess;
using DapperMVC.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperMVC.Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ISqlDataAccess _dataAccess;
        private readonly IConfiguration _configuration;
        public PersonRepository(ISqlDataAccess dataAccess, IConfiguration configuration)
        {
            _dataAccess = dataAccess;
            _configuration = configuration;
        }
    
        public async Task<bool> AddAsync(Person person)
        {
            try
            {
                await _dataAccess.SaveData("sp_add_person", new
                {
                    name = person.FullName,
                    email = person.Email,
                    address = person.Address
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            try
            {
                await _dataAccess.SaveData("sp_update_person", new
                {
                    id = person.Id,
                    name = person.FullName,
                    email = person.Email,
                    address = person.Address
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _dataAccess.SaveData("sp_delete_person", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            IEnumerable<Person> result = await _dataAccess.GetData<Person, dynamic>
                ("sp_get_person", new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Person>> GetAllPersonAsync()
        {
            string query = "sp_get_Allperson";
            return await _dataAccess.GetData<Person, dynamic>(query, new { });
        }
    }
}
