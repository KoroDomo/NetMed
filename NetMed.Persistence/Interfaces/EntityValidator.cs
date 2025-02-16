

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
                result.success = false;
                result.Mesagge = $"{entityName} No puede ser nulo";
            }
            else 
            {

             result.success = true;
           
            }
            return result;

        }



    }
}
