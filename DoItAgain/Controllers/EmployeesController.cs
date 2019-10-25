using DoItAgain.Models;
using DoItAgain.Repository;
using DoItAgain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoItAgain.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public IActionResult Index(string SearchKeyword)
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            if(SearchKeyword == null)
            {
                employeeListViewModel.Employees = _employeeRepository.GetName(null);
            }
            else
            {
                employeeListViewModel.Employees = _employeeRepository.GetName(SearchKeyword);
            }
            return View(employeeListViewModel);
        }

        public IActionResult Create()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            TempData["Message"] = null;
            return View(employeeViewModel);
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            _employeeRepository.Create(employee);
            var result = _employeeRepository.Commit();
            if (result)
            {
                TempData["Message"] = "Create successful";
            }
            else
            {
                TempData["Message"] = "Create fail";
            }

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            _employeeRepository.Del(Id);
            _employeeRepository.Commit();   
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.Employee =  _employeeRepository.GetById(Id);
            if(employeeViewModel.Employee == null)
            {
                return NotFound();
            }
            return View(employeeViewModel);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            _employeeRepository.Repair(employee);
            var result = _employeeRepository.Commit();
            if (result)
            {
                TempData["Message"] = "Edit successful";
            }
            else
            {
                TempData["Message"] = "Edit fail";
            }
            return RedirectToAction("Index");
        }
    }
}
