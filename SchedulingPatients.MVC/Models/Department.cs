using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchedulingPatients.MVC.Models
{
    /// <summary>
    /// Medicina e Enfermagem
    /// </summary>
    public class Department
    {
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Conselho de Classe")]
        public Category Category { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Service> Services { get; set; }
    }

    public enum Category
    {
        [Display(Name = "Enfermagem")]
        NURSING,
        [Display(Name = "Medicina")]
        MEDICINE
    }
}
