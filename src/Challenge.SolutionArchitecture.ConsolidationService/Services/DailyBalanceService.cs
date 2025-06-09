using Challenge.SolutionArchitecture.ConsolidationService.Http.Clients;
using Challenge.SolutionArchitecture.ConsolidationService.Models;
using Challenge.SolutionArchitecture.ConsolidationService.Repositories;

namespace Challenge.SolutionArchitecture.ConsolidationService.Services;

public class DailyBalanceService : IDailyBalanceService
{
    private readonly IDailyBalanceRepository _repository;
    private readonly ILaunchingServiceClient _launchingClient;

    public DailyBalanceService(IDailyBalanceRepository repository, ILaunchingServiceClient launchingClient)
    {
        _repository = repository;
        _launchingClient = launchingClient;
    }

    public async Task<DailyBalance?> AddAsync(DateOnly date)
    {
        var transactions = await _launchingClient.GetTransactionsByDateAsync(date);

        var totalCredit = transactions
            .Where(t => t.Type.Equals("credit", StringComparison.OrdinalIgnoreCase))
            .Sum(t => t.Amount);

        var totalDebit = transactions
            .Where(t => t.Type.Equals("debit", StringComparison.OrdinalIgnoreCase))
            .Sum(t => t.Amount);

        var balance = new DailyBalance
        {
            Id = Guid.NewGuid(),
            ReferenceDate = date,
            TotalCredit = totalCredit,
            TotalDebit = totalDebit,
            Balance = totalCredit - totalDebit,
            GeneratedAt = DateTime.Now
        };

        await _repository.AddAsync(balance);
        return balance;
    }

    public async Task<DailyBalance?> GetByReferenceDateAsync(DateTime date)
    {
        return await _repository.GetByReferenceDateAsync(date);
    }
}
