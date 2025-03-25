using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MVCWithLinq3.Models
{
    public class EmployeeDAL
    {
        MVCDBDataContext dc = new MVCDBDataContext(
ConfigurationManager.ConnectionStrings["MVCDBConnectionString"].ConnectionString); //New Version
        public List<SelectListItem> GetDepartments()
        {
            List<SelectListItem> Depts = new List<SelectListItem>();
            foreach (var Item in dc.Departments)
            {
                SelectListItem li = new SelectListItem { Text = Item.Dname, Value = Item.Did.ToString() };
                Depts.Add(li);
            }
            return Depts;
        }
        public EmpDept GetEmployee(int Eid, bool? Status)
        {
            dynamic Record;
            if (Status == null)
            {
                Record = (from E in dc.Employees
                          join D in dc.Departments on E.Did equals D.Did
                          where E.Eid == Eid
                          select
        new { E.Eid, E.Ename, E.Job, E.Salary, D.Did, D.Dname, D.Location }).Single();
            }
            else
            {
                Record = (from E in dc.Employees
                          join D in dc.Departments on E.Did equals D.Did
                          where E.Eid == Eid &&
               E.Status == Status
                          select new { E.Eid, E.Ename, E.Job, E.Salary, D.Did, D.Dname, D.Location }).Single();
            }
            EmpDept Emp = new EmpDept
            {
                Eid = Record.Eid,
                Ename = Record.Ename,
                Job = Record.Job,
                Salary = Record.Salary,
                Did = Record.Did,
                Dname = Record.Dname,
                Location = Record.Location
            };
            return Emp;
        }
        public List<EmpDept> GetEmployees(bool? Status)
        {
            dynamic Records;
            if (Status != null)
            {
                Records = from E in dc.Employees
                          join D in dc.Departments on E.Did equals D.Did
                          where E.Status == true
                          select new { E.Eid, E.Ename, E.Job, E.Salary, D.Did, D.Dname, D.Location };
            }
            else
            {
                Records = from E in dc.Employees
                          join D in dc.Departments on E.Did equals D.Did
                          select new
                          {
                              E.Eid,
                              E.Ename,
                              E.Job,
                              E.Salary,
                              D.Did,
                              D.Dname,
                              D.Location
                          };
            }
            List<EmpDept> Emps = new List<EmpDept>();
            foreach (var Record in Records)
            {
                EmpDept Emp = new EmpDept
                {
                    Eid = Record.Eid,
                    Ename = Record.Ename,
                    Job = Record.Job,
                    Salary = Record.Salary,
                    Did = Record.Did,
                    Dname = Record.Dname,
                    Location = Record.Location
                };
                Emps.Add(Emp);
            }
            return Emps;
        }
        public void Employee_Insert(EmpDept obj)
        {
            Employee Emp = new Employee
            {
                Ename = obj.Ename,
                Job = obj.Job,
                Salary = obj.Salary,
                Did = obj.Did,
                Status = true
            };
            dc.Employees.InsertOnSubmit(Emp);
            dc.SubmitChanges();
        }
        public void Employee_Update(EmpDept NewValues)
        {
            Employee OldValues = dc.Employees.Single(E => E.Eid == NewValues.Eid);
            OldValues.Ename = NewValues.Ename;
            OldValues.Job = NewValues.Job;
            OldValues.Salary = NewValues.Salary;
            OldValues.Did = NewValues.Did;
            dc.SubmitChanges();
        }
        public void Employee_Delete(int Eid)
        {
            Employee OldValues = dc.Employees.Single(E => E.Eid == Eid);
            OldValues.Status = false;
            dc.SubmitChanges();
        }
    }
}