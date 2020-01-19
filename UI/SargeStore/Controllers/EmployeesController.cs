using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SargeStoreDomain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using SargeStoreDomain.Entities.Identity;
using SargeStore.Interfaces.Services;

namespace SargeStore.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

        public IActionResult Index()
        {
            return View(_EmployeesData.GetAll());
        }

        public IActionResult Details(int? Id)
        {
            if (Id is null)
                return BadRequest();
            var employee = _EmployeesData.GetById((int)Id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

        public IActionResult DetailsName(string FirstName, string LastName)
        {
            if (FirstName is null && LastName is null)
                return BadRequest();
            IEnumerable<EmployeeView> employees = _EmployeesData.GetAll();
            if (!string.IsNullOrWhiteSpace(FirstName))
                employees = employees.Where(e => e.FirstName == FirstName);
            if (!string.IsNullOrWhiteSpace(LastName))
                employees = employees.Where(e => e.LastName == LastName);

            var employee = employees.FirstOrDefault();

            if (employee is null)
                return NotFound();

            return View(nameof(Details), employee);
        }

        public IActionResult Create() => View(new EmployeeView());

        [HttpPost]
        public IActionResult Create(EmployeeView NewEmployee)
        {
            if (!ModelState.IsValid)
                return View(NewEmployee);

            _EmployeesData.Add(NewEmployee);
            _EmployeesData.SaveChanges();

            return RedirectToAction("Details", new { NewEmployee.Id });
        } 

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit (int? Id)
        {
            if (Id is null) return View(new EmployeeView()); //Для создания сотрудника

            if (Id < 0)
                return BadRequest();
            var employee = _EmployeesData.GetById((int)Id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(EmployeeView Employee)
        {
            if (Employee is null)
                throw new ArgumentNullException(nameof(Employee));

            if(Employee.Age < 18)
            {
                ModelState.AddModelError(nameof(EmployeeView.Age), "Возраст не может быть меньше 18 лет");
            }

            if (!ModelState.IsValid)
                View(Employee);

            var id = Employee.Id;
            if(id == 0)
            {
                _EmployeesData.Add(Employee);
            }
            else
                _EmployeesData.Edit(id, Employee);

            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();
            return View(employee);

            //_EmployeesData.Delete(id);
            //return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeleteConfirmed(int Id)
        {
            _EmployeesData.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}