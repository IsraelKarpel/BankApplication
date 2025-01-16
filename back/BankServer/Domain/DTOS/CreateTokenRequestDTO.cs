namespace BankServer.Domain.DTOS
{
    /// <summary>
    /// DTO for the reqest of create token
    /// </summary>
    public class CreateTokenRequestDTO: IExternalRequestDTO
    {
        public string? Url { get; set; }
        public string? UserID { get; set; }
        public string? SecretID {  get; set; }
    }
}
