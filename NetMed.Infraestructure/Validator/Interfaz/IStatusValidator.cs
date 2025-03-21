
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infrastructure.Validator.Interfaces;

namespace NetMed.Infraestructure.Validator.Interfaz
{
    public interface IStatusValidator : IBaseValidator<Status>
    {

        OperationResult ValidateStatusIsNotNull(Status status, string erroMessage);

        OperationResult ValidateIsStatusIdNotIsNegative(int statusId, string errorMessage);





    }
}
