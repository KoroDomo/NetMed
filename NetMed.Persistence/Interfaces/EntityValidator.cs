

using Microsoft.Identity.Client;
using NetMed.Domain.Base;

namespace NetMed.Persistence.Interfaces
{
    public static class EntityValidator
    {
        public static OperationResult ValidateNotNull<T>(T entity, string errorMessage)
        { 
         var result = new OperationResult();


            if (entity == null)
            {
                result.Success = false;
                result.Message = errorMessage;
            }
            else 
            {

             result.Success = true;

           
            }
            return result;

        }

        public static OperationResult ValidatePositiveNumber(int number, string errorMessage)
        {

            var result = new OperationResult();

            if (number < 0 && number > 999)
            { 
                result.Success = false ;
                result.Message = errorMessage;
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
