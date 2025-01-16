namespace BankServer.Infrastructure.RequestProcessor
{
    using BankServer.Domain.DTOS;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Service for 3rd party requests
    /// </summary>
    public class RequestProcessor : IRequestProcessor
    {
        private const string SerializeFailExceptionError = "The JSON response is null or empty.";
        private const string JsonTypeHeader = "application/json";

        private readonly HttpClient _httpClient;

        public RequestProcessor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse> SendRequestAsync<TResponse>(IExternalRequestDTO request)
        {
            // Serialize the request
            string jsonRequest = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, JsonTypeHeader);

            // Send the request (adjust the URL as needed)
            HttpResponseMessage response = await _httpClient.PostAsync(request.Url, httpContent);
            response.EnsureSuccessStatusCode();

            // Deserialize the response
            string jsonResponse = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(jsonResponse))
            {
                throw new ArgumentException(SerializeFailExceptionError);
            }

            return JsonSerializer.Deserialize<TResponse>(jsonResponse);
        }
    }
}
