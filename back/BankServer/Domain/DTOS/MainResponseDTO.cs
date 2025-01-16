using BankServer.Domain.Enums;

namespace BankServer.Domain.DTOS
{
    /// <summary>
    /// DTO for the main response to the user
    /// </summary>
    public class MainResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public IEnumerable<TransactionDTO>? TransactionsDTO { get; set; }
    }
}
