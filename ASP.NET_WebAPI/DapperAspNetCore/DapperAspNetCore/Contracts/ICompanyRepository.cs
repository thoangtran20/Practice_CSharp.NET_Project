using DapperAspNetCore.Dto;
using DapperAspNetCore.Entities;

namespace DapperAspNetCore.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<Company> CreateCompany(CompanyForCreationDto company);
        public Task UpdateCompany(int id, CompanyForCreationDto company);
        public Task DeleteCompany(int id);
        public Task<Company> GetCompanyByEmployeeId(int id);
        public Task<Company> GetCompanyEmployeesMultipleResults(int id);
        public Task<List<Company>> GetCompaniesEmployeesMultipleMapping();

    }
}
