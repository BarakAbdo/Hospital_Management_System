using Hospital_Management_System.Application.Dtos.DepartmentDtos;
using Hospital_Management_System.Application.Dtos.DoctorsDtos;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DoctorDto> GetAllDoctors()
        {
            var doctors = _unitOfWork.DoctorRepo.GetAllDoc();

            var doctorDtos = doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                UID = d.UID,
                Name = d.Name,
                Specialization = d.Specialization,
                Phone = d.Phone,
                DepartmentId = d.DepartmentId,
                DepartmentName = d.Department?.Name
            }).ToList();

            return doctorDtos;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DoctorRepo.Departments.ToList();

            var departmentDtos = departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Location = d.Location
            }).ToList();

            return departmentDtos;
        }

        public DoctorDto GetDoctorById(int id)
        {
            var d = _unitOfWork.DoctorRepo.GetById(id);
            if (d == null) return null;

            var doctorDto = new DoctorDto
            {
                Id = d.Id,
                UID = d.UID,
                Name = d.Name,
                Specialization = d.Specialization,
                Phone = d.Phone,
                DepartmentId = d.DepartmentId
            };
            return doctorDto;
        }

        public DoctorDto GetDoctorByUId(string uid)
        {
            var doctor = _unitOfWork.DoctorRepo.GetByUId(uid);
            var update = new DoctorDto
            {
                Id = doctor.Id,
                UID = doctor.UID,
                Name = doctor.Name,
                Specialization = doctor.Specialization,
                Phone = doctor.Phone,
                DepartmentId = doctor.DepartmentId,
                DepartmentName = doctor.Department?.Name 
            };
            return update;
        }

        public void AddDoctor(CreateDoctorDto doctorDto)
        {
            var doctor = new Doctor
            {
                UID = Guid.NewGuid().ToString(),
                Name = doctorDto.Name,
                Specialization = doctorDto.Specialization,
                Phone = doctorDto.Phone,
                DepartmentId = doctorDto.DepartmentId
            };

            _unitOfWork.DoctorRepo.Add(doctor);
            _unitOfWork.Save();
        }

        public void UpdateDoctor(UpdateDoctorDto doctorDto)
        {
            if (doctorDto.UID == null)
            {
                doctorDto.UID = Guid.NewGuid().ToString();
            }

            var doctor = new Doctor
            {
                Id = doctorDto.Id,
                UID = doctorDto.UID,
                Name = doctorDto.Name,
                Specialization = doctorDto.Specialization,
                Phone = doctorDto.Phone,
                DepartmentId = doctorDto.DepartmentId,
            };

            _unitOfWork.DoctorRepo.Update(doctor);
            _unitOfWork.Save();
        }

        public void DeleteDoctor(DoctorDto doctorDto)
        {
            var oldDoctor = _unitOfWork.DoctorRepo.GetByUId(doctorDto.UID);
            if (oldDoctor != null)
            {
                _unitOfWork.DoctorRepo.Delete(oldDoctor);
                _unitOfWork.Save();
            }
        }

       

        public IEnumerable<DoctorFile> GetDoctorFiles(int doctorId)
        {
            return _unitOfWork.DoctorRepo.DoctorFiles.Where(e => e.DoctorId == doctorId).ToList();
        }

        public void AddDoctorFile(DoctorFile doctorFile, IFormFile fileDoctor)
        {
            if (fileDoctor != null)
            {
                doctorFile.FileURL = UploadFiles(fileDoctor, doctorFile.Name);
            }

            _unitOfWork.DoctorRepo.AddFile(doctorFile);
            _unitOfWork.Save();
        }

        public void DeleteDoctorFile(int fileId)
        {
            var file = _unitOfWork.DoctorRepo.DoctorFiles.FirstOrDefault(f => f.Id == fileId);
            if (file != null)
            {
                _unitOfWork.DoctorRepo.DeleteDoctorFile(file);
                _unitOfWork.Save();
            }
        }

        private string UploadFiles(IFormFile file, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Files",
                "Doctors"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Doctors/" + fileName;
        }
    }
}