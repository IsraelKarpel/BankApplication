using BankServer.Application.Mappers;
using BankServer.Domain.DTOS;
using BankServer.Domain.Models.Transaction;
using BankServer.Infrastructure.RepositoryService;

namespace BankServer.Application.Services.RepositoryServices
{
    /// <summary>
    /// Service that responsible for handeling the connection with the repository
    /// </summary>
    public class RepositoryService : IRepositoryService
    {
        private const string FailedGetTransactions = "Failed to fetch transactions history from DB";
        private const string FailSaveInDB = "Failed in save to DB";

        private readonly ITransactionMapper _transactionMapper;
        private readonly IRepository _repository;
        public RepositoryService(ITransactionMapper transactionMapper, IRepository repository)
        {
            _transactionMapper = transactionMapper;
            _repository = repository;
        }

        public MainResponseDTO AddTransactionToRepository(Transaction transaction)
        {
            var transactionDTO = _transactionMapper.TransactionToTransactionDTO(transaction);
            var addRowResponse = _repository.AddRow(transactionDTO);
            if (addRowResponse)
            {
                var repositoryResponseDTO = _repository.GetUserRows(transaction.UserID);
                if (repositoryResponseDTO.IsSuccess)
                {
                    return _transactionMapper.RepositoryResponseDTOToMainResponse(repositoryResponseDTO);
                }
                else
                {
                    return _transactionMapper.CreateFailResponse(FailedGetTransactions);
                }
            }
            else
            {
                return _transactionMapper.CreateFailResponse(FailSaveInDB);
            }
        }
    }
}
