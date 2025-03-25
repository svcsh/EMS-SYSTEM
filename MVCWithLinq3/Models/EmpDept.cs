using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWithLinq3.Models
{
    public class EmpDept
    {
        public int Eid {  get; set; }  
        public string Ename { get; set; }
        public string Job {  get; set; }
        public decimal? Salary { get; set; }
        public int Did {  get; set; }
        public string Dname {  get; set; }
        public string Location {  get; set; }
        public List <SelectListItem> Departments {  get; set; }
    }
}