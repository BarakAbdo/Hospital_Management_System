using Hospital_Management_System.Application.Dtos.AppointmentsDtos;
using Hospital_Management_System.Application.Dtos.DoctorsDtos;
using Hospital_Management_System.Application.Dtos.PatientsDtos;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
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

        public IEnumerable<DoctorDto> GetAllDoctors()
        {
            var doctors = _unitOfWork.AppointmentRepo.Doctors.ToList();
            List<DoctorDto> doctorDtos = new List<DoctorDto>();

            foreach (var d in doctors)
            {
                doctorDtos.Add(new DoctorDto
                {
                    Id = d.Id,
                    UID = d.UID,
                    Name = d.Name,
                    Specialization = d.Specialization,
                    Phone = d.Phone,
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.Department?.Name
                });
            }

            return doctorDtos;
        }
        public IEnumerable<PatientDto> GetAllPatients()
        {
            var patients = _unitOfWork.AppointmentRepo.Patients.ToList();
            List<PatientDto> patientDtos = new List<PatientDto>();

            foreach (var p in patients)
            {
                patientDtos.Add(new PatientDto
                {
                    Id = p.Id,
                    UID = p.UID,
                    Name = p.Name,
                    Gender = p.Gender,
                    Phone = p.Phone,
                    DateOfBirth = p.DateOfBirth
                });
            }

            return patientDtos;
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

        public void DeleteAppointment(AppointmentDto appointmentDto)
        {
            var oldAppointment = _unitOfWork.AppointmentRepo.GetByUId(appointmentDto.UID);
            if (oldAppointment != null)
            {
                _unitOfWork.AppointmentRepo.Delete(oldAppointment);
                _unitOfWork.Save();
            }
        }

        public AppointmentDto GetByUId(string uid)
        {
            var a = _unitOfWork.AppointmentRepo.GetByUId(uid);

            var appointmentDto = new AppointmentDto
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
            };

            return appointmentDto;
        }

        public AppointmentDto GetById(int id)
        {
            var a = _unitOfWork.AppointmentRepo.GetById(id);

            var appointmentDto = new AppointmentDto
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
            };

            return appointmentDto;
        }

        public IEnumerable<AppointmentFile> GetAppointmentFiles(int appointmentId)
        {
            return _unitOfWork.AppointmentRepo.AppointmentFiles
                .Where(e => e.AppointmentId == appointmentId)
                .ToList();
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

        public void AddAppointmentFile(AppointmentFile appointmentFile, IFormFile file)
        {
            if (file != null)
            {
                appointmentFile.FileURL = UploadFiles(file, appointmentFile.Status);
            }
            _unitOfWork.AppointmentRepo.AddFile(appointmentFile);
            _unitOfWork.Save();
        }
    }
}