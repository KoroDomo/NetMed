
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Context.Interfaces
{
    public interface IStatusRepository : IBaseRepository<Status> 
    {
        public interface IStatusRepository
        {
            // Obtener todos los estados
            Task<IEnumerable<Status>> GetAllStatusesAsync();

            // Obtener un estado por su ID
            Task<Status> GetStatusByIdAsync(int statusId);

            // Crear un nuevo estado (si es necesario)
            Task CreateStatusAsync(Status status);

            // Actualizar un estado (si es necesario)
            Task UpdateStatusAsync(Status status);
        }


    }
}
