using Challenge.SolutionArchitecture.LaunchingService.Models;
using Challenge.SolutionArchitecture.LaunchingService.Models.Dto;
using FDS.NetCore.ApiResponse.Models;

namespace Challenge.SolutionArchitecture.LaunchingService.Services;

public interface ITransactionService
{
    Task<Response<Transaction>> AddAsync(CreateTransactionDto input);
    Task<Response<IEnumerable<Transaction>>> GetByDateAsync(DateOnly date);
}
