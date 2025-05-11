using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProject.DAL.Entities;
using WebProject.Repository;

namespace WebProject.Controllers
{
    [Authorize]

    public class DepartmentController : Controller
    {
        private readonly DepartmentRepository _repository;

        public DepartmentController(DepartmentRepository repository)
        {
            _repository = repository;
        }

        // GET: Department
        public IActionResult Index()
        {
            var departments = _repository.GetAll();
            return View(departments);
        }

        // GET: Department/Details/5
        public IActionResult Details(int id)
        {
            var department = _repository.GetById(id);
            if (department == null) return NotFound();
            return View(department);
        }

        // GET: Department/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
          var result= _repository.Add(department);
            if (result > 0)
            {
                department.CreationDate = DateTime.Now;
                return RedirectToAction("Index");
            }
                
            return View(department);
        }

        // GET: Department/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = _repository.GetById(id);
            if (department == null) return NotFound();
            return View(department);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department)
        {
           
            
            var result= _repository.Update(department);
            if (result>0)
                return RedirectToAction(nameof(Index));
           
            return View(department);
        }

        // GET: Department/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var department = _repository.GetById(id);
            if (department == null) return NotFound();
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _repository.GetById(id);
            if (department != null)
            {
                _repository.Delete(department);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
