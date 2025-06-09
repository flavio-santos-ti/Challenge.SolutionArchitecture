using Challenge.SolutionArchitecture.ConsolidationService.Models;
using Challenge.SolutionArchitecture.ConsolidationService.Models.Dto;
using Challenge.SolutionArchitecture.ConsolidationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.SolutionArchitecture.ConsolidationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DailyBalancesController : ControllerBase
{
    private readonly IDailyBalanceService _service;

    public DailyBalancesController(IDailyBalanceService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateDailyBalanceDto input)
    {
        var result = await _service.AddAsync(input.ReferenceDate);

        if (result is null)
            return BadRequest("Erro ao consolidar o saldo diário.");

        return CreatedAtAction(
            nameof(GetByReferenceDate),
            new { date = result.ReferenceDate.ToString("yyyy-MM-dd") },
            result
        );
    }

    [HttpGet("{date}")]
    public async Task<IActionResult> GetByReferenceDate(string date)
    {
        if (!DateTime.TryParse(date, out var parsedDate))
            return BadRequest("Data inválida.");

        var result = await _service.GetByReferenceDateAsync(parsedDate);
        return result is null ? NotFound() : Ok(result);
    }
}
