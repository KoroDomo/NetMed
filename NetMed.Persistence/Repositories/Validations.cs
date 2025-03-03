
using NetMed.Domain.Base;

namespace NetMed.Persistence.Repositories
{
    public static class Validations
    {       
        public static OperationResult IsNullOrWhiteSpace<TEntity>(TEntity entity, string entityName)
        {
            var result = new OperationResult();

            if (entity is string str && string.IsNullOrWhiteSpace(str))
            {
                result.Success = false;
                result.Message = $"{entityName} no puede ser nulo ni estar en blanco";
                return result;
            }
            result.Success = true;
            return result;
        }
        public static OperationResult IsInt<TEntity>(TEntity entity)
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
        public static OperationResult CheckDate<TEntity>(TEntity entity)
        {
            var result = new OperationResult();

            if (entity is DateTime date && date >= DateTime.Now)
            {
                result.Success = false;
                result.Message = "La fecha debe ser mayor a la fecha actual";
                return result;
            }
            result.Success = true;
            return result;
        }
        public static async Task<OperationResult> ExistsEntity<TEntity>(TEntity entity, Func<TEntity, Task<bool>> searchFunction)
        {
            var result = new OperationResult();

            if (await searchFunction(entity))
            {
                result.Success = false;
                result.Message = $"Ya existe este registro en el sistema";
                return result;
            }
            result.Success = true;
            return result;
        }
        public static OperationResult ValidateStringLength(string entity, string entityName, int maxLength)
        {
            var result = new OperationResult();

            if (!string.IsNullOrEmpty(entity) && entity.Length > maxLength)
            {
                result.Success = false;
                result.Message = $"{entityName} no puede tener más de {maxLength} caracteres";
                return result;
            }
            result.Success = true;
            return result;
        }
        public static OperationResult PatientExists(int PatientID, Func<int, bool> searchFunction)
        {
            var result = new OperationResult();

            if (!searchFunction(PatientID))
            {
                result.Success = false;
                result.Message = "El paciente no existe en el sistema";
                return result;
            }

            result.Success = true;
            return result;
        }
        public static OperationResult DoctorIsActive(int DoctorID, Func<int, bool> isActiveFunction)
        {
            var result = new OperationResult();

            if (!isActiveFunction(DoctorID))
            {
                result.Success = false;
                result.Message = "El médico no está activo en el sistema";
                return result;
            }
            result.Success = true;
            return result;
        }
        public static OperationResult AppointmentExists(int patientId, DateTime appointmentDate, Func<int, DateTime, bool> searchFunction)
        {
            var result = new OperationResult();

            if (searchFunction(patientId, appointmentDate))
            {
                result.Success = false;
                result.Message = "El paciente ya tiene una cita programada para esta fecha y hora";
                return result;
            }

            result.Success = true;
            return result;
        }
        public static OperationResult DoctorAvailability(int doctorId, DateTime appointmentDate, Func<int, DateTime, bool> availabilityFunction)
        {
            var result = new OperationResult();

            if (!availabilityFunction(doctorId, appointmentDate))
            {
                result.Success = false;
                result.Message = "El médico no está disponible para esta fecha y hora";
                return result;
            }
            result.Success = true;
            return result;
        }
    }
}

