
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
using NetMed.Infrastructure.Validations.Implementations;
using NetMed.Infrastructure.Validations.Interfaces;


namespace NetMed.Application.Services
{
    public class PatientsServices : PatietsValidation, IPatientsServices
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

                // Perform validations
                var validationResults = new List<OperationResult>
                {
                    ValidatePatientAge(patient),
                    ValidatePatientGender(patient),
                    ValidatePatientEmergencyContact(patient),
                    ValidatePatientBloodType(patient),
                    ValidatePatientAllergies(patient),
                    ValidatePatientPhoneNumber(patient),
                    ValidatePatientAddress(patient),
                    ValidatePatientInsuranceProvider(patient),
                    ValidatePatientWithoutInsurance(patient)
                };

                // Check if any validation failed
                var failedValidation = validationResults.FirstOrDefault(v => !v.Success);
                if (failedValidation != null)
                {
                    return failedValidation;
                }
                result = await _patientsRepository.SaveEntityAsync(patient);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error guardando los datos.";
                _logger.LogError(ex, ex.Message);
            }

            result.Success = true;
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
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }

        public async Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _patientsRepository.GetEntityByIdAsync(id);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }



        public async Task<OperationResult> GetAllData()
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _patientsRepository.GetAllAsync();
            
             if (!result.Success)
            {
                result.data = result.data;
                result.Success = result.Success;
            }
        }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
                _logger.LogError(ex, ex.Message);
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
                result.data = await _patientsRepository.UpdateEntityAsync(patient);
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error actualizando los datos.";
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }

    }


}

