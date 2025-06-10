using Challenge.SolutionArchitecture.LaunchingService.Factories;
using Challenge.SolutionArchitecture.LaunchingService.Models;
using Challenge.SolutionArchitecture.LaunchingService.Models.Dto;
using Challenge.SolutionArchitecture.LaunchingService.Repositories;
using Challenge.SolutionArchitecture.LaunchingService.Services;
using Moq;

namespace Challenge.SolutionArchitecture.LaunchingService.Tests.Services;

public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _repositoryMock;
    private readonly TransactionService _service;

    public TransactionServiceTests()
    {
        _repositoryMock = new Mock<ITransactionRepository>();
        _service = new TransactionService(_repositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_DeveCriarTransacaoERetornarRespostaCorreta()
    {
        // Arrange
        var input = new CreateTransactionDto
        {
            Type = "credit",
            Amount = 100.0m,
            Description = "Descrição da transação"
        };

        Transaction? transactionCriada = null;
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Transaction>()))
            .Callback<Transaction>(t => transactionCriada = t)
            .Returns(Task.CompletedTask);

        // Act
        var response = await _service.AddAsync(input);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Data);
        // Verifica se a transação retornada é a mesma que foi passada para o repositório.
        Assert.Equal(transactionCriada, response.Data);

        // Verifica se as propriedades foram mapeadas corretamente.
        Assert.Equal(input.Type, response.Data.Type);
        Assert.Equal(input.Amount, response.Data.Amount);
        Assert.Equal(input.Description, response.Data.Description);

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Once);
    }

    [Fact]
    public async Task GetByDateAsync_DeveRetornarTransacoesParaDataInformada()
    {
        // Arrange
        DateOnly dataTeste = DateOnly.FromDateTime(DateTime.Today);
        var transactions = new List<Transaction>
        {
            new Transaction
            {
                Id = Guid.NewGuid(),
                Type = "credit",
                Amount = 200,
                OccurredAt = dataTeste,
                Description = "Transação de crédito",
                CreatedAt = DateTime.Now
            },
            new Transaction
            {
                Id = Guid.NewGuid(),
                Type = "debit",
                Amount = 50,
                OccurredAt = dataTeste,
                Description = "Transação de débito",
                CreatedAt = DateTime.Now
            }
        };

        _repositoryMock.Setup(r => r.GetByDateAsync(dataTeste))
            .ReturnsAsync(transactions);

        // Act
        var response = await _service.GetByDateAsync(dataTeste);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Data);
        Assert.Equal(transactions.Count, response.Data.Count());
        foreach (var transaction in transactions)
        {
            Assert.Contains(response.Data, t => t.Id == transaction.Id);
        }
    }
}
