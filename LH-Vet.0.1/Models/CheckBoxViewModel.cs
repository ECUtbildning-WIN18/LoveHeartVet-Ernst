using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/* Do not focus too much on the name CheckBox. It is not so relevant. This file will
 * help connect objects to each other and the means that will happen are checkboxes.
 * This file could might as well have the name ConnectionViewModel or
 * AssociateAppointmentWithPatientViewModel. But becuase this is learning and for clearitys
 * sake it is called CheckBox */
/* I think that in this case this model will hold the info of the patient. When i look at
 * an appointment the patients will be in a list of checkboxes. Each checkbox will have a patients
 * id and name associated with it. */
 
namespace LH_Vet._0._1.Models
{
    public class CheckBoxViewModel
    {
        // This is a unique id associated with the information associated with a checkbox.
        public int Id { get; set; }
        // This is the name that will appear next to the checkbox.
        public string Name { get; set; }
        // This will tell whether the checkbox is checked.
        public bool Checked { get; set; }
    }
}