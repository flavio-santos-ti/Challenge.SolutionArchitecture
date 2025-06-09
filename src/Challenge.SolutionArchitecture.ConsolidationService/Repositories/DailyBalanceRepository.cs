using Challenge.SolutionArchitecture.ConsolidationService.Data;
using Challenge.SolutionArchitecture.ConsolidationService.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.SolutionArchitecture.ConsolidationService.Repositories;

public class DailyBalanceRepository : IDailyBalanceRepository
{
    private readonly ConsolidationDbContext _context;

    public DailyBalanceRepository(ConsolidationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(DailyBalance balance)
    {
        await _context.DailyBalances.AddAsync(balance);
        await _context.SaveChangesAsync();
    }

    public async Task<DailyBalance?> GetByReferenceDateAsync(DateTime date)
    {
        return await _context.DailyBalances
            .FirstOrDefaultAsync(b => b.ReferenceDate == DateOnly.FromDateTime(date));
    }
}
