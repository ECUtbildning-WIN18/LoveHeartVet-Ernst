using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LH_Vet._0._1.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public virtual ICollection<AppointmentToPatient> AppointmentsToPatients { get; set; }
    }
}