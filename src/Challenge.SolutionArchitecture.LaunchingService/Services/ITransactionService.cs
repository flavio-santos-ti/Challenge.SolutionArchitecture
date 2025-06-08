using Challenge.SolutionArchitecture.LaunchingService.Models;

namespace Challenge.SolutionArchitecture.LaunchingService.Services;

public interface ITransactionService
{
    Task<Transaction> RegisterAsync(Transaction input); 
    Task<Transaction?> GetByIdAsync(Guid id);
}
