

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
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
        public PatientsRepository(NetMedContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Patients> GetByBloodTypeAsync(string bloodType)
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.Patients.FindAsync(bloodType);
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return await _context.Patients.FindAsync(bloodType) ?? throw new InvalidOperationException("Patient not found");
        }

            public async Task<Patients> GetByInsuranceProviderAsync(int providerId)
        {
            var result = new OperationResult();
            result.Result = await _context.Patients.FindAsync(providerId);
            result.Success = true;
            try
            {
                var data = await _context.Patients.FindAsync(providerId);
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return await _context.Patients.FindAsync(providerId) ?? throw new InvalidOperationException("Patient not found");
        }
            public async Task<Patients> GetPatientsAsyncWithoutInsuranceAsync()
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.Patients.FindAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return await _context.Patients.FindAsync() ?? throw new InvalidOperationException("Patient not found");
        }

        public async Task<Patients> SearchByAddressAsync(string addressFragment)
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.Patients.FindAsync(addressFragment);
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return await _context.Patients.FindAsync(addressFragment) ?? throw new InvalidOperationException("Patient not found");
        }

        public async Task<Patients> GetPatientsByAgeRangeAsync(int minAge, int maxAge)
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.Patients.FindAsync(minAge, maxAge);
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }

            return await _context.Patients.FindAsync(minAge, maxAge) ?? throw new InvalidOperationException("Patient not found");
        }

        public async Task<Patients> GetByEmergencyContactAsync(string contactInfo)
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.Patients.FindAsync(contactInfo);
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return await _context.Patients.FindAsync(contactInfo) ?? throw new InvalidOperationException("Patient not found");
        }

        public async Task<Patients> GetPatientsWithAllergiesAsync(string? allergy = null)
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.Patients.FindAsync(allergy);
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return await _context.Patients.FindAsync(allergy) ?? throw new InvalidOperationException("Patient not found");
        }

        public async Task<Patients> GetPatientsByGenderAsync(string gender)
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.Patients.FindAsync(gender);
                result.Result = data;
                result.Success = true;
            }
            catch
        (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + "Ocurrio un error al buscar los datos";
            }
            return await _context.Patients.FindAsync(gender) ?? throw new InvalidOperationException("Patient not found");
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
                result.Result = await _context.Patients.Where(filter).ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;

        }
        public override async Task<Patients> GetEntityByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id) ?? throw new InvalidOperationException("Entity not found");
        }

        public override async Task<bool> ExistsAsync(Expression<Func<Patients, bool>> filter)
        {
            return await _context.Patients.AnyAsync(filter);
        }
    }



}

