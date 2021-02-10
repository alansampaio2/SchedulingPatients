using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingPatients.MVC.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Contratação")]
        public DateTime HireDate { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Nº do Conselho")]
        public string Register { get; set; }

        [Display(Name = "Nome Completo")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public ICollection<ServiceAssignment> ServiceAssignments { get; set; }
        public OfficeAssignment OfficeAssignment { get; set; }
    }
}
