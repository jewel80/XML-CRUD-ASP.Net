using Microsoft.EntityFrameworkCore;

namespace ImportXmlData.Models
{
    public class AppDataContext : DbContext
    {


        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {

        }
        public DbSet<EmployeeModel> employeeModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
