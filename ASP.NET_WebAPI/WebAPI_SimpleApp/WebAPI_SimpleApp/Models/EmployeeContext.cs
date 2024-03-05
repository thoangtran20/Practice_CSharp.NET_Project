using Microsoft.EntityFrameworkCore;

namespace WebAPI_SimpleApp.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                FirstName = "Thoang",
                LastName = "Tran",
                Email = "thoangtran@gmail.com",
                DateOfBirth = new DateTime(2000, 10, 24),
                PhoneNumber = "0918814325"
            }, new Employee
            {
                EmployeeId = 2,
                FirstName = "Ken",
                LastName = "Phan",
                Email = "kenphan@gmail.com",
                DateOfBirth = new DateTime(1998, 08, 12),
                PhoneNumber = "0912324122"
            });
        }
    }
}
