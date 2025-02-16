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
     public  class StatusRepository : BaseRepository<Status>, IStatusRepository
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
        public override Task<Status> GetEntityByIdAsync(int id)
        {
            return base.GetEntityByIdAsync(id);

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
            var result = new OperationResult();

            if (statusId < 0)
            {
                result.success = false;
                result.Mesagge = "ID no válido.";
                return result;
            }

            try
            {
                var status = await _context.statuses
                    .FirstOrDefaultAsync(n => n.Id == statusId);

                if (status == null)
                {
                    result.success = false;
                    result.Mesagge = "Status no encontrado.";
                }
                else
                {
                    result.success = true;
                    result.Mesagge = "Statu obtenida con éxito.";
                    result.data = status;
                }
            }
            catch (Exception ex)
            {

                result.success = false;
                result.Mesagge = $"Error al obtener el rol: {ex.Message}";
                await _context.SaveChangesAsync();
            }
            return result;
        
        }

        public async Task <OperationResult> CreateStatusAsync(Status status)
        {
            var validationResult = EntityValidator.ValidateNotNull(status, "Status");


            if (!validationResult.success)
            {
                return validationResult;

            }

            var result = new OperationResult();
            try
            {

                {
                    _context.statuses.Add(status);
                    await _context.SaveChangesAsync();
                    result.success = true;
                    result.Mesagge = "El status se a creado con exito";

                };
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Mesagge = "Problemas con crear el status";

            }

            return result;
        }

        public async Task<OperationResult> UpdateStatusAsync(Status status)
        {
            var validationResult = EntityValidator.ValidateNotNull(status, "Status");


            if (!validationResult.success)
            {
                return validationResult;

            }

            var result = new OperationResult();
            try
            {

                {
                    _context.statuses.Update(status);
                    await _context.SaveChangesAsync();
                    result.success = true;
                    result.Mesagge = "El Status se actualizo con exito";

                };
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Mesagge = "Problemas con actualizar el status";

            }

            return result;
        }


       
    }
}
