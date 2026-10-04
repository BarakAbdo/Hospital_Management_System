using Hospital_Management_System.Application.Dtos.PrescriptionsDtos;
using Hospital_Management_System.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital_Management_System.Application.Services.Base
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

