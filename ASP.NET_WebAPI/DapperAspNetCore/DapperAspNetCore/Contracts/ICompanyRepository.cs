using DapperAspNetCore.Dto;
using DapperAspNetCore.Entities;

namespace DapperAspNetCore.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<Company> CreateCompany(CompanyForCreationDto company);
    }
}
