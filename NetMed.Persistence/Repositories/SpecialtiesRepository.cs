
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NetMed.Persistence;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Metadata;
using NetMed.Model.Models;

namespace NetMed.Persistence.Repositories
{
    public class SpecialtiesRepository : BaseRepository<Specialties>, ISpecialtiesRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<SpecialtiesRepository> _logger;
        private readonly IConfiguration _configuration;

        public SpecialtiesRepository(NetMedContext context, ILogger<SpecialtiesRepository> logger, IConfiguration configuration)
            : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<OperationResult> SaveEntityAsync(Specialties entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if(entity == null)
                {
                    result.Success = false;
                    result.Message = "La especialidad no puede ser nula";
                    return result;
                }
                if (string.IsNullOrEmpty(entity.SpecialtyName))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede ser nulo";
                    return result;
                }
                if(entity.SpecialtyName.Length > 90)
                {
                    //  "Medicina física y rehabilitación con subespecialidad en rehabilitación neuromuscular" siendo la especialidad mas larga; 84 caracteres
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede tener mas de 90 caracteres";
                    return result;
                }
                if (await base.Exists(specialty => specialty.SpecialtyName == entity.SpecialtyName))
                {
                    result.Success = false;
                    result.Message = "La especialidad ya existe";
                    return result;
                }
                
                await base.SaveEntityAsync(entity);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error guardando la especialidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(Specialties entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "La especialidad no puede ser nula";
                    return result;
                }
                if (string.IsNullOrEmpty(entity.SpecialtyName))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede ser nulo";
                    return result;
                }
                if (entity.SpecialtyName.Length > 90)
                {
                    //  "Medicina física y rehabilitación con subespecialidad en rehabilitación neuromuscular" siendo la especialidad mas larga; 84 caracteres
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede tener mas de 90 caracteres";
                    return result;
                }
                if (!await base.Exists(specialty => specialty.SpecialtyName == entity.SpecialtyName))
                {
                    result.Success = false;
                    result.Message = "La especialidad no existe";
                    return result;
                }
                await base.UpdateEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error actualizando la especialidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }


        public async Task<OperationResult> GetByNameAsync(string specialtyName)
        {
            OperationResult result = new OperationResult();
            try
            {
                var specialty = await _context.Specialties.FirstOrDefaultAsync
                    (s => s.SpecialtyName == specialtyName);

                if (specialty == null)
                {
                    result.Success = false;
                    result.Message = "No se encontró la especialidad";
                    return result;
                }
                result.Data = specialty;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error obteniendo la especialidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        //El Remove debe ser un Update que cambie la propiedad IsActive a false
        /*
        public async Task<OperationResult> RemoveEntityAsync(Specialties entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "La especialidad no puede ser nula";
                    return result;
                }
                if (string.IsNullOrEmpty(entity.SpecialtyName))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede ser nulo";
                    return result;
                }
                if (entity.SpecialtyName.Length > 90)
                {
                    //  "Medicina física y rehabilitación con subespecialidad en rehabilitación neuromuscular" siendo la especialidad mas larga; 84 caracteres
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede tener mas de 90 caracteres";
                    return result;
                }
                if (!await base.Exists(specialty => specialty.SpecialtyName == entity.SpecialtyName))
                {
                    result.Success = false;
                    result.Message = "La especialidad no existe";
                    return result;
                }
                await base.UpdateEntityAsync(entity.IsActive = false);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error eliminando la especialidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        */

        public override async Task<Specialties> GetEntityByIdAsync(short Id)
        {
            OperationResult result = new OperationResult();
            var specialty = await _context.Specialties.FirstOrDefaultAsync(s => s.Id == Id);
            try
            {
                if (specialty == null)
                {
                    result.Success = false;
                    result.Message = "No se encontró la especialidad";
                    return specialty;
                }
                return specialty;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error obteniendo la especialidad";
                _logger.LogError(result.Message, ex.ToString());
                return null;
            }
        }
    }
}
