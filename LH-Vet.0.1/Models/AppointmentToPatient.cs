using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LH_Vet._0._1.Models
{
    public class AppointmentToPatient
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
                
        public virtual Appointment Appointment { get; set; }
        public virtual Patient Patient { get; set; }
    }
}