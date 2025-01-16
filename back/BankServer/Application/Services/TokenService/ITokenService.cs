using BankServer.Domain.Models;
using BankServer.Domain.Models.Transaction;

namespace BankServer.Application.Services.TokenServices
{
    /// <summary>
    /// service that handle the creation of the token
    /// </summary>
    public interface ITokenService
    {
        public Task<Transaction?> CreateToken(MainRequstDTO mainRequstDTO);
    }
}
