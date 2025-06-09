using Challenge.SolutionArchitecture.ConsolidationService.Models;
using FDS.NetCore.ApiResponse.Models;

namespace Challenge.SolutionArchitecture.ConsolidationService.Services;

public interface IDailyBalanceService
{
    Task<Response<DailyBalance>> AddAsync(DateOnly date);
    Task<Response<DailyBalance?>> GetByReferenceDateAsync(DateOnly date);
}
