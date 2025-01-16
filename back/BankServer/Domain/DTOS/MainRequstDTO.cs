using BankServer.Domain.Enums;

namespace BankServer.Domain.Models
{
    /// <summary>
    /// DTO for the main request form thw user
    /// </summary>
    public class MainRequstDTO
    {
        public required string TransactionID { get; set; }
        public required string UserID { get; set; }
        public required string BankNumber { get; set; }
        public double Amount { get; set; }
        public required string ActionType { get; set; }
    }
}
