namespace EmployeePayroll_MVC_AJAX.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using EmployeePayroll_MVC_AJAX.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class EmployeeController : Controller
    {
        private readonly EmployeePayrollDbContext employeePayrollDbContext;

        public EmployeeController(EmployeePayrollDbContext employeePayrollDbContext)
        {
            this.employeePayrollDbContext = employeePayrollDbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await employeePayrollDbContext.Employee.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployee(int Id = 0)
        {
            if (Id == 0)
            {
                return View(new EmployeeModel());
            }
            else
            {
                var emp = await employeePayrollDbContext.Employee.FindAsync(Id);
                if (emp == null)
                {
                    return NotFound();
                }

                return View(emp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(int Id, [Bind("Emp_Id,Name,Gender,Department,Notes")] EmployeeModel emps)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (Id == 0)
                {
                    employeePayrollDbContext.Add(emps);
                    await employeePayrollDbContext.SaveChangesAsync();
                }

                //// Update
                //else
                //{
                //    try
                //    {
                //        employeePayrollDbContext.Update(emps);
                //        await employeePayrollDbContext.SaveChangesAsync();
                //    }
                //    catch (DbUpdateConcurrencyException)
                //    {
                //        if (!EmployeeModelExists(emps.Emp_Id))
                //        {
                //            return NotFound();
                //        }
                //        else
                //        {
                //            throw;
                //        }
                //    }
                //}

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", employeePayrollDbContext.Employee.ToList()) });
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEmployee", emps) });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int? Id)
        {
            if (Id == 0)
            {
                return View(new EmployeeModel());
            }
            else
            {
                var emp = await employeePayrollDbContext.Employee.FindAsync(Id);
                if (emp == null)
                {
                    return NotFound();
                }

                return View(emp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployee(int Id, [Bind("Emp_Id,Name,Gender,Department,Notes")] EmployeeModel emps)
        {
            if (ModelState.IsValid)
            {
                // Update
                try
                {
                    employeePayrollDbContext.Update(emps);
                    await employeePayrollDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeModelExists(emps.Emp_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", employeePayrollDbContext.Employee.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEmployee", emps) });
        }

        // GET: Employee/DeleteEmployee
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empl = await employeePayrollDbContext.Employee.FirstOrDefaultAsync(m => m.Emp_Id == id);
            if (empl == null)
            {
                return NotFound();
            }

            return View(empl);
        }

        // POST: Employee/DeleteEmployee/5
        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empl = await employeePayrollDbContext.Employee.FindAsync(id);
            employeePayrollDbContext.Employee.Remove(empl);
            await employeePayrollDbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", employeePayrollDbContext.Employee.ToList()) });
        }

        private bool EmployeeModelExists(int id)
        {
            return employeePayrollDbContext.Employee.Any(e => e.Emp_Id == id);
        }
    }
}
