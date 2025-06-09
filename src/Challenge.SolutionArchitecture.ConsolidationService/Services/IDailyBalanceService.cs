using Challenge.SolutionArchitecture.ConsolidationService.Models;

namespace Challenge.SolutionArchitecture.ConsolidationService.Services;

public interface IDailyBalanceService
{
    Task<DailyBalance?> AddAsync(DateOnly date);
    Task<DailyBalance?> GetByReferenceDateAsync(DateTime referenceDate);
}
