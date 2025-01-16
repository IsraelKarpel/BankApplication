using BankServer.Application.Mappers;
using BankServer.Application.Services.ActionServices;
using BankServer.Application.Services.RepositoryServices;
using BankServer.Application.Services.TokenServices;
using BankServer.Application.Services.ValidationServices;
using BankServer.Domain.DTOS;
using BankServer.Domain.Enums;
using BankServer.Domain.Models;

namespace BankServer.Application.Managers
{
    /// <summary>
    ///  Manager to handle the Transactions
    /// </summary>
    public class TransactionManager : ITransactionManager
    {
        private const string CreateTokenFailed = "Failed in create Token";

        private readonly IValidationService _validationService;
        private readonly ITransactionMapper _transactionMapper;
        private readonly ITokenService _tokenService;
        private readonly IRepositoryService _repositoryService;
        private readonly IActionService _actionService;

        public TransactionManager(IValidationService validationService,              
                                  ITransactionMapper transactionMapper,
                                  ITokenService tokenService,
                                  IRepositoryService repositoryService,
                                  IActionService actionService)
        {
            _validationService = validationService;
            _transactionMapper = transactionMapper;
            _tokenService = tokenService;
            _repositoryService = repositoryService;
            _actionService = actionService;
        }

        public async Task<MainResponseDTO> ProcessAddTransactionAsync(MainRequstDTO request)
        {
            _validationService.ValidateRequest(request);
            var transaction = await _tokenService.CreateToken(request);
            if (transaction is null)
            {
                return _transactionMapper.CreateFailResponse(CreateTokenFailed);
            }
            var responseAction = await _actionService.DoAction(transaction);
            if (responseAction.Code.Equals((int)ActionStatusType.Success))
            {
                return _repositoryService.AddTransactionToRepository(transaction);
            }
            else
            {
                return _transactionMapper.CreateFailResponse(CreateTokenFailed);
            }
        }
    }
}
