using System.Collections.Generic;
using System.Linq;
using WebProject.DAL.Contexts;
using WebProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebProject.Repository
{
    public class EmployeeRepository
    {
        private readonly ProjectDbContext _dbContext;

        public EmployeeRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees
                .Include(e => e.Department) 
                .AsNoTracking()
                .ToList();
        }

        public Employee GetById(int employeeId)
        {
            return _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        public int Add(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            return _dbContext.SaveChanges();
        }

        public int Update(Employee employee)
        {
            var existing = _dbContext.Employees.Find(employee.EmployeeId);
            if (existing == null) return 0;

            _dbContext.Entry(existing).CurrentValues.SetValues(employee);
            return _dbContext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            return _dbContext.SaveChanges();
        }

        public int DeleteById(int employeeId)
        {
            var employee = _dbContext.Employees.Find(employeeId);
            if (employee == null) return 0;

            _dbContext.Employees.Remove(employee);
            return _dbContext.SaveChanges();
        }


    }
}
