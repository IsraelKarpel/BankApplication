namespace BankServer.Common
{
    /// <summary>
    /// Settings of the URL
    /// </summary>
    public class UrlSettings
    {
        public required string TokenUrl { get; set; }
        public required string DepositUrl { get; set; }
        public required string WithdrawlUrl { get; set; }
    }
}
