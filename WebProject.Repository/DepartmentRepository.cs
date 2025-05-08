using System;
using System.Collections.Generic;
using System.Linq;
using WebProject.DAL.Contexts;
using WebProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebProject.Repository
{
    public class DepartmentRepository
    {
        private readonly ProjectDbContext _dbContext;

        public DepartmentRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbContext.Departments
                .AsNoTracking()
                .ToList();
        }

        public Department GetById(int id)
        {
            return _dbContext.Departments.Find(id);
        }

        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

        public int Update(Department department)
        {
            var existing = _dbContext.Departments.Find(department.Id);
            if (existing == null) return 0;

            _dbContext.Entry(existing).CurrentValues.SetValues(department);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        public int DeleteById(int id)
        {
            var department = _dbContext.Departments.Find(id);
            if (department == null) return 0;

            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

       
    }
}
