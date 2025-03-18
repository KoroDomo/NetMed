using NetMed.Domain.Base;

namespace NetMed.Infraestructure.Validators
{
    public interface IValidations 
    {
        OperationResult IsNullOrWhiteSpace<TEntity>(TEntity entity);
        OperationResult IsInt<TEntity>(TEntity entity);
        OperationResult CheckDate<TEntity>(TEntity entity);
        Task<OperationResult> ExistsEntity<TEntity>(TEntity entity, Func<TEntity, Task<bool>> searchFunction);
        Task<OperationResult> PatientExists(int PatientID, Func<int, Task<bool>> searchFunction);
        OperationResult AppointmentExists(int patientId, int doctorId, DateTime appointmentDate, int statusID, Func<int, int, DateTime, int, bool> searchFunction);
        OperationResult Time(TimeOnly StartTime, TimeOnly EndTime);

    }
}

