using Challenge.SolutionArchitecture.ConsolidationService.Models;

namespace Challenge.SolutionArchitecture.ConsolidationService.Services;

public interface IDailyBalanceService
{
    Task<DailyBalance> RegisterAsync(DailyBalance balance);
    Task<DailyBalance?> GetByReferenceDateAsync(DateTime referenceDate);
}
