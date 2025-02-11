

using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories.Interfaces;


namespace NetMed.Persistence.Repositories
{
    public class PatientsRepository : BaseRepository<Patients, int> , IPatientsRepository
    {
        private readonly NetMedContext context;
        public PatientsRepository(NetMedContext context) : base(context)
        {
            this.context = context;
        }
        public override Task<OperationResult> SaveEntityAsync(Patients entity)
        {
            //Agregar las validaciones necesarias
            OperationResult result = new OperationResult();
            //try
            //{
            //    context.Patients.Add(entity);
            //    context.SaveChanges();
            //}
            //catch (Exception ex)
            //{
            //    result.Mesagge = ex.Message + " Ocurrio un error guardando los datos.";
            //    result.Success = false;
            //}
            return Task.FromResult(result);
        }

    }
}
