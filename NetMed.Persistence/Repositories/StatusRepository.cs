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
using System.ComponentModel.DataAnnotations;



namespace NetMed.Persistence.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        private readonly NetmedContext _context;
        private readonly ILogger<StatusRepository> _logger;
        private readonly IConfiguration _configuration;

        public StatusRepository(NetmedContext context,
                                     ILogger<StatusRepository> logger,
                                     IConfiguration configuration) : base(context)
        {

            this._context = context;
            this._logger = logger;
            this._configuration = configuration;
        }
        public override Task<Status> GetEntityByIdAsync(int statusID)
        {
            return base.GetEntityByIdAsync(statusID);

        }

        public override Task<OperationResult> SaveEntityAsync(Status entity)
        {
            return base.SaveEntityAsync(entity);
        }

        public override Task<List<Status>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override Task<OperationResult> UpdateEntityAsync(Status entity)
        {

            return base.UpdateEntityAsync(entity);
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Status, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }


        public async Task<OperationResult> GetStatusByIdAsync(int statusId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(statusId, "No esta permitido valores negativos");

            if (!validationResult.Success)
            {

                return validationResult;

            }

            try
            {
                var status = await _context.statuses.FindAsync(statusId);

                var notNullStatus = EntityValidator.ValidateNotNull(statusId, "La status no a sido encontrada");


                if (!notNullStatus.Success)
                {

                    return notNullStatus;

                }
                else
                {
                    return new OperationResult { Success = true, Mesagge = "Status obtenido con éxito", Data = statusId };

                }
            }
            catch (Exception ex)
            {
                await _context.SaveChangesAsync();
                return new OperationResult { Success = false, Mesagge = "Error al obtener el status" };

            }
        }

        public async Task<OperationResult> CreateStatusAsync(Status status)
        {
            var validationResult = EntityValidator.ValidateNotNull(status, "LA notificacion no puede ser creada");


            if (!validationResult.Success)
            {
                return validationResult;

            }

            try
            {

                {
                    _context.statuses.Add(status);
                    await _context.SaveChangesAsync();

                    return new OperationResult { Success = true, Mesagge = "El Status se a creado con exito" };


                };
            }
            catch (Exception ex)
            {

                return new OperationResult { Success = false, Mesagge = "Problemas con crear el status" };

            }

        }

        public async Task<OperationResult> UpdateStatusAsync(Status status)
        {
            var validationResult = EntityValidator.ValidateNotNull(status, "El status no se a encontrado");


            if (!validationResult.Success)
            {
                return validationResult;

            }

            try
            {

                {
                    _context.statuses.Update(status);
                    await _context.SaveChangesAsync();

                    return new OperationResult { Success = true, Mesagge = "El status se actualizo con exito" };

                };
            }
            catch (Exception ex)
            {

                return new OperationResult { Success = false, Mesagge = "Problemas con actualizar el status" };

            }

        }

        public async Task<OperationResult> DeleteStatusAsync(int statusId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(statusId, "No esta permitido valores negativos");

            if (!validationResult.Success)
            {

                return validationResult;

            }

            try
            {
                var status = await _context.statuses.FindAsync(statusId);

                var notNullStatus = EntityValidator.ValidateNotNull(status, "El status no a sido encontrada");

                if (!notNullStatus.Success)
                {
                    return notNullStatus;
                }; 

                _context.statuses.Remove(status);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Mesagge = " eliminado con exito" };

            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Mesagge = "Surgieron problemas a la hora de eliminar el status"

                };

            }
        }
    }
}

