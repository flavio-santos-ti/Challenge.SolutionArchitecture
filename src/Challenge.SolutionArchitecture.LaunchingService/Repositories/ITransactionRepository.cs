using Challenge.SolutionArchitecture.LaunchingService.Models;

namespace Challenge.SolutionArchitecture.LaunchingService.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transacion);
    Task<Transaction?> GetByIdAsync(Guid id);
}
