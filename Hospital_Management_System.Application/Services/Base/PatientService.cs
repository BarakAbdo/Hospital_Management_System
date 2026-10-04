using Hospital_Management_System.Application.Dtos.PatientsDtos;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PatientDto> GetAllPatients()
        {
            var patients = _unitOfWork.PatientRepo.GetAll();

            var patientDtos = patients.Select(p => new PatientDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name,
                Gender = p.Gender,
                Phone = p.Phone,
                DateOfBirth = p.DateOfBirth
            }).ToList();

            return patientDtos;
        }

        public Patient GetPatientById(int id)
        {
            return _unitOfWork.PatientRepo.GetById(id);
        }

        public Patient GetPatientByUId(string uid)
        {
            return _unitOfWork.PatientRepo.GetByUId(uid);
        }

        public void AddPatient(CreatePatientDto patientDto)
        {
            var patient = new Patient
            {
                UID = Guid.NewGuid().ToString(),
                Name = patientDto.Name,
                Gender = patientDto.Gender,
                Phone = patientDto.Phone,
                DateOfBirth = patientDto.DateOfBirth
            };

            _unitOfWork.PatientRepo.Add(patient);
            _unitOfWork.Save();
        }

        public void UpdatePatient(UpdatePatientDto patientDto)
        {
            if (patientDto.UID == null)
            {
                patientDto.UID = Guid.NewGuid().ToString();
            }

            var patient = new Patient
            {
                Id = patientDto.Id,
                UID = patientDto.UID,
                Name = patientDto.Name,
                Gender = patientDto.Gender,
                Phone = patientDto.Phone,
                DateOfBirth = patientDto.DateOfBirth
            };

            _unitOfWork.PatientRepo.Update(patient);
            _unitOfWork.Save();
        }

        public void DeletePatient(Patient patient)
        {
            var oldPatient = _unitOfWork.PatientRepo.GetByUId(patient.UID);
            if (oldPatient != null)
            {
                _unitOfWork.PatientRepo.Delete(oldPatient);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<PatientFile> GetPatientFiles(int patientId)
        {
            return _unitOfWork.PatientRepo.PatientFiles.Where(e => e.PatientId == patientId).ToList();
        }

        public void AddPatientFile(PatientFile patientFile, IFormFile filePatient)
        {
            if (filePatient != null)
            {
                patientFile.FileURL = UploadFiles(filePatient, patientFile.Name);
            }

            _unitOfWork.PatientRepo.AddFile(patientFile);
            _unitOfWork.Save();
        }

        public void DeletePatientFile(int fileId)
        {
            var file = _unitOfWork.PatientRepo.PatientFiles.FirstOrDefault(f => f.Id == fileId);
            if (file != null)
            {
                _unitOfWork.PatientRepo.DeletePatientFile(file);
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
                "Patients"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Patients/" + fileName;
        }

    }
}
