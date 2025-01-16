using BankServer.Domain.DTOS;

namespace BankServer.Infrastructure.RequestProcessor
{
    /// <summary>
    /// Service for 3rd party requests
    /// </summary>
    public interface IRequestProcessor
    {
        Task<TResponse> SendRequestAsync<TResponse>(IExternalRequestDTO request);
    }
}
