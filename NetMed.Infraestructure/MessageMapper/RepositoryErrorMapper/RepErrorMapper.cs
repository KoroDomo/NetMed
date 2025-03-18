using NetMed.Infrastructure.Mapper.IRepositoryErrorMapper;


namespace NetMed.Infrastructure.Mapper.RepositoryErrorMapper
{
    public class RepErrorMapper : IRepErrorMapper
    {
        public Dictionary<string, string> ErrorDoctorsRepositoryMessages { get; set; }
        public Dictionary<string, string> ErrorPatientsRepositoryMessages { get; set; }
        public Dictionary<string, string> ErrorUsersRepositoryMessages { get; set; }
        public Dictionary<string, string> SuccessDoctorsRepositoryMessages { get; set; }
        public Dictionary<string, string> SuccessPatientsRepositoryMessages { get; set; }
        public Dictionary<string, string> SuccessUsersRepositoryMessages { get; set; }
        public Dictionary<string, string> DataISNullErrorGlogal { get; set; }
        public Dictionary<string, string> SaveEntityErrorMessage { get; set; }
        public Dictionary<string, string> UpdateEntityErrorMessage { get; set; }
        public Dictionary<string, string> GetEntityByIdErrorMessage { get; set; }
        public Dictionary<string, string> GetAllEntitiesErrorMessage { get; set; }

        public RepErrorMapper()
        {
            ErrorDoctorsRepositoryMessages = InitializeDictionary(new Dictionary<string, string>
            {
                ["GetByAvailabilityModeAsync"] = "Error al obtener disponibilidad",
                ["GetByLicenseNumberAsync"] = "Error al obtener numero de licencia",
                ["GetBySpecialtyAsync"] = "Error al obtener la especialidad",
                ["GetDoctorByConsultationFeeAsync"] = "Error al obtener el precio de consulta",
                ["GetDoctorWithExpiringLicenseAsync"] = "Error al obtener la fecha de expiracion",
                ["GetActiveDoctorsAsync"] = "Error al obtener doctores activos",
                ["GetDoctorsByExperienceAsync"] = "Error al obtener doctor por experiencia",
               
            });

            ErrorPatientsRepositoryMessages = InitializeDictionary(new Dictionary<string, string>
            {
                ["GetPatientsAsyncWithoutInsuranceAsync"] = "Error al obtener paciente sin seguro",
                ["GetByBloodTypeAsync"] = "Error al obtener el tipo de sangre",
                ["GetByInsuranceProviderAsync"] = "Error al obtener el proveedor de seguro",
                ["SearchByAddressAsync"] = "Error al obtener la direccion",
                ["GetPatientsByAgeRangeAsync"] = "Error al obtener el rango de edad del paciente",
                ["GetByEmergencyContactAsync"] = "Error al obtener el contacto de emergencia",
                ["GetPatientsWithAllergiesAsync"] = "Error al obtener las alergias del paciente",
                ["GetPatientsByGenderAsync"] = "Error al obtener genero del paciente"
            });

            ErrorUsersRepositoryMessages = InitializeDictionary(new Dictionary<string, string>
            {
                ["GetEmailAsync"] = "Error obteniendo el email",
                ["GetActiveUsersAsync"] = "Error cambiado el estado de usuario",
                ["GetByRoleByIDAsync"] = "Error obteniendo Rol de usuario",
                ["SearchByNameAsync"] = "Error obteniendo nombre de usuario",
                ["GetPhoneNumberAsync"] = "Error obteniendo numero de telefono",
                ["GetAddressAsync"] = "Error obteniendo direccion",
                ["GetPasswordAsync"] = "Error obteniendo contraseña",
                ["GetUsersRegisteredInRangeAsync"] = "Error obteniendo usuarios registrados en rango"
            });

            SuccessDoctorsRepositoryMessages = InitializeDictionary(new Dictionary<string, string>
            {
                ["GetByAvailabilityModeAsync"] = "Disponibilidad obtenida correctamente",
                ["GetByLicenseNumberAsync"] = "Numero de licencia obtenido correctamente",
                ["GetBySpecialtyAsync"] = "Especialidad obtenida correctamente",
                ["GetDoctorByConsultationFeeAsync"] = "Precio de consulta obtenido correctamente",
                ["GetDoctorWithExpiringLicenseAsync"] = "Fecha de expiracion obtenida correctamente"
            });

            SuccessPatientsRepositoryMessages = InitializeDictionary(new Dictionary<string, string>
            {
                ["GetPatientsAsyncWithoutInsuranceAsync"] = "Disponibilidad obtenida correctamente",
                ["GetBloodTypeAsync"] = "Tipo de sangre obtenido correctamente",
                ["GetByInsuranceProviderAsync"] = "Proveedor obtenido correctamente",
                ["SearchByAdressAsync"] = "Direccion obtenida correctamente",
                ["GetPatientsByAgeRangeAsync"] = "Rango de edad obtenido correctamente",
                ["GetByEmergencyContactAsync"] = "Contacto de emergencia obtenido correctamente",
                ["GetPatientsWithAllergiesAsync"] = "Alergias obtenidas correctamente",
                ["GetPatientsByGenderAsync"] = "Genero del paciente obtenido exitosamente"
            });

            SuccessUsersRepositoryMessages = InitializeDictionary(new Dictionary<string, string>
            {
                ["GetEmailAsync"] = "Email obtenido correctamente",
                ["GetActiveUsersAsync"] = "Estado del usuario obtenido exitosamente",
                ["GetByRoleByIDAsync"] = "Rol del usuario obtenido correctamente",
                ["SearchByNameAsync"] = "Nombre de usuario obtenido con exito",
                ["GetPhoneNumberAsync"] = "Numero de telefono obtenido correctamente",
                ["GetAddressAsync"] = "Direccion obtenida correctamente",
                ["GetPasswordAsync"] = "Contraseña obtenida correctamente"
            });

            DataISNullErrorGlogal = InitializeDictionary(new Dictionary<string, string>
            {
                ["DataIsNull"] = "No se encontraron datos",
                ["DataDoctorsIsNull"] = "No se encontraron datos de doctores",
                ["DataPatientsIsNull"] = "No se encontraron datos de pacientes",
                ["DataUsersIsNull"] = "No se encontraron datos de usuarios"
            });

            SaveEntityErrorMessage = InitializeDictionary(new Dictionary<string, string>
            {
                ["SaveEntityError"] = "Error al guardar entidad"
            });

            UpdateEntityErrorMessage = InitializeDictionary(new Dictionary<string, string>
            {
                ["UpdateEntityError"] = "Error al actualizar entidad"
            });

            GetEntityByIdErrorMessage = InitializeDictionary(new Dictionary<string, string>
            {
                ["GetEntityByIdError"] = "Error al obtener entidad por id"
            });

            GetAllEntitiesErrorMessage = InitializeDictionary(new Dictionary<string, string>
            {
                ["GetAllEntitiesError"] = "Error al obtener todas las entidades"
            });
        }

        private Dictionary<string, string> InitializeDictionary(Dictionary<string, string> messages)
        {
            return new Dictionary<string, string>(messages);
        }
    }
}


