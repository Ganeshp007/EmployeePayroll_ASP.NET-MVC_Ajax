namespace EmployeePayroll_MVC_AJAX.Models
{
    using Microsoft.EntityFrameworkCore;

    public class EmployeePayrollDbContext : DbContext
    {
        public EmployeePayrollDbContext(DbContextOptions<EmployeePayrollDbContext> options)
            : base(options)
        { }

        public DbSet<EmployeeModel> Employee { get; set; }
    }

}
