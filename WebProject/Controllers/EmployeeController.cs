using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using WebProject.DAL.Entities;
using WebProject.Repository;

namespace WebProject.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly DepartmentRepository _departmentRepository;

        // Inject DepartmentRepository to get list of departments
        public EmployeeController(EmployeeRepository employeeRepository, DepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        // GET: Employee
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }

        // GET: Employee/Details/5
        public IActionResult Details(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        // GET: Employee/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Fetch all departments for the dropdown
            ViewBag.Departments = new SelectList(_departmentRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            
            
                var result = _employeeRepository.Add(employee);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
            

           
            ViewBag.Departments = new SelectList(_departmentRepository.GetAll(), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employee/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return NotFound();

            // Fetch departments for the dropdown
            ViewBag.Departments = new SelectList(_departmentRepository.GetAll(), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
                var result = _employeeRepository.Update(employee);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            ViewBag.Departments = new SelectList(_departmentRepository.GetAll(), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employee/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee != null)
            {
                _employeeRepository.Delete(employee);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
