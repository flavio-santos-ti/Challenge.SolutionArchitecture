using Challenge.SolutionArchitecture.ConsolidationService.Http.Dto;

namespace Challenge.SolutionArchitecture.ConsolidationService.Http.Clients;

public interface ILaunchingServiceClient
{
    Task<IEnumerable<LaunchingDto>> GetTransactionsByDateAsync(DateOnly date);
}
