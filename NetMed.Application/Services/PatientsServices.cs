
using System.Numerics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Base;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Doctors;
using NetMed.Application.Dtos.PatientsDto;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Application.Services
{
    public class PatientsServices : IPatientsServices
    {
        private readonly ILogger<PatientsServices> _logger;
        private readonly IPatientsRepository _patientsRepository;

        public PatientsServices(IPatientsRepository patientsRepository,
            ILogger<PatientsServices> logger)
        {
            this._patientsRepository = patientsRepository;
            this._logger = logger;

        }

        public async Task<OperationResult> Add(AddPatientDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var patient = new Patients
                {
                    UserId = dto.UserId,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    EmergencyContactPhone = dto.EmergencyContactPhone ?? string.Empty, 
                    EmergencyContactName = dto.EmergencyContactName,
                    BloodType = dto.BloodType,
                    Allergies = dto.Allergies,
                    InsuranceProviderID = dto.InsuranceProviderID,
                };
                result = await _patientsRepository.SaveEntityAsync(patient);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error guardando los datos.";
            }
            return result;
        }

        public async Task<OperationResult> Delete(DeletePatientDto dto)
        {
            OperationResult result = new OperationResult();

            try
            {
                var patient = new Patients
                {
                    UserId = dto.UserId,
                   EmergencyContactPhone = string.Empty
                };
                result = await _patientsRepository.DeleteEntityAsync(patient);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error eliminando los datos.";
            }
            return result;
        }

        public async Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = await _patientsRepository.GetEntityByIdAsync(id);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }



        public async Task<OperationResult> GetAllData()
        {
            OperationResult result = new OperationResult();
            try
            {
                result = await _patientsRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

      
        public async Task<OperationResult> Update(UpdatePatientDto dto)
        {
            OperationResult result = new OperationResult();

            try
            {
                var patient = new Patients
                {
                    UserId = dto.UserId,
                    EmergencyContactPhone = dto.EmergencyContactPhone ?? string.Empty
                };
                result = await _patientsRepository.UpdateEntityAsync(patient);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error actualizando los datos.";
            }
            return result;
        }

    }


}

