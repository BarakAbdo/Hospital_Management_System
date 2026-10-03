using Hospital_System.Dtos.PrescriptionsDtos;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital_System.Services.Base
{
  
        public interface IPrescriptionService
        {
            IEnumerable<PrescriptionDto> GetAllPrescriptions();
            Prescription GetPrescriptionById(int id);
            Prescription GetPrescriptionByUId(string uid);

            void AddPrescription(CreatePrescriptionDto prescriptionDto);
            void UpdatePrescription(UpdatePrescriptionDto prescriptionDto);
            void DeletePrescription(string uid);

            
            IEnumerable<Patient> GetAllPatients();
            IEnumerable<Doctor> GetAllDoctors();
            IEnumerable<Medication> GetAllMedications();


            IEnumerable<PrescriptionFile> GetPrescriptionFiles(int prescriptionId);
            void AddPrescriptionFile(PrescriptionFile prescriptionFile, IFormFile filePrescription);
            void DeletePrescriptionFile(int fileId);
        }
    }

