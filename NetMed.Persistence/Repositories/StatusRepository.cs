using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Interfaces;
using System.Data;
using System.Linq.Expressions;



namespace NetMed.Persistence.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        private readonly NetmedContext _context;
        private readonly ILogger<StatusRepository> _logger;
        private readonly IConfiguration _configuration;

        public StatusRepository(NetmedContext context,
                               ILogger<StatusRepository> logger,
                               IConfiguration configuration) : base(context,logger,configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

     

        public override Task<List<Status>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override async Task<OperationResult> UpdateEntityAsync(Status status)
        {
            var validationResult = EntityValidator.ValidateNotNull(status, "La entidad 'Status' no puede ser nula");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                await _context.statuses.AddAsync(status);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Status creado con éxito", Data = status };
            }
            catch (Exception ex)
            {
                return new OperationResult
                { Success = false, Message = "Error con la base de datos" };
            }
        }
        

        public override Task<OperationResult> GetAllAsync(Expression<Func<Status, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public async Task<OperationResult> GetStatusByIdAsync(int statusId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(statusId, "El ID proporcionado no es válido");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                var status = await _context.statuses.FindAsync(statusId);

                var notNullStatus = EntityValidator.ValidateNotNull(status, " Status no encontrado");

                if (!notNullStatus.Success)
                {
                    return notNullStatus;
                }

                return new OperationResult { Success = true, Message = "Status obtenido con éxito", Data = status };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al procesar la solicitud" };
            }
        }

        public async Task<OperationResult> CreateStatusAsync(Status status)
        {
            var validationResult = EntityValidator.ValidateNotNull(status, "La entidad 'Status' no puede ser nula");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                await _context.statuses.AddAsync(status);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Status creado con éxito", Data = status};
            }
            catch (Exception ex)
            {
                return new OperationResult
                { Success = false, Message = "Error al procesar la solicitud" };
            }
        }

        public async Task<OperationResult> UpdateStatusAsync(Status status)
        {
            var validationResult = EntityValidator.ValidateNotNull(status, " La entidad no puede ser nula");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                _context.statuses.Update(status);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Estado actualizado con éxito", Data = status };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al procesar la solicitud" };
            }
        }

        public async Task<OperationResult> DeleteStatusAsync(int statusId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(statusId, "El ID proporcionado no es válido");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                var status = await _context.statuses.FindAsync(statusId);

                var notNullStatus = EntityValidator.ValidateNotNull(status, "No se encontró el status");

                if (!notNullStatus.Success)
                {
                    return notNullStatus;
                }

                _context.statuses.Remove(status);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Estado eliminado con éxito", Data = status };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al procesar la solicitud" };


            }
        }
    }
}