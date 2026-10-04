using Hospital_Management_System.Application.Dtos.MedicalRecordsDtos;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicalRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MedicalRecordDto> GetAllMedicalRecords()
        {
            var medicalRecords = _unitOfWork.MedicalRecordRepo.GetAllMedr();

            var medicalRecordDtos = medicalRecords.Select(m => new MedicalRecordDto
            {
                Id = m.Id,
                UID = m.UID,
                Diagnosis = m.Diagnosis,
                Notes = m.Notes,
                PatientId = m.PatientId,
                DoctorId = m.DoctorId,
                PatientName = m.Patient?.Name,
                DoctorName = m.Doctor?.Name
            }).ToList();

            return medicalRecordDtos;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _unitOfWork.MedicalRecordRepo.Patients.ToList();
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _unitOfWork.MedicalRecordRepo.Doctors.ToList();
        }

        public MedicalRecord GetMedicalRecordById(int id)
        {
            return _unitOfWork.MedicalRecordRepo.GetById(id);
        }

        public MedicalRecord GetMedicalRecordByUId(string uid)
        {
            return _unitOfWork.MedicalRecordRepo.GetByUId(uid);
        }

        public void AddMedicalRecord(CreateMedicalRecordDto medicalRecordDto)
        {
            var medicalRecord = new MedicalRecord
            {
                UID = Guid.NewGuid().ToString(),
                Diagnosis = medicalRecordDto.Diagnosis,
                Notes = medicalRecordDto.Notes,
                PatientId = medicalRecordDto.PatientId,
                DoctorId = medicalRecordDto.DoctorId
            };

            _unitOfWork.MedicalRecordRepo.Add(medicalRecord);
            _unitOfWork.Save();
        }

        public void UpdateMedicalRecord(UpdateMedicalRecordDto medicalRecordDto)
        {
            if (medicalRecordDto.UID == null)
            {
                medicalRecordDto.UID = Guid.NewGuid().ToString();
            }

            var medicalRecord = new MedicalRecord
            {
                Id = medicalRecordDto.Id,
                UID = medicalRecordDto.UID,
                Diagnosis = medicalRecordDto.Diagnosis,
                Notes = medicalRecordDto.Notes,
                PatientId = medicalRecordDto.PatientId,
                DoctorId = medicalRecordDto.DoctorId
            };

            _unitOfWork.MedicalRecordRepo.Update(medicalRecord);
            _unitOfWork.Save();
        }

        public void DeleteMedicalRecord(MedicalRecord medicalRecord)
        {
            var oldMedicalRecord = _unitOfWork.MedicalRecordRepo.GetByUId(medicalRecord.UID);
            if (oldMedicalRecord != null)
            {
                _unitOfWork.MedicalRecordRepo.Delete(oldMedicalRecord);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<MedicalRecordFile> GetMedicalRecordFiles(int medicalRecordId)
        {
            return _unitOfWork.MedicalRecordRepo.MedicalRecordFiles.Where(e => e.MedicalRecordId == medicalRecordId).ToList();
        }

        public void AddMedicalRecordFile(MedicalRecordFile medicalRecordFile, IFormFile fileMedicalRecord)
        {
            if (fileMedicalRecord != null)
            {
                medicalRecordFile.FileURL = UploadFiles(fileMedicalRecord, medicalRecordFile.Notes);
            }

            _unitOfWork.MedicalRecordRepo.AddFile(medicalRecordFile);
            _unitOfWork.Save();
        }

        public void DeleteMedicalRecordFile(int fileId)
        {
            var file = _unitOfWork.MedicalRecordRepo.MedicalRecordFiles.FirstOrDefault(f => f.Id == fileId);
            if (file != null)
            {
                _unitOfWork.MedicalRecordRepo.DeleteMedicalRecordFile(file);
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
                "MedicalRecords"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/MedicalRecords/" + fileName;
        }
    }
}
