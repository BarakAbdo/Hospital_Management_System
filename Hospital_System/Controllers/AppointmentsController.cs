using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _db;
        public AppointmentsController(AppDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            //Entity Framework Approach      
            IEnumerable<Appointment> appointments = _db.Appointments.Include(e=>e.Patient)
                .Include(e=>e.Doctor).ToList();
            return View(appointments);

        }
        public void GetDoctor() 
        {
            IEnumerable<Doctor> doctors = _db.Doctors.ToList();
            SelectList doctorSelectList = new SelectList(doctors,"Id" ,"Name");
            ViewBag.DoctorSelectList = doctorSelectList;
        }

        public void GetPatient() 
        {
            IEnumerable<Patient> patients = _db.Patients.ToList();
            SelectList patientSelectList = new SelectList(patients,"Id","Name" );
            ViewBag.patientSelectList = patientSelectList;
        }

        [HttpGet]
        public IActionResult Create()
        {
            GetDoctor();
            GetPatient();

            return View();
        }


        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _db.Appointments.Add(appointment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetDoctor();
            GetPatient();
            return View(appointment);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            GetDoctor();
            GetPatient();
            var appointment = _db.Appointments.Find(Id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }


        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _db.Appointments.Update(appointment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetDoctor();
            GetPatient();
            return View(appointment);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            GetDoctor();
            GetPatient();
            var appointment = _db.Appointments.Find(Id);
            if (appointment == null)
            {
                return NotFound();
            }
            
            return View(appointment);
        }


        [HttpPost]
        public IActionResult Delete(Appointment appointment)
        {
            GetDoctor();
            GetPatient();
            var appointments = _db.Appointments.Find(appointment.Id);
            if (appointments == null)
            {
                return NotFound();
            }

            _db.Appointments.Remove(appointments);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        //public IActionResult Details(int id)
        //{
        //    //ViewBag.Departments = _db.Departments.ToList();
        //    var appointment = _db.Appointments.Include(e => e.Patient).Include(e => e.Doctor).ToList();
        //    if (appointment == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(appointment);
        //}


    }
}
