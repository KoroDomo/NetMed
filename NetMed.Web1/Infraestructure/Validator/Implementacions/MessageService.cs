using NetMed.ApiConsummer.Infraestructure.Validator.Interfaces;

namespace NetMed.ApiConsummer.Infraestructure.Validator.Implementacions
{
    public class MessageService : IMessageService
    {
        private static readonly Dictionary<string, Dictionary<string, string>> _messages = new()
        {
            ["Success"] = new()
            {
                ["Save"] = "Registro creado exitosamente",
                ["Delete"] = "Registro eliminado correctamente",
                ["Get"] = "Registro obtenidos con éxito",
                ["Update"] = "Registro actualizado"
            },
            ["Error"] = new()
            {
                ["Save"] = "Error al crear el registro",
                ["Delete"] = "Error al eliminar el registro",
                ["Get"] = "Error obteniendo registros",
                ["Update"] = "Error actualizando registros"
            },
            ["ServiceError"] = new()
            {
                ["Get"] = "Error en el servicio al obtener datos",
                ["Save"] = "Error en el servicio al crear registro",
                ["Update"] = "Error en el servicio al actualizar",
                ["Delete"] = "Error en el servicio al eliminar"
            }
        };

        public string GetSuccessMessage(string operationType)
        {
            return _messages["Success"][operationType];
        }
        public string GetErrorMessage(string operationType)
        {
            return _messages["Error"][operationType];
        }
        public string GetServiceErrorMessage(string operationType)
        {
            return _messages["ServiceError"][operationType];
        }
    }
}