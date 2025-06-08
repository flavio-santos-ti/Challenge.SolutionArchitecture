using Challenge.SolutionArchitecture.LaunchingService.Data;
using Challenge.SolutionArchitecture.LaunchingService.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<Transaction>> GetByDateAsync(DateOnly date)
    {
        return await _context.Transactions
            .Where(t => t.OccurredAt == date)
            .AsNoTracking()
            .ToListAsync();
    }
}
