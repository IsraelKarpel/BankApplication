using BankServer.Domain.DTOS;
using BankServer.Domain.Models;

namespace BankServer.Application.Managers
{
    /// <summary>
    /// Interface manager to handle the transactions
    /// </summary>
    public interface ITransactionManager
    {
        Task<MainResponseDTO> ProcessAddTransactionAsync(MainRequstDTO request);
    }
}
