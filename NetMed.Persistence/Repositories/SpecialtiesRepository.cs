
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using System.Reflection.Metadata.Ecma335;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NetMed.Model.Models;

namespace NetMed.Persistence.Repositories
{
    public class SpecialtiesRepository : BaseRepository<Specialties>, ISpecialtiesRepository
    {
        private readonly NetMedContext context;
        private readonly ILogger<SpecialtiesRepository> logger;
        private readonly IConfiguration configuration;

        public SpecialtiesRepository(NetMedContext context, ILogger<SpecialtiesRepository> logger, IConfiguration configuration) : base(context)
        {
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }
        public override Task<OperationResult> SaveEntityAsync(Specialties entity)
        {
            //agregar validaciones correspondientes

            return base.SaveEntityAsync(entity);
        }

        public async Task<OperationResult> SpecialtyName(string SpecialtyName)
        {
            OperationResult result = new OperationResult();
            try
            {
                var querys = await (from specialty in context.Specialties
                             where specialty.SpecialtyName == SpecialtyName
                             select new SpecialtiesModel() 
                             {  
                                 Id = specialty.Id, 
                                 Name = specialty.SpecialtyName
                             }).ToListAsync();

                //result.Data = querys;
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["Messages:ErrorSpecialtiesRepository:SpecialtyName"];
                result.Success = false;
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public override Task<OperationResult> UpdateEntityAsync(Specialties entity)
        {
            //agregar validaciones correspondientes
            return base.UpdateEntityAsync(entity);
        }

       
    }
}
