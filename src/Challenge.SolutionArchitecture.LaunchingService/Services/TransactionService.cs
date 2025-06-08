using Challenge.SolutionArchitecture.LaunchingService.Factories;
using Challenge.SolutionArchitecture.LaunchingService.Models;
using Challenge.SolutionArchitecture.LaunchingService.Models.Dto;
using Challenge.SolutionArchitecture.LaunchingService.Repositories;
using FDS.NetCore.ApiResponse.Models;
using FDS.NetCore.ApiResponse.Results;

namespace Challenge.SolutionArchitecture.LaunchingService.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<Transaction>> AddAsync(CreateTransactionDto input)
    {
        var transaction = TransactionFactory.FromDto(input);
        await _repository.AddAsync(transaction);
        return Result.CreateAdd(transaction);
    }

    public async Task<Response<IEnumerable<Transaction>>> GetByDateAsync(DateOnly date)
    {
        return Result.CreateGet<IEnumerable<Transaction>>(await _repository.GetByDateAsync(date));
    }

}
