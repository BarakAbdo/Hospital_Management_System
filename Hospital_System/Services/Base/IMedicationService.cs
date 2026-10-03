using Hospital_System.Dtos.MedicationsDtos;
using Hospital_System.Models;

namespace Hospital_System.Services.Base
{
    public interface IMedicationService
    {
        IEnumerable<MedicationDto> GetAllMedications();
        Medication GetMedicationById(int id);
        Medication GetMedicationByUId(string uid);
        void AddMedication(CreateMedicationDto medicationDto);
        void UpdateMedication(UpdateMedicationDto medicationDto);
        void DeleteMedication(Medication medication);

        // دوال الملفات
        IEnumerable<MedicationFile> GetMedicationFiles(int medicationId);
        void AddMedicationFile(MedicationFile medicationFile, IFormFile fileMedication);
        void DeleteMedicationFile(int fileId);
    }
}
