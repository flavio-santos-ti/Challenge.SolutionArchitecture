namespace Challenge.SolutionArchitecture.LaunchingService.Models.Dto;

public class CreateTransactionDto
{
    public DateOnly OccurredAt { get; set; }

    public decimal Amount { get; set; }

    public required string Type { get; set; }

    public required string Description { get; set; }
}
