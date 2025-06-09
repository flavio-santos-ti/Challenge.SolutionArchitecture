using Challenge.SolutionArchitecture.ConsolidationService.Configuration;
using Challenge.SolutionArchitecture.ConsolidationService.Http.Dto;
using FDS.NetCore.ApiResponse.Models;
using Microsoft.Extensions.Options;

namespace Challenge.SolutionArchitecture.ConsolidationService.Http.Clients;

public class LaunchingServiceClient : ILaunchingServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly string _getByDateUrl;

    public LaunchingServiceClient(HttpClient httpClient, IOptions<LaunchingServiceOptions> options)
    {
        _httpClient = httpClient;

        var baseUrl = options.Value.BaseUrl.TrimEnd('/');
        var path = options.Value.GetTransactionsByDatePath.TrimStart('/');

        _getByDateUrl = $"{baseUrl}/{path}";
    }

    public async Task<IEnumerable<LaunchingDto>> GetTransactionsByDateAsync(DateOnly date)
    {
        var url = $"{_getByDateUrl}{date:yyyy-MM-dd}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Erro ao chamar {url}:\nStatus: {response.StatusCode}\nBody: {content}");
            return Enumerable.Empty<LaunchingDto>();
        }

        // Usa Response<T> da package FDS.NetCore.ApiResponse
        var apiResponse = await response.Content.ReadFromJsonAsync<ResponseWrapper<IEnumerable<LaunchingDto>>>();

        return apiResponse?.Data ?? Enumerable.Empty<LaunchingDto>();
    }
}
