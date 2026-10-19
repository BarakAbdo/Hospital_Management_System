using Hospital_Management_System.Application.Dtos.PrescriptionsDtos;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PrescriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PrescriptionDto> GetAllPrescriptions()
        {
            var prescriptions = _unitOfWork.PrescriptionRepo.GetAllPre();

            var prescriptionDto = prescriptions.Select(p => new PrescriptionDto
            {
                Id = p.Id,
                UID = p.UID,
                Dosage = p.Dosage,
                Duration = p.Duration,
                PatientId = p.PatientId,
                DoctorId = p.DoctorId,
                MedicationId = p.MedicationId,
                PatientName = p.Patient?.Name,
                DoctorName = p.Doctor?.Name,
                MedicationName = p.Medication?.Name
            }).ToList();
            return prescriptionDto;
        }

        public PrescriptionDto GetPrescriptionById(int id)
        {
            var p = _unitOfWork.PrescriptionRepo.GetById(id);

            var prescriptionDto = new PrescriptionDto
            {
                Id = p.Id,
                UID = p.UID,
                Dosage = p.Dosage,
                Duration = p.Duration,
                PatientId = p.PatientId,
                DoctorId = p.DoctorId,
                MedicationId = p.MedicationId,
                PatientName = p.Patient?.Name,
                DoctorName = p.Doctor?.Name,
                MedicationName = p.Medication?.Name
            };
            return prescriptionDto;
        }

        public PrescriptionDto GetPrescriptionByUId(string uid)
        {
            var p = _unitOfWork.PrescriptionRepo.GetByUId(uid);

            var prescriptionDto = new PrescriptionDto
            {
                Id = p.Id,
                UID = p.UID,
                Dosage = p.Dosage,
                Duration = p.Duration,
                PatientId = p.PatientId,
                DoctorId = p.DoctorId,
                MedicationId = p.MedicationId,
                PatientName = p.Patient?.Name,
                DoctorName = p.Doctor?.Name,
                MedicationName = p.Medication?.Name
            };
            return prescriptionDto;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _unitOfWork.PrescriptionRepo.Patients.ToList();
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _unitOfWork.PrescriptionRepo.Doctors.ToList();
        }

        public IEnumerable<Medication> GetAllMedications()
        {
            return _unitOfWork.PrescriptionRepo.Medications.ToList();
        }

        public void AddPrescription(CreatePrescriptionDto prescriptionDto)
        {
            var pre = new Prescription
            {
                UID = Guid.NewGuid().ToString(),
                Dosage = prescriptionDto.Dosage,
                Duration = prescriptionDto.Duration,
                PatientId = prescriptionDto.PatientId,
                DoctorId = prescriptionDto.DoctorId,
                MedicationId = prescriptionDto.MedicationId
            };

            _unitOfWork.PrescriptionRepo.Add(pre);
            _unitOfWork.Save();
        }

        public void UpdatePrescription(UpdatePrescriptionDto prescriptionDto)
        {
            if (string.IsNullOrEmpty(prescriptionDto.UID))
            {
                prescriptionDto.UID = Guid.NewGuid().ToString();
            }

            var pre = new Prescription
            {
                Id = prescriptionDto.Id,
                UID = prescriptionDto.UID,
                Dosage = prescriptionDto.Dosage,
                Duration = prescriptionDto.Duration,
                PatientId = prescriptionDto.PatientId,
                DoctorId = prescriptionDto.DoctorId,
                MedicationId = prescriptionDto.MedicationId
            };

            _unitOfWork.PrescriptionRepo.Update(pre);
            _unitOfWork.Save();
        }

        public void DeletePrescription(PrescriptionDto prescriptionDto)
        {
            var oldPrescription = _unitOfWork.PrescriptionRepo.GetByUId(prescriptionDto.UID);
            if (oldPrescription != null)
            {
                _unitOfWork.PrescriptionRepo.Delete(oldPrescription);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<PrescriptionFile> GetPrescriptionFiles(int prescriptionId)
        {
            return _unitOfWork.PrescriptionRepo.PrescriptionFiles.Where(e => e.PrescriptionId == prescriptionId).ToList();
        }

        public void AddPrescriptionFile(PrescriptionFile prescriptionFile, IFormFile filePrescription)
        {
            if (filePrescription != null)
            {
                prescriptionFile.FileURL = UploadFiles(filePrescription, prescriptionFile.Dosage);
            }

            _unitOfWork.PrescriptionRepo.AddFile(prescriptionFile);
            _unitOfWork.Save();
        }

        public void DeletePrescriptionFile(int fileId)
        {
            var file = _unitOfWork.PrescriptionRepo.PrescriptionFiles.FirstOrDefault(f => f.Id == fileId);
            if (file != null)
            {
                _unitOfWork.PrescriptionRepo.DeletePrescriptionFile(file);
                _unitOfWork.Save();
            }
        }

        private string UploadFiles(IFormFile file, string name)
        {
            string fileName = (string.IsNullOrEmpty(name) ? "File" : name) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Files",
                "Prescriptions"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Prescriptions/" + fileName;
        }
    }
}