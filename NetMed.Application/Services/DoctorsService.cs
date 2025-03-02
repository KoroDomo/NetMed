
using System.Numerics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Base;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Doctors;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Application.Services
{
    public class DoctorsService : IDoctorsServices
    {
        private readonly IDoctorsRepository _doctorsRepository;
        public DoctorsService(IDoctorsRepository doctorsRepository,
            ILogger<DoctorsService> logger,
            IConfiguration configuration)
        {

            this._doctorsRepository = doctorsRepository;
        }

        public async Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var doctor = await _doctorsRepository.GetEntityByIdAsync(id);
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

                result = await _doctorsRepository.GetAllAsync();

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }
       
     public async Task<OperationResult> Add(AddDoctorsDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var doctor = new Doctors
                {
                    UserId = dto.UserId,
                    SpecialtyID = dto.SpecialtyID,
                    LicenseNumber = dto.LicenseNumber,
                    PhoneNumber = dto.PhoneNumber,
                    YearsOfExperience = dto.YearsOfExperience,
                    Education = dto.Education,
                    Bio = dto.Bio,
                    ConsultationFee = dto.ConsultationFee,
                    ClinicAddress = dto.ClinicAddress,
                    LicenseExpirationDate = dto.LicenseExpirationDate
                };

                var doc = await _doctorsRepository.SaveEntityAsync(doctor);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error guardando los datos.";
            }
            return result;
        }


        public async Task<OperationResult> Update(UpdateDoctorsDto dto)
        {
            OperationResult result = new OperationResult();

            try
            {
                var doctor = new Doctors
                {
                    UserId = dto.UserId,
                    SpecialtyID = dto.SpecialtyID,
                    LicenseNumber = dto.LicenseNumber,
                    PhoneNumber = dto.PhoneNumber,
                    YearsOfExperience = dto.YearsOfExperience,
                    Education = dto.Education,
                    Bio = dto.Bio,
                    ConsultationFee = dto.ConsultationFee,
                    ClinicAddress = dto.ClinicAddress,
                    LicenseExpirationDate = dto.LicenseExpirationDate
                };
                var doc = await _doctorsRepository.UpdateEntityAsync(doctor);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error actualizando los datos.";
            }
            return result;
        }

        public async Task<OperationResult> Delete(DeleteDoctorDto dto)
        {
            OperationResult result = new OperationResult();

            try
            {
                var doctor = new Doctors
                {
                    UserId = dto.UserId,
                    SpecialtyID = dto.SpecialtyID,
                    LicenseNumber = dto.LicenseNumber,
                    PhoneNumber = dto.PhoneNumber,
                    YearsOfExperience = dto.YearsOfExperience,
                    Education = dto.Education,
                    Bio = dto.Bio,
                    ConsultationFee = dto.ConsultationFee,
                    ClinicAddress = dto.ClinicAddress,
                    LicenseExpirationDate = dto.LicenseExpirationDate
                };
                var doc = await _doctorsRepository.DeleteEntityAsync(doctor);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error eliminando los datos.";
            }
            return result;
        }
    }
}
