namespace Challenge.SolutionArchitecture.ConsolidationService.Models;

public class DailyBalance
{
    public Guid Id { get; set; }

    public DateTime ReferenceDate { get; set; }

    public decimal TotalCredit { get; set; }

    public decimal TotalDebit { get; set; }

    public decimal Balance { get; set; }

    public DateTime GeneratedAt { get; set; }
}
