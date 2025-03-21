using NetMed.Domain.Base;


namespace NetMed.Infraestructure.Validators
{
    public class Validations : IValidations
    {       
        
        public OperationResult AppointmentExists(int patientId, int doctorId, DateTime appointmentDate, int statusID, Func<int, int, DateTime, int, bool> searchFunction)
        {
            var result = new OperationResult();

            if (searchFunction(patientId, doctorId, appointmentDate, statusID))
            {
                result.Success = false;
                result.Message = "El paciente ya tiene una cita programada con este doctor para esta fecha y hora";
                return result;
            }

            result.Success = true;
            return result;
        }
        public OperationResult CheckDate<TEntity>(TEntity entity)
        {
            var result = new OperationResult();

            if (entity is DateTime date && date < DateTime.Now.Date)
            {
                result.Success = false;
                result.Message = "La fecha debe ser mayor a la fecha actual";
                return result;
            }
            result.Success = true;
            result.Data = entity;
            return result;
        }
        public async Task<OperationResult> ExistsEntity<TEntity>(TEntity entity, Func<TEntity, Task<bool>> searchFunction)
        {
            var result = new OperationResult();

            if (!await searchFunction(entity))
            {
                result.Success = false;
                result.Message = "No existe este registro en el sistema";
                return result;
            }
            result.Success = true;
            return result;
        }
        public OperationResult IsInt<TEntity>(TEntity entity)
        {
            var result = new OperationResult();

            if (entity is int intValue)
            {
                if (intValue <= 0)
                {
                    result.Success = false;
                    result.Message = "El Id debe ser mayor que cero";
                    return result;
                }
            }
            result.Success = true;
            return result;
        }
        public OperationResult IsNullOrWhiteSpace<TEntity>(TEntity entity)
        {
            var result = new OperationResult();

            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad no puede ser nula";
            }
            else if (entity is string str && string.IsNullOrWhiteSpace(str))
            {
                result.Success = false;
                result.Message = "La entidad no puede ser nula ni estar en blanco";
            }
            else
            {
                result.Success = true;
            }
            return result;
        }
        public async Task<OperationResult> PatientExists(int PatientID, Func<int, Task<bool>> searchFunction)
        {
            var result = new OperationResult();

            if (!await searchFunction(PatientID))
            {
                result.Success = false;
                result.Message = "El paciente no existe en el sistema";
                return result;
            }
            result.Success = true;
            return result;
        }
        public OperationResult Time(TimeOnly StartTime, TimeOnly EndTime)
        {
            var result = new OperationResult();

            if (StartTime >= EndTime)
            {
                result.Success = false;
                result.Message = "La hora de inicio debe ser anterior a la hora de finalización.";
                return result;
            }
            result.Success = true;
            return result;
        }
    }
}