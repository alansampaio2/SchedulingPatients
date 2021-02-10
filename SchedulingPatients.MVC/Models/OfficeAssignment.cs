using System.ComponentModel.DataAnnotations;

namespace SchedulingPatients.MVC.Models
{
    /// <summary>
    /// Consultorio
    /// </summary>
    public class OfficeAssignment
    {
        [Key]
        public int EmployeeID { get; set; }

        [StringLength(50)]
        [Display(Name = "Consultório")]
        public string Location { get; set; }

        public Employee Employee { get; set; }
    }
}
