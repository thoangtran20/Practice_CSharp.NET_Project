using ExportAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ExportAppDemo.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)  
        {
            
        }
        public DbSet<UserMasterModel> UserMasters { get; set; }
    }
}
