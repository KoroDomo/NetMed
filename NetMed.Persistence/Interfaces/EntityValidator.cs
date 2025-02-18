

using NetMed.Domain.Base;

namespace NetMed.Persistence.Interfaces
{
    public static class EntityValidator
    {
        public static OperationResult ValidateNotNull<T>(T entity, string entityName)
        { 
         var result = new OperationResult();


            if (entity == null)
            {
                result.Success = false;
                result.Mesagge = $"{entityName} No puede ser nulo";
            }
            else 
            {

             result.Success = true;
           
            }
            return result;

        }

        //Debo buscar un sitio donde ubicar esta clase.
        //Debo agregar más métodos para no repetir tanto código.
     

    }
}
