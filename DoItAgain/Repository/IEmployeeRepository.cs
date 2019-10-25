using DoItAgain.Context;
using DoItAgain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoItAgain.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetName(string name);
        void Create(Employee employee);
        bool Commit();
        Employee GetById(int Id);
        void Del(int Id);
        Employee LastManStanding { get; }
        void Repair(Employee employee);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public Employee LastManStanding => _appDbContext.Employees.LastOrDefault();

        public bool Commit()
        {
            try
            {
                 _appDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
          
        }

        public void Create(Employee employee)
        {
            
            _appDbContext.Add(employee);
        }

        public void Del(int Id)
        {
            var delEmployee = GetById(Id);

            _appDbContext.Remove(delEmployee);
        }

        public Employee GetById(int Id)
        {
            return _appDbContext.Employees.Find(Id);
        }

        public IEnumerable<Employee> GetName(string name)
        {
            if(name == null)
            {
                return _appDbContext.Employees.ToList();
            }
            else
            {
                return _appDbContext.Employees.Where(x => x.Name.Contains(name));
            }
        }

        public void Repair(Employee employee)
        {
            var demo = _appDbContext.Employees.Attach(employee);
            demo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
