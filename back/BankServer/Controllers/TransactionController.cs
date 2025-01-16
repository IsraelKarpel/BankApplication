using BankServer.Application.Managers;
using BankServer.Domain.DTOS;
using BankServer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionManager _transactionManager;
        public TransactionController(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }
        [HttpPost("process")]
        public async Task<MainResponseDTO> ProcessAddTransaction([FromBody] MainRequstDTO request)
        {
                var response = await _transactionManager.ProcessAddTransactionAsync(request);
                return response;
        }
    }
}
