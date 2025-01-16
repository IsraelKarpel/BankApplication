using BankServer.Domain.Enums;

namespace BankServer.Domain.Models.Transaction
{
    /// <summary>
    /// Model for the Transaction
    /// </summary>
    public interface ITransaction
    {
        Guid TransactionID { get; set; }
        string UserID { get; set; }
        string? Token { get; set; }
        double Amount { get; set; }
        string? BankNumber { get; set; }
        DateTime dateTime { get; set; }
        ActionType? ActionType { get; set; }
        ActionStatusType ActionStatus { get; set; }
    }
}
