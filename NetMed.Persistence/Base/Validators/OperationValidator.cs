using Microsoft.Extensions.Configuration;
using NetMed.Domain.Base;

public class OperationValidator
{
    private readonly IConfiguration _configuration;

    public OperationValidator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public OperationResult SuccessResult(dynamic result, string configKey = null)
    {
        var successMessage = _configuration[$"Messages:Success:{configKey}"] ;

        return new OperationResult
        {
            Success = true,
            Result = result,
            Message = successMessage
        };
    }

    public OperationResult HandleException(Exception ex, string configKey)
    {
        var errorMessage = _configuration[$"Messages:Error:{configKey}"];

        return new OperationResult
        {
            Success = false,
            Message = $"{errorMessage}: {ex.Message}"
        };
    }

}