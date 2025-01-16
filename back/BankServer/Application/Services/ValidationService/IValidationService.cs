using BankServer.Domain.Models;

namespace BankServer.Application.Services.ValidationServices
{
    /// <summary>
    /// Service that responsible fro validation of the request
    /// </summary>
    public interface IValidationService
    {
        void ValidateRequest(MainRequstDTO request);
    }
}
