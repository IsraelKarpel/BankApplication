using BankServer.Domain.Enums;

namespace BankServer.Domain.DTOS
{
    /// <summary>
    /// DTO for the transactiony
    /// </summary>
    public class TransactionDTO
    {
        public Guid? TransactionID { get; set; }
        public string? UserID { get; set; }
        public double Amount { get; set; }
        public string? BankNumber { get; set; }
        public DateTime? dateTime { get; set; }
        public ActionType? ActionType { get; set; }
        public ActionStatusType? ActionStatus { get; set; }
    }
}
