using Challenge.SolutionArchitecture.ConsolidationService.Models;

namespace Challenge.SolutionArchitecture.ConsolidationService.Repositories;

public interface IDailyBalanceRepository
{
    Task AddAsync(DailyBalance balance);
    Task<DailyBalance?> GetByReferenceDateAsync(DateTime referenceDate);
}
