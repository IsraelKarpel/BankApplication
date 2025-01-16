using System.Globalization;

namespace BankServer.Domain.DTOS
{
    /// <summary>
    /// DTO for the response of create action
    /// </summary>
    public class CreateActionResponseDTO
    {
        public required int Code { get; set; }
        public required string Data { get; set; }
    }
}
