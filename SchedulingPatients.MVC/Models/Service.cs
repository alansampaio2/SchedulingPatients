using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulingPatients.MVC.Models
{
    /// <summary>
    /// Consulta de Hipertensos e diabéticos, de Gestante, Puericultura, Acupuntura, etc
    /// </summary>
    public class Service
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Código")]
        public int ServiceID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Descrição")]
        public string Title { get; set; }

        [Display(Name = "Departamento")]
        public int DepartmentID { get; set; }

        [Display(Name = "Departamento")]
        public Department Department { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<ServiceAssignment> ServiceAssignments { get; set; }

    }
}
