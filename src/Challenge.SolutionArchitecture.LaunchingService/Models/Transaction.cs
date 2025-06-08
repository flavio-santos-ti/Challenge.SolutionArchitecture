namespace Challenge.SolutionArchitecture.LaunchingService.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public DateTime OccurredAt { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = null!; // "credit" ou "debit"
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
