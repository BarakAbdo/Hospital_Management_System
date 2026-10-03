using Hospital_System.Data;
using Hospital_System.Dtos.InvoicesDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.AppointmentRepo;
using Hospital_System.Repositories.Base;
using Hospital_System.Repositories.InvoiceRepo;
using Hospital_System.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var invoices = _invoiceService.GetAllInvoices();
            return View(invoices);
        }


        public void GetPatient() 
        {
            var patients = _invoiceService.GetAllPatients();
            SelectList patientSelectList = new SelectList(patients, "Id", "Name");
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
                _invoiceService.AddInvoice(invoice);
                return RedirectToAction("Index");
            }

            GetPatient();
            return View(invoice);
        }


        [HttpGet]
        public IActionResult Edit(string uid)
        {
            GetPatient();
            var invoice = _invoiceService.GetInvoiceByUId(uid);
            if (invoice == null)
            {
                return NotFound();
            }
            var update = new UpdateInvoiceDto
            {
                Id = invoice.Id,
                UID = invoice.UID,
                Amount = invoice.Amount,
                Date = invoice.Date,
                Status = invoice.Status,
                PatientId = invoice.PatientId
            };
            return View(update);
        }


        [HttpPost]
        public IActionResult Edit(UpdateInvoiceDto invoice)
        {
            if (ModelState.IsValid)
            {
                _invoiceService.UpdateInvoice(invoice);
                return RedirectToAction("Index");
            }

            GetPatient();
            return View(invoice);
        }
          
        
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            GetPatient();
            var invoice = _invoiceService.GetInvoiceByUId(uid);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }


        [HttpPost]
        public IActionResult Delete(Invoice invoice)
        {
            var oldInvoice = _invoiceService.GetInvoiceByUId(invoice.UID);
            if (oldInvoice != null)
            {
                _invoiceService.DeleteInvoice(oldInvoice);
                return RedirectToAction("Index");
            }
            GetPatient();
            return View(invoice);
        }


        public IActionResult ManageFiles(string uid)
        {
            var invoice = _invoiceService.GetInvoiceByUId(uid);
            if (invoice == null)
            {
                return NotFound();
            }

            var files = _invoiceService.GetInvoiceFiles(invoice.Id);
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
            _invoiceService.AddInvoiceFile(invoiceFile, fileInvoice);

            var invoice = _invoiceService.GetInvoiceById(invoiceFile.InvoiceId);
            return RedirectToAction(nameof(ManageFiles), new { uid = invoice?.UID });
        }

        public IActionResult DeleteFile(int id, string uid)
        {
            _invoiceService.DeleteInvoiceFile(id);
            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }
    }
}
