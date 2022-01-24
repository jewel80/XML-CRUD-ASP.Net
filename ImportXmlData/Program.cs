using ImportXmlData.Controllers;
using ImportXmlData.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDataContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("myConnectionDb")));
builder.Services.AddScoped<IEmployee, EmploeeRepo>();
builder.Services.AddScoped< AppDataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EmployeeCon}/{action=listOfEmployee}/{id?}");

app.Run();
