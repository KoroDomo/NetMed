using NetMed.Domain.Base;

namespace NetMed.Persistence
{
    public static class Persistence
    {
        public static void ifNull(OperationResult result)
        {
            if (result.data == null)
            {
                result.Message = "No se encontraron datos";
                result.Success = false;
            }

            ifNull(result);
        }

 

    }
}

