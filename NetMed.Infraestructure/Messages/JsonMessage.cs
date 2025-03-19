namespace NetMed.Infraestructure.Validators
{
    public class JsonMessage
    {
        /// <summary>
        /// Diccionario de mensajes de error agrupados por categoría.
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> ErrorMessages { get; }

        /// <summary>
        /// Diccionario de mensajes de éxito.
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> SuccessMessages { get; }
        public JsonMessage()
        {
            ErrorMessages = new Dictionary<string, Dictionary<string, string>>
            {
                // Mensaje de error genérico
                ["Generic"] = new Dictionary<string, string>
                {
                    ["GenericError"] = "Ha ocurrido un error inesperado."
                },
                // Errores generales de entidad
                ["Entitys"] = new Dictionary<string, string>
                {
                    ["InvalidID"] = "ID no válido.",
                    ["NotFound"] = "Registro no encontrado.",
                    ["DuplicateEntry"] = "Registro duplicado detectado.",
                    ["ProtectedDelete"] = "El registro no puede ser eliminado porque está siendo referenciado.",
                    ["NullEntity"] = "Objeto de entidad nulo.",
                    ["DuplicateName"]= "Ya existe un registro con este nombre."
                },

                // Errores de operaciones CRUD
                ["Operations"] = new Dictionary<string, string>
                {
                    ["SaveFailed"] = "Error al guardar el registro.",
                    ["GetFailed"] = "Error al obtener el registro.",
                    ["UpdateFailed"] = "Error al actualizar el registro.",
                    ["DeleteFailed"] = "Error al eliminar el registro.",
                    ["RestoreFailed"] = "Error al restaurar el registro.",
                    ["DbException"] = "Excepción en la base de datos."
                },
                // Validaciones específicas de InsuranceProviders
                ["Insurances"] = new Dictionary<string, string>
                {
                    ["RemoveInsurenProvider"] = "Error al remover el Provider.",
                    ["GetInsurenProvider"] = "Error al obtener el Provider.",
                    ["GetPreferredInsuranceProviders"] = "Error al obtener los Providers con preferencia.",
                    ["GetActiveInsuranceProviders"] = "Error al obtener los Provider/s activos.",
                    ["isNull"] = "El campo InsuranceProvider nulo.",
                    ["NetworkTypeID"] = "El campo NetworkTypeID debe ser un número entero.",
                    ["MaxCoverageAmount"] = "El campo MaxCoverageAmount no es valido.",
                    ["Name"] = "Ya existe un InsuranceProvider con este nombre."
                },
                // Validaciones específicas de NetworkType
                ["Networks"] = new Dictionary<string, string>
                {
                    ["RemoveNetworkType"] = "Error al remover el Network.",
                    ["GetNetworkType"] = "Error al obtener el Network/s.",
                    ["isNull"] = "NetworkType nulo.",
                    ["Name"] = "Ya existe un NetworkType con este nombre."
                }


            };

            SuccessMessages = new Dictionary<string, Dictionary<string, string>>
            {
                // Mensaje de error genérico
                ["Operations"] = new Dictionary<string, string>
                {
                    ["GenericSuccess"] = "Operación completada exitosamente.",
                    ["GetSuccess"] = "Registro obtenido exitosamente.",
                    ["SaveSuccess"] = "Registro guardado exitosamente.",
                    ["UpdateSuccess"] = "Registro actualizado exitosamente.",
                    ["DeleteSuccess"] = "Registro eliminado exitosamente."
                    
                },
                // Mensaje de exito de InsuranceProviders
                ["Insurances"] = new Dictionary<string, string>
                {
                    ["RemoveInsurenProvider"] = "Provider eliminado exitosamente.",
                    ["GetInsurenProvider"] = "Provider/s obtenido exitosamente.",
                    ["GetPreferredInsuranceProviders"] = "Provider/s con preferencia obtenidos exitosamente.",
                    ["GetActiveInsuranceProviders"] = "Provider/s activos obtenidos exitosamente."

                },
                // Mensaje de exito de NetworkType
                ["Networks"] = new Dictionary<string, string>
                {
                    ["RemoveNetworkType"] = "Network eliminado exitosamente.",
                    ["GetNetworkType"] = "Network/s obtenido exitosamente."
                }

            };
        }
    }
}