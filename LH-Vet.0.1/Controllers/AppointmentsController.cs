using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LH_Vet._0._1.Models;

namespace LH_Vet._0._1.Controllers
{
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            return View(db.Appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            var Results = from p in db.Patients
                          select new
                          {
                              p.Id,
                              p.Name,
                              p.Owner,
                              // If the current appointment is associated with a patient in the
                              // AppointmentsToPatients table then set Checked to true.
                              Checked = ((from ap in db.AppointmentsToPatients
                                          where (ap.AppointmentId == id) && (p.Id == ap.PatientId)
                                          select ap).Count() > 0)
                          };
            var AssocViewModel = new AppointmentsViewModel();
            AssocViewModel.AppointmentId = appointment.Id;
            AssocViewModel.StartTime = appointment.StartTime;
            AssocViewModel.EndTime = appointment.EndTime;
            var myCheckBoxList = new List<CheckBoxViewModel>();
            var myCheckBoxViewModel = new CheckBoxViewModel();
            foreach (var r in Results)
            {
                myCheckBoxViewModel.Id = r.Id;
                myCheckBoxViewModel.Name = r.Name;
                myCheckBoxViewModel.Checked = r.Checked;
                myCheckBoxList.Add(myCheckBoxViewModel);
                myCheckBoxViewModel = null;
                myCheckBoxViewModel = new CheckBoxViewModel();
            }
            AssocViewModel.Patients = myCheckBoxList;
            return View(AssocViewModel);
            //return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartTime,EndTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // GET: Appointments/Edit/5
        [Authorize(Roles = "CanManageAppointments")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            var Results = from p in db.Patients
                          select new
                          {
                              p.Id,
                              p.Name,
                              p.Owner,
                              // If the current appointment is associated with a patient in the
                              // AppointmentsToPatients table then set Checked to true.
                              Checked = ((from ap in db.AppointmentsToPatients
                                          where (ap.AppointmentId == id) && (p.Id == ap.PatientId)
                                          select ap).Count() > 0)
                          };
            var AssocViewModel = new AppointmentsViewModel();
            AssocViewModel.AppointmentId = appointment.Id;
            AssocViewModel.StartTime = appointment.StartTime;
            AssocViewModel.EndTime = appointment.EndTime;
            var myCheckBoxList = new List<CheckBoxViewModel>();
            var myCheckBoxViewModel = new CheckBoxViewModel();
            foreach( var r in Results)
            {
                myCheckBoxViewModel.Id = r.Id;
                myCheckBoxViewModel.Name = r.Name;
                myCheckBoxViewModel.Checked = r.Checked;
                myCheckBoxList.Add(myCheckBoxViewModel);
                myCheckBoxViewModel = null;
                myCheckBoxViewModel = new CheckBoxViewModel();
            }
            AssocViewModel.Patients = myCheckBoxList;
            return View(AssocViewModel);
            //return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,StartTime,EndTime")] Appointment appointment)
        public ActionResult Edit(AppointmentsViewModel appointment)
        {
            // Here the edited appointment will be saved along with the patients that
            // are associated with it. 
            if (ModelState.IsValid)
            {
                var myAppointment = db.Appointments.Find(appointment.AppointmentId);
                myAppointment.StartTime = appointment.StartTime;
                myAppointment.EndTime = appointment.EndTime;

                /* Here we loop through the entries in the table AppointmentsToPatients.
                 * All the entries of the current appointment will be deleted so we can add
                 * the new entries. */
                foreach (var item in db.AppointmentsToPatients)
                {
                    if (item.AppointmentId == appointment.AppointmentId)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (var item in appointment.Patients)
                {
                    if (item.Checked)
                    {
                        db.AppointmentsToPatients.Add( new AppointmentToPatient { AppointmentId = appointment.AppointmentId, PatientId = item.Id } );
                    }
                }

                //db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
