
using NetMedWebApi.Infrastructure.MessageJson.interfaces;

namespace NetMedWebApi.Infrastructure.MessageJson
{
    public class JsonMessage : IJsonMessage
    {
        // Diccionario de mensajes de error.

        public Dictionary<string, string> ErrorMessages { get; }

        // Diccionario de mensajes de éxito.
        public Dictionary<string, string> SuccessMessages { get; }

        public Dictionary<string, string> ErrorNotification { get; }



        public JsonMessage()
        {
            ErrorMessages = new Dictionary<string, string>
            {
                ["ValidationFailed"] = "Validación fallida }",
                ["GetAllNull"] = "Problemas extrayendo los valores",
                ["NotificationNotFound"] = "No se encontró la notificación",
                ["GeneralError"] = "Error al procesar la solicitud",
                ["InvalidId"] = "El ID proporcionado no es válido",
                ["InvalidNullId"] = "El ID no puede ser nulo ",
                ["NullEntity"] = "La entidad no puede ser nula",
                ["DatabaseError"] = "Error en la base de datos",
                ["RoleNotFound"] = "No se encontró el rol ",
                ["NotificationNotFound"] = "No se encontró la notificación",
                ["StatusNotFound"] = "No se encontró el status ",
                ["EntityNotFound"] = "No se encontró la entidad ",
                ["EntityNotCreated"] = "La entidad no pudo ser creada ",
                ["EntityNotUpdated"] = "La entidad no pudo ser actualizada",
                ["EntityNotDeleted"] = "La entidad no pudo ser desactivada",
                ["NotificationNull"] = "La notificacion no puede ser nula",
                ["RolesNull"] = "Los roles no puede ser nula",
                ["StatusNull"] = "El estado no puede ser nula",
                ["NotificationLessZero"] = "El ID de notificacion no puede ser menor a 0",
                ["RolesLessZero"] = "El ID de  roles no puede ser menor a 0",
                ["StatusLessZero"] = "El ID de Status no puede ser menor a 0",

                ["NotificationMessage"] = "El mensaje no puede pasar de los 600 caracteres",
                ["RolesMessage"] = "Los roles no puede pasar de 20 caracteres",
                ["NotificationSentAt"] = "No puede sobrepasar el tiempo mano",
                ["RoleIsNotActive"] = " El rol no esta activo",
                ["RegexIsNull"] = "La entrada no puede ser nula o vacía.",
                ["InvalidDate"] = "La fecha no puede ser mayor que la actual"

            };

            SuccessMessages = new Dictionary<string, string>
            {
                ["EntityRetrieved"] = "Entidad recumperada con exito",
                ["EntityFound"] = "Entidad encontrada con exito",
                ["GetAllEntity"] = "Valores extraídos con éxito ",
                ["EntityCreated"] = "Entidad guardada con éxito ",
                ["EntityUpdated"] = "Entidad actualizada con éxito",
                ["EntityDeleted"] = "Entidad desactivada con éxito ",
                ["ValidationSucceeded"] = "Validación exitosa ",
                ["NotificationFound"] = "Notificación encontrada ",
                ["RoleFound"] = "Rol encontrado con exito",
                ["RoleCreated"] = "Rol creado con éxito",
                ["RoleUpdated"] = "Rol actualizado con éxito",
                ["RoleDeleted"] = "Rol desactivado con éxito",
                ["StatusFound"] = "Estado encontrado con exito",
                ["EntityFound"] = "Entidad encontrada ",
                ["StatusRetrieved"] = "Status obtenido con éxito ",
                ["StatusCreated"] = "Status guardado con éxito ",
                ["StatusUpdated"] = "Status actualizado con éxito ",
                ["StatusDeleted"] = "Status desactivado con éxito ",
                ["NotificationRetrieved"] = "Notificación obtenida con éxito ",
                ["NotificationCreated"] = "Notificación creada con éxito ",
                ["NotificationUpdated"] = "Notificación actualizada con éxito ",
                ["NotificationDeleted"] = "Notificación desactivada con éxito ",
                ["RoleIsActive"] = " El rol esta activo",
                ["RegexSuccess"] = "La entrada es válida.",
                ["ApiConfiguration"] = "APi configurada con exito"


            };

        }
    }

}

