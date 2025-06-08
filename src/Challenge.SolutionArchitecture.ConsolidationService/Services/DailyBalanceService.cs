using Challenge.SolutionArchitecture.ConsolidationService.Models;
using Challenge.SolutionArchitecture.ConsolidationService.Repositories;

namespace Challenge.SolutionArchitecture.ConsolidationService.Services;

public class DailyBalanceService : IDailyBalanceService
{
    private readonly IDailyBalanceRepository _repository;

    public DailyBalanceService(IDailyBalanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<DailyBalance> RegisterAsync(DailyBalance balance)
    {
        balance.Id = Guid.NewGuid();
        balance.GeneratedAt = DateTime.UtcNow;

        await _repository.AddAsync(balance);
        return balance;
    }

    public async Task<DailyBalance?> GetByReferenceDateAsync(DateTime referenceDate)
    {
        return await _repository.GetByReferenceDateAsync(referenceDate);
    }
}
