
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;

namespace NetMed.Persistence.Repositories
{
    public class PatientsRepository : BaseRepository<Patients>, IPatientsRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<PatientsRepository> _logger;
        private readonly IConfiguration _configuration;
        public PatientsRepository(NetMedContext context,
            ILogger<PatientsRepository> logger,
            IConfiguration configuration) 
            : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetByBloodTypeAsync(string bloodType)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Patients.FindAsync(bloodType);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return result;
        }

        public async Task<OperationResult> GetByInsuranceProviderAsync(int providerId)
        {
            OperationResult result = new OperationResult();
            
            try
               

            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Patients.FindAsync(providerId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return  result;
        }
            public async Task<OperationResult> GetPatientsAsyncWithoutInsuranceAsync()
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Patients.FindAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return result;
        }

        public async Task<OperationResult> SearchByAddressAsync(string addressFragment)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Patients.FindAsync(addressFragment);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return result;
        }

        public async Task<OperationResult> GetPatientsByAgeRangeAsync(int minAge, int maxAge)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Patients.FindAsync(minAge, maxAge);
             
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }

            return result;
        }

        public async Task<OperationResult> GetByEmergencyContactAsync(string contactInfo)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Patients.FindAsync(contactInfo);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return result;
        }
        public async Task<OperationResult> GetPatientsWithAllergiesAsync(string? allergy = null)
        {
            OperationResult result = new OperationResult();
            try
            {
              if(result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }   

                result.data = await _context.Patients.FindAsync(allergy);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return result;
        }

        public async Task<OperationResult> GetPatientsByGenderAsync(string gender)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Patients.FindAsync(gender);
             
                result.Success = true;
            }
            catch
        (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return result;
            }
            public override async Task<OperationResult> SaveEntityAsync(Patients entity)
        {
            //Agregar las validaciones necesarias
            OperationResult result = new OperationResult();
            try
            {
                _context.Patients.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + " Ocurrio un error guardando los datos.";
                result.Success = false;
            }
            return await Task.FromResult(result);
        }


        public override async Task<OperationResult> UpdateEntityAsync(Patients entity)
        {

            OperationResult result = new OperationResult();
            try
            {
                _context.Patients.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + " Ocurrio un error actualizando los datos.";
                result.Success = false;
            }
            return await Task.FromResult(result);
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<Patients, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Patients.Where(filter).ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;

        }
        public override async Task<OperationResult> GetEntityByIdAsync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.data = await _context.Doctors.FindAsync(id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;

        }

        public override async Task<bool> ExistsAsync(Expression<Func<Patients, bool>> filter)
        {
            return await _context.Patients.AnyAsync(filter);
        }

        public override async Task<OperationResult> GetAllAsync()
        {
         OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Patients.ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
                _logger.LogError(ex.Message);
            }
            return result;
        }

    }




}

