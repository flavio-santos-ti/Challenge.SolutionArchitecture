using Challenge.SolutionArchitecture.LaunchingService.Data;
using Challenge.SolutionArchitecture.LaunchingService.Models;

namespace Challenge.SolutionArchitecture.LaunchingService.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly LaunchingDbContext _context;

    public TransactionRepository(LaunchingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await _context.Transactions.FindAsync(id);
    }
}
