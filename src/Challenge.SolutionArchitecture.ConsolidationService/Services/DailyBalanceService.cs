using Challenge.SolutionArchitecture.ConsolidationService.Http.Clients;
using Challenge.SolutionArchitecture.ConsolidationService.Models;
using Challenge.SolutionArchitecture.ConsolidationService.Repositories;
using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;

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

    public async Task<Response<DailyBalance>> AddAsync(DateOnly date)
    {
        var existing = await _repository.GetByReferenceDateAsync(date);
        if (existing is not null)
        {
            return Result.CreateValidationError<DailyBalance>("Não é possível consolidar. Já existe um saldo para esta data.");
        }
        
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

        return Result.CreateAdd(balance);
    }

    public async Task<Response<DailyBalance?>> GetByReferenceDateAsync(DateOnly date)
    {
        var dailyBalance = await _repository.GetByReferenceDateAsync(date);

        if (dailyBalance is null)
            return Result.CreateNotFound<DailyBalance?>("Nenhum saldo encontrado para a data informada.");

        return Result.CreateGet<DailyBalance?>(dailyBalance);
    }
}
