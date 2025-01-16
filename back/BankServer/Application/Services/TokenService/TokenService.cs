using BankServer.Application.Mappers;
using BankServer.Domain.DTOS;
using BankServer.Domain.Enums;
using BankServer.Domain.Models;
using BankServer.Domain.Models.Transaction;
using BankServer.Infrastructure.LoggerService;
using BankServer.Infrastructure.RequestProcessor;

namespace BankServer.Application.Services.TokenServices
{
    /// <summary>
    /// service that handle the creation of the token
    /// </summary>

    public class TokenService : ITokenService
    {
        private const string SuccessFetchToken = "Success getting token for user id: ";
        private const string ErrorFetchToken = "error create token for user id: ";

        private readonly ITransactionMapper _transactionMapper;
        private readonly IRequestProcessor _requestProcessor;
        private readonly ILoggerService _loggerService;
        public TokenService(ITransactionMapper transactionMapper, IRequestProcessor requestProcessor, ILoggerService loggerService)
        {
            _transactionMapper = transactionMapper;
            _requestProcessor = requestProcessor;
            _loggerService = loggerService;
        }

        public async Task<Transaction?> CreateToken(MainRequstDTO mainRequstDTO)
        {
            var transaction = _transactionMapper.RequestDtoToTransaction(mainRequstDTO);
            var createTokenRequestDTO = _transactionMapper.TransactionToCreateTokenRequestDto(transaction);
            var responseToken = await _requestProcessor.SendRequestAsync<CreateTokenResponseDTO>(createTokenRequestDTO);
            if (responseToken.Code.Equals((int)ActionStatusType.Success))
            {
                transaction.Token = responseToken.Data;
                _loggerService.LogInfo(SuccessFetchToken + transaction.UserID);
                return transaction;
            }
            else
            {
                _loggerService.LogError(ErrorFetchToken + transaction.UserID);
                return null;
            }
        }
    }
}
