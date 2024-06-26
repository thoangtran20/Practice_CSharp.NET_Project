using Microsoft.EntityFrameworkCore;
using WebAPI_SimpleApp.Models;
using WebAPI_SimpleApp.Models.DataManager;
using WebAPI_SimpleApp.Models.Repository;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddDbContext<EmployeeContext>(
        opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ));
services.AddScoped<IDataRepository<Employee>, EmployeeManager>();
services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
