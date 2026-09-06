using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IActionResult Create()
        {
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
            return View(appointment);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
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
            return View(appointment);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
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
            if (ModelState.IsValid)
            {
                _db.Appointments.Remove(appointment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

    }
}
