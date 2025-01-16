using BankServer.Common;
using BankServer.Domain.DTOS;
using BankServer.Domain.Enums;
using BankServer.Domain.Models;
using BankServer.Domain.Models.Transaction;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace BankServer.Application.Mappers
{
    /// <summary>
    /// map for convert different types of transactions
    /// </summary>
    public class TransactionMapper: ITransactionMapper
    {
        private const string SuccessTransaction = "went well";
        private const string PermanentToken = "Je45GDf34";

        private readonly UrlSettings _urlSettings;
        public TransactionMapper(IOptions<UrlSettings> urlSettings)
        {
            _urlSettings = urlSettings.Value;
        }
        public Transaction RequestDtoToTransaction(MainRequstDTO request)
        {
            var transaction = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                UserID = request.UserID,
                Amount = request.Amount,
                BankNumber = request.BankNumber,
                dateTime = DateTime.Now,
                ActionType = null
            };
            if (!string.IsNullOrEmpty(request.ActionType) && Enum.TryParse(typeof(ActionType), request.ActionType, true, out var parsedActionType))
            {
                transaction.ActionType = (ActionType)parsedActionType;
            }
            return transaction;
        }
    
        public CreateTokenRequestDTO TransactionToCreateTokenRequestDto(Transaction transaction)
        {
            return new CreateTokenRequestDTO
            {
                UserID = transaction.UserID,
                SecretID = PermanentToken,
                Url = _urlSettings.TokenUrl
            };
        }
    
        public MainResponseDTO CreateFailResponse(string message)
        {
            return new MainResponseDTO
            {
                IsSuccess = false,
                Message = message,
                TransactionsDTO = new List<TransactionDTO>()
            };
        }

        public CreateActionRequestDTO TransactionToCreateActionRequestDTO(Transaction transaction)
        {
            string url = transaction.ActionStatus.Equals(ActionType.Deposit) ?
                _urlSettings.DepositUrl :
               _urlSettings.WithdrawlUrl;
            return new CreateActionRequestDTO
            {
                Amount = transaction.Amount,
                BankAccount = transaction.BankNumber,
                Url = url
            };
        }

        public TransactionDTO TransactionToTransactionDTO(Transaction transaction)
        {
            return new TransactionDTO
            {
                TransactionID = transaction.TransactionID,
                UserID = transaction.UserID,
                Amount = transaction.Amount,
                BankNumber = transaction.BankNumber,
                dateTime = transaction.dateTime,
                ActionType = transaction.ActionType,
                ActionStatus = transaction.ActionStatus
            };
        }
        public MainResponseDTO RepositoryResponseDTOToMainResponse(RepositoryResponseDTO repositoryResponseDTO)
        {
            return new MainResponseDTO
            {
                IsSuccess = true,
                Message = SuccessTransaction,
                TransactionsDTO = repositoryResponseDTO.Data
            };
        }
    }
}
