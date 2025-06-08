using Challenge.SolutionArchitecture.LaunchingService.Models;
using Challenge.SolutionArchitecture.LaunchingService.Repositories;

namespace Challenge.SolutionArchitecture.LaunchingService.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Transaction> RegisterAsync(Transaction input)
    {
        input.Id = Guid.NewGuid();
        input.CreatedAt = DateTime.UtcNow;

        await _repository.AddAsync(input);
        return input;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
