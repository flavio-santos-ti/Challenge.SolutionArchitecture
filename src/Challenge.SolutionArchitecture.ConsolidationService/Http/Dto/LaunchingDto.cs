namespace Challenge.SolutionArchitecture.ConsolidationService.Http.Dto;

public class LaunchingDto
{
    public Guid Id { get; set; }
    public DateOnly OccurredAt { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}