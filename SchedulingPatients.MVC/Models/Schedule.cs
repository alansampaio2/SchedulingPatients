using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingPatients.MVC.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int ServiceID { get; set; }
        public int PatientID { get; set; }
        public Group? Group { get; set; }
        public DateTime Date { get; set; }

        public Service Service { get; set; }
        public Patient Patient { get; set; }

    }

    public enum Group
    {
        PREGNANT,
        CHILD,
        DIABETIC_HYPERTENSE,
        HANSENIASE,
        TUBERCULOSIS,
        FERTIL_AGE_WOMEN,

    }
}
