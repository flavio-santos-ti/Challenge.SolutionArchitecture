using Challenge.SolutionArchitecture.LaunchingService.Models;
using Challenge.SolutionArchitecture.LaunchingService.Models.Dto;
using Challenge.SolutionArchitecture.LaunchingService.Services;
using FDS.NetCore.ApiResponse.Models;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.SolutionArchitecture.LaunchingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(CreateTransactionDto input)
    {
        var response = await _service.AddAsync(input);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetByDate([FromQuery] DateOnly date)
    {
        var transactions = await _service.GetByDateAsync(date);
        return StatusCode(transactions.StatusCode, transactions);
    }

}
