using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingPatients.MVC.Models.PatientViewModel
{
    public class EmployeeIndexData
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
    }
}
