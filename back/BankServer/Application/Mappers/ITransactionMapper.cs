using BankServer.Domain.DTOS;
using BankServer.Domain.Models;
using BankServer.Domain.Models.Transaction;

namespace BankServer.Application.Mappers
{
    /// <summary>
    /// map for convert different types of transactions
    /// </summary>
    public interface ITransactionMapper
    {
        public Transaction RequestDtoToTransaction(MainRequstDTO request);
        public CreateTokenRequestDTO TransactionToCreateTokenRequestDto(Transaction transaction);
        public MainResponseDTO CreateFailResponse(string message);
        public CreateActionRequestDTO TransactionToCreateActionRequestDTO(Transaction transaction);
        public TransactionDTO TransactionToTransactionDTO(Transaction transaction);
        public MainResponseDTO RepositoryResponseDTOToMainResponse(RepositoryResponseDTO repositoryResponseDTO);
    }
}
