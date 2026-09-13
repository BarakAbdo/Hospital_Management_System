using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class InvoicesController : Controller
    {
        //Dependancy Injaction
        private readonly AppDbContext _db;
        public InvoicesController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //Entity Framework Approach
            IEnumerable<Invoice> invoices = _db.Invoices.Include(e=>e.Patient).ToList();
            return View(invoices);
        }

        public void GetPatient() 
        {
            IEnumerable<Patient> patients = _db.Patients.ToList();
            SelectList patientSelectList = new SelectList(patients,"Id","Name");
            ViewBag.patientSelectList = patientSelectList; 
        }
        [HttpGet]
        public IActionResult Create()
        {

            GetPatient();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _db.Invoices.Add(invoice);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            GetPatient();
            return View(invoice);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            GetPatient();
            var invoice = _db.Invoices.Find(Id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);

        }
        [HttpPost]
        public IActionResult Edit(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _db.Invoices.Update(invoice);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetPatient();
            return View(invoice);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            GetPatient();
            var invoice = _db.Invoices.Find(Id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }
        [HttpPost]
        public IActionResult Delete(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _db.Invoices.Remove(invoice);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetPatient();
            return View(invoice);
        }
    }
}
