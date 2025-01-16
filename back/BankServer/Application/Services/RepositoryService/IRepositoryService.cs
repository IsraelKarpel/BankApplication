using BankServer.Domain.DTOS;
using BankServer.Domain.Models.Transaction;

namespace BankServer.Application.Services.RepositoryServices
{
    /// <summary>
    /// Service that responsible for handeling the connection with the repository
    /// </summary>
    public interface IRepositoryService
    {
        public MainResponseDTO AddTransactionToRepository(Transaction transaction);
    }
}
