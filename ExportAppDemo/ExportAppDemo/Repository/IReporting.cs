using ExportAppDemo.Models;

namespace ExportAppDemo.Repository
{
    public interface IReporting
    {
        List<UserMasterViewModel> GetUserwiseReport();
    }
}
