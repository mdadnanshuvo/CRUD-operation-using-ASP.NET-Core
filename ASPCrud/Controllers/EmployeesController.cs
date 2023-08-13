using ASPCrud.Data;
using ASPCrud.Models.Domain;
using ASPCrud.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCrud.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DbClass dbClass;

        public EmployeesController(DbClass dbClass)
        {
            this.dbClass = dbClass;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> index()
        {
            var employees = await dbClass.Employees.ToListAsync();

            return View(employees);
        }


        [HttpPost]
        public IActionResult Add(AddEmployeeViewModel model)
        {
            var employee = new Employee()
            {
                ID = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                Salary = model.Salary,
                DateofBirth = model.DateofBirth,
                Department = model.Department,
            };

            dbClass.Employees.Add(employee);
            dbClass.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var editEmployee = await dbClass.Employees.FirstOrDefaultAsync(x => x.ID == id);

            if (editEmployee != null)
            {
                var newModel = new UpdateEmployeeViewModel()
                {
                    ID = editEmployee.ID,
                    Name = editEmployee.Name,
                    Email = editEmployee.Email,
                    Salary = editEmployee.Salary,
                    DateofBirth = editEmployee.DateofBirth,
                    Department = editEmployee.Department,
                };

                return await Task.Run(() => View("View", newModel));
            }

            return RedirectToAction("index");
        }

        [HttpPost]

        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await dbClass.Employees.FindAsync(model.ID);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateofBirth = model.DateofBirth;



                await dbClass.SaveChangesAsync();
                return RedirectToAction("index");
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult>Delete(UpdateEmployeeViewModel model)
        {
               var employee = await dbClass.Employees.FindAsync(model.ID);

            if(employee != null)
            {
                dbClass.Employees.Remove(employee);
                await dbClass.SaveChangesAsync();


                return RedirectToAction("index");
            }

            return RedirectToAction("index");

               
        }


    }
}
