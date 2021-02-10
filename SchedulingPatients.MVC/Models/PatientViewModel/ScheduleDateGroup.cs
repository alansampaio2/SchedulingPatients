using System;
using System.ComponentModel.DataAnnotations;

namespace SchedulingPatients.MVC.Models.PatientViewModel
{
    public class ScheduleDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? ScheduleDate { get; set; }
        public int PatientCount { get; set; }
    }
}
