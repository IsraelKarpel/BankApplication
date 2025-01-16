using BankServer.Domain.DTOS;

namespace BankServer.Infrastructure.RepositoryService
{
    /// <summary>
    /// repository for connections with the DB
    /// </summary>
    public interface IRepository
    {
        bool AddRow(TransactionDTO transactionDTO);
        RepositoryResponseDTO GetUserRows(string userID);
        public RepositoryResponseDTO DeleteRows(Guid transactionID);
    }
}
