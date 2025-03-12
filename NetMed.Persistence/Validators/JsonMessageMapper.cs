
namespace NetMed.Persistence.Validators
{
    public class MessageMapper
    {
        /// <summary>
        /// Diccionario de mensajes de error agrupados por categoría.
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> ErrorMessages { get; }

        /// <summary>
        /// Diccionario de mensajes de éxito.
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> SuccessMessages { get; }
        //_logger.LogError(ex, _messageMapper.ErrorMessages["Categoria"]["GetAllError"]);
        //_logger.LogWarning(_messageMapper.ErrorMessages["EntityBase"]["InvalidID"]);
        //Message = _messageMapper.SuccessMessages["SaveSuccess"],
        //Message = _messageMapper.ErrorMessages["EntityBase"]["NotFound"]
        public MessageMapper()
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

                //_operations.GetErrorMessage("Entitys", "NotFound")
                //_operations.HandleException("Entitys", "NotFound")
                //_logger.LogInformation(_operations.GetSuccesMessage("Insurances", "RemoveInsurenProvider"));
                //_operations.SuccessResult(provider, "Insurances", "RemoveInsurenProvider");
                //_logger.LogError(ex, _operations.GetErrorMessage("Insurances", "RemoveInsurenProvider"));

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
                // Validaciones específicas de Insurance
                ["Insurances"] = new Dictionary<string, string>
                {
                    ["RemoveInsurenProvider"] = "Error al remover el Provider.",
                    ["GetInsurenProvider"] = "Error al obtener el Provider.",
                    ["GetPreferredInsuranceProviders"] = "Error al obtener los Providers con preferencia.",
                    ["GetActiveInsuranceProviders"] = "Error al obtener los Provider/s activos."
                },
                ["Networks"] = new Dictionary<string, string>
                {
                    ["RemoveNetworkType"] = "Error al remover el Network.",
                    ["GetNetworkType"] = "Error al obtener el Network/s."
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