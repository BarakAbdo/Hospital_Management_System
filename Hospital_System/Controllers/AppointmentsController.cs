using Hospital_System.Data;
using Hospital_System.Dtos.AppointmentsDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.AppointmentRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Controllers
{
    public class AppointmentsController : Controller
    {

        private readonly IAppointmentRepository _repo;
        public AppointmentsController(IAppointmentRepository repo)
        {
            _repo = repo;

        }


        //private readonly AppDbContext _db;
        //public AppointmentsController(AppDbContext db)
        //{
        //    _db = db;

        //}
        public IActionResult Index()
        {
            //IEnumerable<Appointment> appointments = _repo.GetAll();
            var appointment = _repo.GetAll().Select(a => new AppointmentDto
            {
                Id = a.Id,
                UID = a.UID,
                Date = a.Date,
                Time = a.Time,
                Status = a.Status,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                PatientName = a.Patient.Name,
                DoctorName = a.Doctor.Name
            }).ToList();

            return View(appointment);
        }
        public void GetDoctor()
        {
            IEnumerable<Doctor> doctors = _repo.Doctors.ToList();
            SelectList doctorSelectList = new SelectList(doctors, "Id", "Name");
            ViewBag.DoctorSelectList = doctorSelectList;
        }

        public void GetPatient()
        {
            IEnumerable<Patient> patients = _repo.Patients.ToList();
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
        public IActionResult Create(CreateAppointmentDto appointment)
        {
            if (ModelState.IsValid)
            {

                //Mapping
                var app = new Appointment
                {
                    UID = Guid.NewGuid().ToString(),
                    Date = appointment.Date,
                    Time = appointment.Time,
                    Status = appointment.Status,
                    PatientId = appointment.PatientId,
                    DoctorId = appointment.DoctorId,

                };
                _repo.Add(app);
                _repo.Save();

                //_db.Appointments.Add(app);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetDoctor();
            GetPatient();
            return View(appointment);
        }

        [HttpGet]
        public IActionResult Edit(string uid)
        {
            GetDoctor();
            GetPatient();

            var appointment = _repo.GetByUId(uid);

            //var appointment = _repo.Appointments.FirstOrDefault(a=>a.UID == uid);
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
        public IActionResult Edit(UpdateAppointmentDto appointment)
        {
            if (ModelState.IsValid)
            {
                if (appointment.UID == null)
                    appointment.UID = Guid.NewGuid().ToString();
                var app = new Appointment
                {
                    UID = appointment.UID,
                    Date = appointment.Date,
                    Time = appointment.Time,
                    Status = appointment.Status,
                    PatientId = appointment.PatientId,
                    DoctorId = appointment.DoctorId,
                    Id = appointment.Id,


                };

                _repo.Update(app);
                _repo.Save();

                //_repo.Appointments.Update(app);
                //_repo.SaveChanges();
                return RedirectToAction("Index");
            }
            GetDoctor();
            GetPatient();
            return View(appointment);
        }

        [HttpGet]
        public IActionResult Delete(string uid)
        {
            GetDoctor();
            GetPatient();
            var appointment = _repo.GetByUId(uid);
            //var appointment = _repo.Appointments.FirstOrDefault(a=>a.UID == uid);
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
            var oldAppointment = _repo.GetByUId(appointment.UID);
            //var oldAppointment = _repo.Appointments.FirstOrDefault(a => a.UID == appointment.UID);

            if (oldAppointment == null)
            {
                return NotFound();
            }

            _repo.Delete(oldAppointment);
            _repo.Save();

            //_repo.Appointments.Remove(oldAppointment);
            //_repo.SaveChanges();

            return RedirectToAction("Index");

        }

        private string UploadFiles(IFormFile file, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString()
                              + Path.GetExtension(file.FileName);


            string folderPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      "Files",
      "Appointments"
  );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Appointments/" + fileName;
        }




        public IActionResult ManageFiles(string uid)
        {
            var appointment = _repo.GetByUId(uid);
            //var appointment = _repo.Appointments.FirstOrDefault(e => e.Id == appointmentId);

            if (appointment == null)
                return NotFound();

            var files = _repo.AppointmentFiles.Where(e => e.AppointmentId == appointment.Id).ToList();
            ViewBag.AppointmentName = appointment.Status;

            ViewBag.Files = files;

            AppointmentFile appointmentFile = new AppointmentFile();

            appointmentFile.AppointmentId = appointment.Id;

            return View(appointmentFile);
        }


        [HttpPost]
        public IActionResult ManageFiles(AppointmentFile appointmentFile, IFormFile fileAppointment)
        {
            if (fileAppointment != null)
            {
                appointmentFile.FileURL = UploadFiles(fileAppointment, appointmentFile.Status);
            }

            _repo.AddFile(appointmentFile);
            _repo.Save();


            var appointment = _repo.GetAll().FirstOrDefault(a => a.Id == appointmentFile.AppointmentId);
            return RedirectToAction(nameof(ManageFiles), new { uid = appointment?.UID });

        }

        public IActionResult DeleteFile(int id, string uid)
        {
            var file = _repo.AppointmentFiles.FirstOrDefault(f => f.Id == id);
            if (file != null)
            {
                _repo.DeleteAppointmentFile(file);
                _repo.Save();
            }

            return RedirectToAction(nameof(ManageFiles), new { uid = uid });
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
