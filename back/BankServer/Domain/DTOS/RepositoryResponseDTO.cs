namespace BankServer.Domain.DTOS
{
    /// <summary>
    /// DTO for the response from the repository
    /// </summary>
    public class RepositoryResponseDTO
    {
        public bool IsSuccess { get; set; }
        public required string ErrorMessage { get; set; }
        public required IEnumerable<TransactionDTO> Data { get; set; }
    }
}
