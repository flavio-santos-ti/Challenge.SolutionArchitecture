using Challenge.SolutionArchitecture.LaunchingService.Models;
using Challenge.SolutionArchitecture.LaunchingService.Models.Dto;

namespace Challenge.SolutionArchitecture.LaunchingService.Factories;

public static class TransactionFactory
{
    public static Transaction FromDto(CreateTransactionDto dto)
    {
        Transaction transaction = new Transaction();

        transaction.Id = Guid.NewGuid();
        transaction.CreatedAt = DateTime.Now;
        transaction.OccurredAt = dto.OccurredAt;
        transaction.Amount = dto.Amount;
        transaction.Type = dto.Type;
        transaction.Description = dto.Description;

        return transaction;
    }
}
