using BankServer.Application.Mappers;
using BankServer.Domain.DTOS;
using BankServer.Domain.Models.Transaction;
using BankServer.Infrastructure.RequestProcessor;

namespace BankServer.Application.Services.ActionServices
{
    /// <summary>
    /// Service that handles the different actions
    /// </summary>
    public class ActionService : IActionService
    {
        private readonly ITransactionMapper _transactionMapper;
        private readonly IRequestProcessor _requestProcessor;
        public ActionService(ITransactionMapper transactionMapper, IRequestProcessor requestProcessor)
        {
            _transactionMapper = transactionMapper;
            _requestProcessor = requestProcessor;
        }

        public async Task<CreateActionResponseDTO> DoAction(Transaction transaction)
        {
            var createActionRequestDto = _transactionMapper.TransactionToCreateActionRequestDTO(transaction);
            return await _requestProcessor.SendRequestAsync<CreateActionResponseDTO>(createActionRequestDto);
        }
    }
}
