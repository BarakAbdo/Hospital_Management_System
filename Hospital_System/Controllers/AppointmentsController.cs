using Hospital_System.Dtos.AppointmentsDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;
using Hospital_System.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace Hospital_System.Controllers
{
    public class AppointmentsController : Controller
    {

        //private readonly IUnitOfWork _unitOfWork;
        //public AppointmentsController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public IActionResult Index()
        {
            var appointments = _appointmentService.GetAllAppointments();
            return View(appointments);
        }

        public void GetDoctor()
        {
            IEnumerable<Doctor> doctors = _appointmentService.GetAllDoctors();
            SelectList doctorSelectList = new SelectList(doctors, "Id", "Name");
            ViewBag.DoctorSelectList = doctorSelectList;
        }

        public void GetPatient()
        {
            IEnumerable<Patient> patients = _appointmentService.GetAllPatients();
            SelectList patientSelectList = new SelectList(patients, "Id", "Name");
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
        public IActionResult Create(CreateAppointmentDto appointmentDto)
        {
            if (ModelState.IsValid)
            {
                _appointmentService.AddAppointment(appointmentDto);
                return RedirectToAction("Index");
            }
            GetDoctor();
            GetPatient();
            return View(appointmentDto);
        }


        [HttpGet]
        public IActionResult Edit(string uid)
        {
            GetDoctor();
            GetPatient();
            var appointment = _appointmentService.GetByUId(uid);
            if (appointment == null)
            {
                return NotFound();
            }

            //Mapping
            var update = new UpdateAppointmentDto
            {
                Id = appointment.Id,
                UID = appointment.UID,
                Date = appointment.Date,
                Time = appointment.Time,
                Status = appointment.Status,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,

            };

            return View(update);
        }


        [HttpPost]
        public IActionResult Edit(UpdateAppointmentDto appointmentDto)
        {
            if (ModelState.IsValid)
            {

                _appointmentService.UpdateAppointment(appointmentDto);
                return RedirectToAction("Index");
            }
            GetDoctor();
            GetPatient();
            return View(appointmentDto);
        }

        [HttpGet]
        public IActionResult Delete(string uid)
        {
            GetDoctor();
            GetPatient();
            var appointment = _appointmentService.GetByUId(uid);
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
            var oldAppointment = _appointmentService.GetByUId(appointment.UID);
            if (oldAppointment != null)
            {
                _appointmentService.DeleteAppointment(oldAppointment);
                return RedirectToAction("Index");
            }
            return View(appointment);
        }


        public IActionResult ManageFiles(string uid)
        {
            var appointment = _appointmentService.GetByUId(uid);
            if (appointment == null)
                return NotFound();

            var files = _appointmentService.GetAppointmentFiles(appointment.Id);
            ViewBag.AppointmentName = appointment.Status;
            ViewBag.Files = files;

            AppointmentFile appointmentFile = new AppointmentFile
            {
                AppointmentId = appointment.Id
            };

            return View(appointmentFile);
        }


        [HttpPost]
        public IActionResult ManageFiles(AppointmentFile appointmentFile, IFormFile fileAppointment)
        {
            _appointmentService.AddAppointmentFile(appointmentFile, fileAppointment);

            var appointment = _appointmentService.GetById(appointmentFile.AppointmentId);
            return RedirectToAction(nameof(ManageFiles), new { uid = appointment?.UID });

        }

        public IActionResult DeleteFile(int id, string uid)
        {
            _appointmentService.DeleteAppointmentFile(id);
            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
        }

    }
}
