using Hospital_System.Dtos.AppointmentsDtos;
using Hospital_System.Models;
using Hospital_System.Repositories.Base;

namespace Hospital_System.Services.Base
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AppointmentDto> GetAllAppointments()
        {
            IEnumerable<Appointment> appointments = _unitOfWork.AppointmentRepo.GetAllApp();
            IEnumerable<AppointmentDto> appointmentDtos = appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                UID = a.UID,
                Date = a.Date,
                Time = a.Time,
                Status = a.Status,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                PatientName = a.Patient?.Name,
                DoctorName = a.Doctor?.Name
            }).ToList();

            return appointmentDtos;
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _unitOfWork.AppointmentRepo.Doctors.ToList();
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _unitOfWork.AppointmentRepo.Patients.ToList();
        }

        public void AddAppointment(CreateAppointmentDto appointmentDto)
        {
            var app = new Appointment
            {
                UID = Guid.NewGuid().ToString(),
                Date = appointmentDto.Date,
                Time = appointmentDto.Time,
                Status = appointmentDto.Status,
                PatientId = appointmentDto.PatientId,
                DoctorId = appointmentDto.DoctorId
            };
            _unitOfWork.AppointmentRepo.Add(app);
            _unitOfWork.Save();
        }

        public void UpdateAppointment(UpdateAppointmentDto appointmentDto)
        {
            if (string.IsNullOrEmpty(appointmentDto.UID))
                appointmentDto.UID = Guid.NewGuid().ToString();

            var app = new Appointment
            {
                Id = appointmentDto.Id,
                UID = appointmentDto.UID,
                Date = appointmentDto.Date,
                Time = appointmentDto.Time,
                Status = appointmentDto.Status,
                PatientId = appointmentDto.PatientId,
                DoctorId = appointmentDto.DoctorId
            };

            _unitOfWork.AppointmentRepo.Update(app);
            _unitOfWork.Save();
        }

        public void DeleteAppointment(Appointment appointment)
        {
            var oldAppointment = _unitOfWork.AppointmentRepo.GetByUId(appointment.UID);
            if (oldAppointment != null)
            {
                _unitOfWork.AppointmentRepo.Delete(oldAppointment);
                _unitOfWork.Save();
            }
        }

        public Appointment GetByUId(string uid)
        {
            return _unitOfWork.AppointmentRepo.GetByUId(uid);
        }

        public Appointment GetById(int id)
        {
            return _unitOfWork.AppointmentRepo.GetById(id);
        }

        public IEnumerable<AppointmentFile> GetAppointmentFiles(int appointmentId)
        {
            return _unitOfWork.AppointmentRepo.AppointmentFiles
                .Where(e => e.AppointmentId == appointmentId)
                .ToList();
        }

        public void AddAppointmentFile(AppointmentFile appointmentFile, IFormFile file)
        {
            if (file != null)
            {
                appointmentFile.FileURL = UploadFiles(file, appointmentFile.Status);
            }
            _unitOfWork.AppointmentRepo.AddFile(appointmentFile);
            _unitOfWork.Save();
        }

        public void DeleteAppointmentFile(int fileId)
        {
            var file = _unitOfWork.AppointmentRepo.AppointmentFiles.FirstOrDefault(f => f.Id == fileId);
            if (file != null)
            {
                _unitOfWork.AppointmentRepo.DeleteAppointmentFile(file);
                _unitOfWork.Save();
            }
        }

        private string UploadFiles(IFormFile file, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Appointments");

            Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Appointments/" + fileName;
        }

    }
}
