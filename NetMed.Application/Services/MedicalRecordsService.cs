using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Dtos.MedicalRecords;
using NetMed.Application.Dtos.Specialties;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Application.Services
{
    public class MedicalRecordsService : IMedicalRecordsService
    {
        private readonly IMedicalRecordsRepository _medicalRecordsRepository;
        private readonly ILogger<MedicalRecordsService> _logger;
        private readonly IConfiguration _configuration;

        public MedicalRecordsService(IMedicalRecordsRepository medicalRecordsRepository,
                                    ILogger<MedicalRecordsService> logger,
                                    IConfiguration configuration) 
        {
            _medicalRecordsRepository = medicalRecordsRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var records = await _medicalRecordsRepository.GetAll();

                if (!records.Success)
                {
                    result.Success = false;
                    result.Message = records.Message;
                    return result;
                }
                result.Data = records.Data;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obteniendo los Records Medicos";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GetById(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var records = await _medicalRecordsRepository.GetEntityByIdAsync(Id);
                if (!records.Success)
                {
                    result.Success = false;
                    result.Message = records.Message;
                    return result;
                }
                result.Data = records.Data;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obteniendo el Record Medico";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public Task<OperationResult> Remove(RemoveMedicalRecordsDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Save(SaveMedicalRecordsDto dto)
        {
            OperationResult result = new OperationResult();

            try
            {
                MedicalRecords record = new MedicalRecords()
                {
                    PatientID = dto.PatientId,
                    Treatment = dto.Treatment,
                    DateOfVisit = dto.DateOfVisit,
                    Diagnosis = dto.Diagnosis,
                    DoctorID = dto.DoctorID


                };

                var m = await _medicalRecordsRepository.SaveEntityAsync(record);

                result.Success = m.Success;
                result.Message = m.Message;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error guardando el Record Medico";
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async Task<OperationResult> Update(UpdateMedicalRecordsDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var resultGetById = await _medicalRecordsRepository.GetEntityByIdAsync(dto.RecordID);

                if (!resultGetById.Success)
                {
                    result.Success = resultGetById.Success;
                    result.Message = resultGetById.Message;
                    return result;
                }

                MedicalRecords? record = new MedicalRecords()
                {
                    Id = dto.RecordID,
                    Treatment = dto.Treatment,
                    Diagnosis = dto.Diagnosis,
                    DateOfVisit = dto.DateOfVisit,
                    PatientID = dto.PatientID

                };
                var s = await _medicalRecordsRepository.UpdateEntityAsync(record);
                result.Success = s.Success;
                result.Message = s.Message;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error actualizando el Record Medico";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
