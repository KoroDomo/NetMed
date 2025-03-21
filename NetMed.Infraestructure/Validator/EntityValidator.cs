using NetMed.Domain.Base;


namespace NetMed.Infraestructure.Validator
{
    public static class EntityValidator
    {
        public static OperationResult ValidateNotNull<T>(T entity, string errorMessage)
        {
            var result = new OperationResult();


            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad no puede ser nula";
            }
            else
            {

                result.Success = true;


            }
            return result;

        }

        public static OperationResult ValidatePositiveNumber( int number, string errorMessage)
        {

            var result = new OperationResult();

            
                if (number < 0)
                {
                    result.Success = false;
                    result.Message = "El ID proporcionado no es válido";
                }
                else
                {
                    result.Success = true;
                }

            return result;

        }

        public static OperationResult ValidateMultieNumbers(int number1, int number2, string errorMessage)
        {
            var result = new OperationResult { Success = true };

            if (number1 < 0 || number2 < 0)
            {
                result.Success = false;
                result.Message = "El ID proporcionado no es válido";
            }
            else 
            {

              result.Success = true;

            }

                return result;
        }
    }
}


           
