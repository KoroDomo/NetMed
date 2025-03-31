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
                result.success = false;
                result.message = "El paciente ya tiene una cita programada con este doctor para esta fecha y hora";
                return result;
            }

            result.success = true;
            return result;
        }
        public OperationResult CheckDate<TEntity>(TEntity entity)
        {
            var result = new OperationResult();

            if (entity is DateTime date && date < DateTime.Now.Date)
            {
                result.success = false;
                result.message = "La fecha debe ser mayor a la fecha actual";
                return result;
            }
            result.success = true;
            result.data = entity;
            return result;
        }
        public async Task<OperationResult> ExistsEntity<TEntity>(TEntity entity, Func<TEntity, Task<bool>> searchFunction)
        {
            var result = new OperationResult();

            if (!await searchFunction(entity))
            {
                result.success = false;
                result.message = "No existe este registro en el sistema";
                return result;
            }
            result.success = true;
            return result;
        }
        public OperationResult IsInt<TEntity>(TEntity entity)
        {
            var result = new OperationResult();

            if (entity is int intValue)
            {
                if (intValue <= 0)
                {
                    result.success = false;
                    result.message = "El Id debe ser mayor que cero";
                    return result;
                }
            }
            result.success = true;
            return result;
        }
        public OperationResult IsNullOrWhiteSpace<TEntity>(TEntity entity)
        {
            var result = new OperationResult();

            if (entity == null)
            {
                result.success = false;
                result.message = "La entidad no puede ser nula";
            }
            else if (entity is string str && string.IsNullOrWhiteSpace(str))
            {
                result.success = false;
                result.message = "La entidad no puede ser nula ni estar en blanco";
            }
            else
            {
                result.success = true;
            }
            return result;
        }
        public async Task<OperationResult> PatientExists(int PatientID, Func<int, Task<bool>> searchFunction)
        {
            var result = new OperationResult();

            if (!await searchFunction(PatientID))
            {
                result.success = false;
                result.message = "El paciente no existe en el sistema";
                return result;
            }
            result.success = true;
            return result;
        }
        public OperationResult Time(TimeOnly StartTime, TimeOnly EndTime)
        {
            var result = new OperationResult();

            if (StartTime >= EndTime)
            {
                result.success = false;
                result.message = "La hora de inicio debe ser anterior a la hora de finalización.";
                return result;
            }
            result.success = true;
            return result;
        }
    }
}