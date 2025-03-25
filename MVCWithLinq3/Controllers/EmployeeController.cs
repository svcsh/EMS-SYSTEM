using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWithLinq3.Models;

namespace MVCWithLinq3.Controllers
{
    public class EmployeeController : Controller
    {

        EmployeeDAL obj = new EmployeeDAL();
        public ViewResult DisplayEmployees()
        {
            List<EmpDept> employees = obj.GetEmployees(true);
            return View(employees);
        }
        public ViewResult DisplayEmployee(int? eid)
        {
            if (eid == null)
            {
                ViewBag.Message = "Employee ID is missing!";
                return View("Error"); 
            }

            EmpDept employee = obj.GetEmployee(eid.Value, true);

            if (employee == null)
            {
                ViewBag.Message = "Employee not found!";
                return View("Error"); 
            }

            return View(employee);
        }

        [HttpGet]
        public ViewResult AddEmployee()
        {
            EmpDept emp = new EmpDept();
            emp.Departments = obj.GetDepartments();
            return View(emp);
        }
        [HttpPost]
        public RedirectToRouteResult AddEmployee(EmpDept emp) 
        {
            if (ModelState.IsValid)
            {
                obj.Employee_Insert(emp);
            }
            return RedirectToAction("DisplayEmployees");
        }
        public ViewResult EditEmployee(int? Eid)
        {
            if (Eid == null)
            {
                return View("Error"); 
            }

            EmpDept emp = obj.GetEmployee(Eid.Value, true);
            if (emp == null)
            {
                ViewBag.Message = "Employee not found.";
                return View("Error");
            }

            emp.Departments = obj.GetDepartments();
            return View(emp);
        }

        [HttpPost]
        public RedirectToRouteResult updateEmployee(EmpDept emp) 
        {
            obj.Employee_Update(emp);
            return RedirectToAction("DisplayEmployees");
        }
        public RedirectToRouteResult DeleteEmployee(int eid) 
        {
            obj.Employee_Delete(eid);
            return RedirectToAction("DisplayEmployee");
        }

    }
}