using Challenge.SolutionArchitecture.ConsolidationService.Models;
using Challenge.SolutionArchitecture.ConsolidationService.Models.Dto;
using Challenge.SolutionArchitecture.ConsolidationService.Services;
using FDS.NetCore.ApiResponse.Models;
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
        var response = await _service.AddAsync(input.ReferenceDate);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{date}")]
    public async Task<IActionResult> GetByReferenceDate(string date)
    {
        if (!DateTime.TryParse(date, out var parsedDate))
            return BadRequest("Data inválida.");
        var referenceDate = DateOnly.FromDateTime(parsedDate);
        var response = await _service.GetByReferenceDateAsync(referenceDate);
        return StatusCode(response.StatusCode, response);
    }
}
