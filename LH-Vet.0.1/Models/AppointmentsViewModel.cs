using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/* We have an appointment model but that contains only info about appointments.
 * We need a model that has info about an appointment but also has info about the
 * patients associated with an appointment. */
namespace LH_Vet._0._1.Models
{
    public class AppointmentsViewModel
    {
        public int AppointmentId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        // Again, don't focus too much on the name CheckBoxViewModel. This is a list of the
        // patients that will have checkboxes attached to them. Every checkbox is a model
        // of a patient.
        public List<CheckBoxViewModel> Patients { get; set; }
    }
}