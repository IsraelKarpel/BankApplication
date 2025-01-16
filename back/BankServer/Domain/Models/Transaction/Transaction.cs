using BankServer.Domain.Enums;

namespace BankServer.Domain.Models.Transaction
{
    /// <summary>
    /// Model for the Transaction
    /// </summary>
    public class Transaction : ITransaction
    {
        public Guid TransactionID { get; set; }
        public required string UserID { get; set; }
        public string? Token { get; set; }
        public double Amount { get; set; }
        public string? BankNumber { get; set; }
        public DateTime dateTime { get; set; }
        public ActionType? ActionType { get; set; }
        public ActionStatusType ActionStatus { get; set; }
    }
}
