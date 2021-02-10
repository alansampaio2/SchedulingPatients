using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingPatients.MVC.Models
{
    /// <summary>
    /// Employee e Services
    /// </summary>
    public class ServiceAssignment
    {
        public int EmployeeID { get; set; }
        public int ServiceID { get; set; }

        public Employee Employee { get; set; }
        public Service Service { get; set; }
    }
}
