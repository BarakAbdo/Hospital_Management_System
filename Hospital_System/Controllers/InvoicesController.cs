using Hospital_System.Data;
using Hospital_System.Dtos.InvoicesDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.AppointmentRepo;
using Hospital_System.Repositories.InvoiceRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class InvoicesController : Controller
    {

        private readonly IInvoiceRepository _repo;
        public InvoicesController(IInvoiceRepository repo)
        {
            _repo = repo;

        }


        //Dependancy Injaction
        //private readonly AppDbContext _db;
        //public InvoicesController(AppDbContext db)
        //{
        //    _db = db;
        //}
        [HttpGet]
        public IActionResult Index()
        {

            //IEnumerable<Invoice> invoices = _repo.GetAll();
            var invoice = _repo.GetAll().Select(i => new InvoiceDto
            {
                Id = i.Id,
                UID = i.UID,
                Amount = i.Amount,
                Date = i.Date,
                Status = i.Status,
                PatientId = i.PatientId,
                PatientName = i.Patients.Name
            }).ToList();

            return View(invoice);
        }

        public void GetPatient() 
        {
            IEnumerable<Patient> patients = _repo.Patients.ToList();
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
        public IActionResult Create(CreateInvoiceDto invoice)
        {
            if (ModelState.IsValid)
            {
                var inv = new Invoice
                {
                    UID = Guid.NewGuid().ToString(),
                    Amount = invoice.Amount,
                    Date = invoice.Date,
                    Status = invoice.Status,
                    PatientId = invoice.PatientId,
                };

                _repo.Add(inv);
                _repo.Save();

                //_db.Invoices.Add(inv);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }

            GetPatient();
            return View(invoice);
        }
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            GetPatient();
            var invoice = _repo.GetByUId(uid);
            if (invoice == null)
            {
                return NotFound();
            }

            //var invoice = _repo.Invoices.FirstOrDefault(i=>i.UID == uid);
            var update = new UpdateInvoiceDto
            {
                Id = invoice.Id,
                UID = invoice.UID,
                Amount = invoice.Amount,
                Date = invoice.Date,
                Status = invoice.Status,
                PatientId = invoice.PatientId,

            };



            return View(update);

        }
        [HttpPost]
        public IActionResult Edit(UpdateInvoiceDto invoice)
        {
            if (ModelState.IsValid)
            {
                if (invoice.UID == null)
                    invoice.UID = Guid.NewGuid().ToString();
                var inv = new Invoice
                {
                    UID = invoice.UID,
                    Amount = invoice.Amount,
                    Date = invoice.Date,
                    Status = invoice.Status,
                    PatientId = invoice.PatientId,
                    Id = invoice.Id
                };

                _repo.Update(inv);
                _repo.Save();

                //_db.Invoices.Update(inv);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetPatient();
            return View(invoice);
        }
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            GetPatient();
            var invoice = _repo.GetByUId(uid);
            //var invoice = _repo.Invoices.FirstOrDefault(i=>i.UID == uid);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }
        [HttpPost]
        public IActionResult Delete(Invoice invoice)
        {
            var oldInvoice = _repo.GetByUId(invoice.UID);
            //var oldInvoice = _repo.Invoices.FirstOrDefault(i => i.UID == invoice.UID);
            if (oldInvoice != null)
            {
                _repo.Delete(oldInvoice);
                _repo.Save();

                //_db.Invoices.Remove(invoice);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetPatient();
            return View(invoice);
        }

        private string UploadFiles(IFormFile file, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString()
                              + Path.GetExtension(file.FileName);


            string folderPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      "Files",
      "Invoices"
  );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Invoices/" + fileName;
        }




        public IActionResult ManageFiles(string uid)
        {
            var invoice = _repo.Invoices.FirstOrDefault(e => e.UID == uid);

            if (invoice == null)
                return NotFound();

            var files = _repo.InvoiceFiles.Where(e => e.InvoiceId == invoice.Id).ToList();
            ViewBag.InvoiceStatus = invoice.Status;
            ViewBag.InvoiceUid = invoice.UID;
            ViewBag.Files = files;

            InvoiceFile invoiceFile = new InvoiceFile();

            invoiceFile.InvoiceId = invoice.Id;

            return View(invoiceFile);
        }


        [HttpPost]
        public IActionResult ManageFiles(InvoiceFile invoiceFile, IFormFile fileInvoice)
        {
            if (fileInvoice != null)
            {
                invoiceFile.FileURL = UploadFiles(fileInvoice, invoiceFile.Status);
            }

            _repo.AddFile(invoiceFile);
            _repo.Save();

            //_db.InvoiceFiles.Add(invoiceFile);
            //_db.SaveChanges();

            var invoice = _repo.Invoices.FirstOrDefault(p => p.Id == invoiceFile.InvoiceId);
            return RedirectToAction(nameof(ManageFiles), new { uid = invoice?.UID });

            //return RedirectToAction(nameof(ManageFiles), new { invoiceId = invoiceFile.InvoiceId });

        }

        public IActionResult DeleteFile(int id, string uid)
        {
            var file = _repo.InvoiceFiles.FirstOrDefault(f => f.Id == id);
            if (file != null)
            {
                _repo.DeleteInvoiceFile(file);
                _repo.Save();
            }

            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
