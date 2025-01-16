namespace BankServer.Domain.DTOS
{
    /// <summary>
    /// DTO for the reqest to external
    /// </summary>
    public interface IExternalRequestDTO
    {
        string? Url { get; set; }
    }
}
