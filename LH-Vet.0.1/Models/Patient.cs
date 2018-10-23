using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LH_Vet._0._1.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Owner { get; set; }

        public virtual ICollection<AppointmentToPatient> AppointmentsToPatients { get; set; }
    }
}