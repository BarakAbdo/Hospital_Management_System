using Hospital_Management_System.Application.Dtos.MedicationsDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IMedicationService
    {
        IEnumerable<MedicationDto> GetAllMedications();
        MedicationDto GetMedicationById(int id);
        MedicationDto GetMedicationByUId(string uid);

        void AddMedication(CreateMedicationDto medicationDto);
        void UpdateMedication(UpdateMedicationDto medicationDto);
        void DeleteMedication(MedicationDto medicationDto);

        IEnumerable<MedicationFile> GetMedicationFiles(int medicationId);
        void AddMedicationFile(MedicationFile medicationFile, IFormFile fileMedication);
        void DeleteMedicationFile(int fileId);
    }
}