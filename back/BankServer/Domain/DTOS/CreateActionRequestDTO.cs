namespace BankServer.Domain.DTOS
{
    /// <summary>
    /// DTO for the reqest of create action
    /// </summary>
    public class CreateActionRequestDTO: IExternalRequestDTO
    {
        public string? Url { get; set; }
        public double Amount {  get; set; }
        public string? BankAccount { get; set; }
    }
}
