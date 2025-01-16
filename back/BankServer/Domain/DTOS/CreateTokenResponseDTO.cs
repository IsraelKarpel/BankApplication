using System.Globalization;

namespace BankServer.Domain.DTOS
{
    /// <summary>
    /// DTO for the reqest of create token
    /// </summary>
    public class CreateTokenResponseDTO
    {
        public required int Code { get; set; }
        public required string Data { get; set; }
    }
}
