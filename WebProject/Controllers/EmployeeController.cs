using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProject.DAL.Entities;
using WebProject.Repository;

namespace WebProject.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _repository;

        public EmployeeController(EmployeeRepository repository)
        {
            _repository = repository;
        }

        // GET: Employee
        public IActionResult Index()
        {
            var employees = _repository.GetAll();
            return View(employees);
        }

        // GET: Employee/Details/5
        public IActionResult Details(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        // GET: Employee/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            var result = _repository.Add(employee);
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View(employee);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            var result = _repository.Update(employee);
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View(employee);
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _repository.GetById(id);
            if (employee != null)
            {
                _repository.Delete(employee);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
