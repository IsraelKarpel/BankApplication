using BankServer.Domain.DTOS;
using BankServer.Domain.Models.Transaction;

namespace BankServer.Application.Services.ActionServices
{
    /// <summary>
    /// Service that handles the different actions
    /// </summary>
    public interface IActionService
    {
        public Task<CreateActionResponseDTO> DoAction(Transaction transaction);
    }
}
