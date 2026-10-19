using Hospital_Management_System.Application.Dtos.PrescriptionsDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
{
    public interface IPrescriptionService
    {
        IEnumerable<PrescriptionDto> GetAllPrescriptions();
        PrescriptionDto GetPrescriptionById(int id);
        PrescriptionDto GetPrescriptionByUId(string uid);

        IEnumerable<Patient> GetAllPatients();
        IEnumerable<Doctor> GetAllDoctors();
        IEnumerable<Medication> GetAllMedications();

        void AddPrescription(CreatePrescriptionDto prescriptionDto);
        void UpdatePrescription(UpdatePrescriptionDto prescriptionDto);
        void DeletePrescription(PrescriptionDto prescriptionDto);

        IEnumerable<PrescriptionFile> GetPrescriptionFiles(int prescriptionId);
        void AddPrescriptionFile(PrescriptionFile prescriptionFile, IFormFile filePrescription);
        void DeletePrescriptionFile(int fileId);
    }
}