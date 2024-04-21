using ExportAppDemo.Data;
using ExportAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ExportAppDemo.Repository
{
    public class ReportingConcrete : IReporting
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        public ReportingConcrete(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }
        public List<UserMasterViewModel> GetUserwiseReport()
        {
            try
            {
                var listofusers = (from usermaster in _databaseContext.UserMasters.AsNoTracking()
                                   select new UserMasterViewModel()
                                   {
                                       UserName = usermaster.UserName,
                                       FirstName = usermaster.FirstName,
                                       LastName = usermaster.LastName,
                                       CreatedOn = usermaster.CreatedOn,
                                       EmailId = usermaster.EmailId,
                                       Gender = usermaster.Gender == "M" ? "Male" : "Female",
                                       MobileNo = usermaster.MobileNo,
                                       Status = usermaster.Status == true ? "Active" : "InActive",

                                   }).ToList();

                return listofusers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
