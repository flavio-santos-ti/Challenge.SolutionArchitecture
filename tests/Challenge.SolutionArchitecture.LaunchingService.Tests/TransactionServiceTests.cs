using Challenge.SolutionArchitecture.LaunchingService.Models;
using Challenge.SolutionArchitecture.LaunchingService.Repositories;
using Challenge.SolutionArchitecture.LaunchingService.Services;
using Moq;

namespace Challenge.SolutionArchitecture.LaunchingService.Tests;

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
    public async Task RegisterAsync_DevePersistirTransacaoDeCredito()
    {
        var transacao = new Transaction
        {
            OccurredAt = DateTime.UtcNow,
            Amount = 500.00m,
            Type = "credit",
            Description = "Recebimento de valor"
        };

        var resultado = await _service.RegisterAsync(transacao);

        Assert.Equal("credit", resultado.Type);
        Assert.Equal(500.00m, resultado.Amount);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_DevePersistirTransacaoDeDebito()
    {
        var transacao = new Transaction
        {
            OccurredAt = DateTime.UtcNow,
            Amount = 200.00m,
            Type = "debit",
            Description = "Pagamento de conta"
        };

        var resultado = await _service.RegisterAsync(transacao);

        Assert.Equal("debit", resultado.Type);
        Assert.Equal(200.00m, resultado.Amount);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Once);
    }


    [Fact]
    public async Task GetByIdAsync_DeveRetornarTransacaoQuandoExiste()
    {
        // Arrange
        var fakeId = Guid.NewGuid();
        var expected = new Transaction { Id = fakeId };

        _repositoryMock.Setup(r => r.GetByIdAsync(fakeId)).ReturnsAsync(expected);

        // Act
        var result = await _service.GetByIdAsync(fakeId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(fakeId, result?.Id);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarNuloQuandoTransacaoNaoExiste()
    {
        // Arrange
        var fakeId = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(fakeId)).ReturnsAsync((Transaction?)null);

        // Act
        var result = await _service.GetByIdAsync(fakeId);

        // Assert
        Assert.Null(result);
    }
}
