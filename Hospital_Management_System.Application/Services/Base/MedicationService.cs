using Hospital_Management_System.Application.Dtos.MedicationsDtos;
using Hospital_Management_System.Domain.Models;
using Hospital_Management_System.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public class MedicationService : IMedicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MedicationDto> GetAllMedications()
        {
            var medications = _unitOfWork.MedicationRepo.GetAll();

            var medicationDtos = medications.Select(m => new MedicationDto
            {
                Id = m.Id,
                UID = m.UID,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                Stock = m.Stock
            }).ToList();

            return medicationDtos;
        }

        public Medication GetMedicationById(int id)
        {
            return _unitOfWork.MedicationRepo.GetById(id);
        }

        public Medication GetMedicationByUId(string uid)
        {
            return _unitOfWork.MedicationRepo.GetByUId(uid);
        }

        public void AddMedication(CreateMedicationDto medicationDto)
        {
            var medication = new Medication
            {
                UID = Guid.NewGuid().ToString(),
                Name = medicationDto.Name,
                Description = medicationDto.Description,
                Price = medicationDto.Price,
                Stock = medicationDto.Stock
            };

            _unitOfWork.MedicationRepo.Add(medication);
            _unitOfWork.Save();
        }

        public void UpdateMedication(UpdateMedicationDto medicationDto)
        {
            if (medicationDto.UID == null)
            {
                medicationDto.UID = Guid.NewGuid().ToString();
            }

            var medication = new Medication
            {
                Id = medicationDto.Id,
                UID = medicationDto.UID,
                Name = medicationDto.Name,
                Description = medicationDto.Description,
                Price = medicationDto.Price,
                Stock = medicationDto.Stock
            };

            _unitOfWork.MedicationRepo.Update(medication);
            _unitOfWork.Save();
        }

        public void DeleteMedication(Medication medication)
        {
            var oldMedication = _unitOfWork.MedicationRepo.GetByUId(medication.UID);
            if (oldMedication != null)
            {
                _unitOfWork.MedicationRepo.Delete(oldMedication);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<MedicationFile> GetMedicationFiles(int medicationId)
        {
            return _unitOfWork.MedicationRepo.MedicationFiles.Where(e => e.MedicationId == medicationId).ToList();
        }

        public void AddMedicationFile(MedicationFile medicationFile, IFormFile fileMedication)
        {
            if (fileMedication != null)
            {
                medicationFile.FileURL = UploadFiles(fileMedication, medicationFile.Name);
            }

            _unitOfWork.MedicationRepo.AddFile(medicationFile);
            _unitOfWork.Save();
        }

        public void DeleteMedicationFile(int fileId)
        {
            var file = _unitOfWork.MedicationRepo.MedicationFiles.FirstOrDefault(f => f.Id == fileId);
            if (file != null)
            {
                _unitOfWork.MedicationRepo.DeleteMedicationFile(file);
                _unitOfWork.MedicationRepo.Save(); // أو _unitOfWork.Save() بناءً على ما تفضله
            }
        }

        private string UploadFiles(IFormFile file, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Files",
                "Medications"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Medications/" + fileName;
        }
    }
}
