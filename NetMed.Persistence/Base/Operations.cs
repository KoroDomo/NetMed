using Microsoft.Extensions.Configuration;
using NetMed.Domain.Base;

public class Operations
{
    public OperationResult SuccessResult(dynamic result,IConfiguration configuration, string configKey = null)
    {
        
        var successMessage = configKey != null
            ? configuration[$"Messages:Success:{configKey}"] ?? configuration["Messages:Success:Default"]
            : configuration["Messages:Success:Default"];

       
        return new OperationResult
        {
            Success = true,
            Result = result,
            Mesagge = successMessage
        };
    }

    public OperationResult HandleException(Exception ex, string configKey, IConfiguration configuration)
    {
        
        var errorMessage = configuration[$"Messages:Error:{configKey}"] ?? configuration["Messages:Error:Default"];

        return new OperationResult
        {
            Success = false,
            Mesagge = $"{errorMessage}: {ex.Message}"
        };
    }
}