using Challenge.SolutionArchitecture.ConsolidationService.Http.Clients;
using Challenge.SolutionArchitecture.ConsolidationService.Http.Dto;
using Challenge.SolutionArchitecture.ConsolidationService.Models;
using Challenge.SolutionArchitecture.ConsolidationService.Repositories;
using Challenge.SolutionArchitecture.ConsolidationService.Services;
using Moq;

namespace Challenge.SolutionArchitecture.ConsolidationService.Tests.Services;

public class DailyBalanceServiceTests
{
    private readonly Mock<IDailyBalanceRepository> _repositoryMock;
    private readonly Mock<ILaunchingServiceClient> _launchingClientMock;
    private readonly DailyBalanceService _service;

    public DailyBalanceServiceTests()
    {
        _repositoryMock = new Mock<IDailyBalanceRepository>();
        _launchingClientMock = new Mock<ILaunchingServiceClient>();
        _service = new DailyBalanceService(_repositoryMock.Object, _launchingClientMock.Object);
    }

    [Fact]
    public async Task AddAsync_RetornaErroQuandoSaldoJaExiste()
    {
        // Arrange
        DateOnly dataTeste = DateOnly.FromDateTime(DateTime.Today);
        var saldoExistente = new DailyBalance
        {
            Id = Guid.NewGuid(),
            ReferenceDate = dataTeste,
            TotalCredit = 100,
            TotalDebit = 50,
            Balance = 50,
            GeneratedAt = DateTime.Now
        };

        _repositoryMock.Setup(r => r.GetByReferenceDateAsync(dataTeste))
            .ReturnsAsync(saldoExistente);

        // Act
        var resultado = await _service.AddAsync(dataTeste);

        // Assert
        Assert.False(resultado.IsSuccess);
        Assert.Equal("Não é possível consolidar. Já existe um saldo para esta data.", resultado.Message);
        _repositoryMock.Verify(r => r.GetByReferenceDateAsync(dataTeste), Times.Once);
        // Não deve invocar o client de lançamentos
        _launchingClientMock.Verify(c => c.GetTransactionsByDateAsync(It.IsAny<DateOnly>()), Times.Never);
    }


    [Fact]
    public async Task AddAsync_CalculaESalvaSaldoCorretamenteQuandoNaoExisteSaldo()
    {
        // Arrange
        DateOnly dataTeste = DateOnly.FromDateTime(DateTime.Today);

        // Simula que não existe saldo para a data
        _repositoryMock.Setup(r => r.GetByReferenceDateAsync(dataTeste))
            .ReturnsAsync((DailyBalance?)null);

        // Simula uma lista de transações do tipo LaunchingDto para a data
        var transacoes = new List<LaunchingDto>
        {
            new LaunchingDto { Type = "credit", Amount = 200 },
            new LaunchingDto { Type = "debit", Amount = 50 },
            new LaunchingDto { Type = "credit", Amount = 100 },
            new LaunchingDto { Type = "debit", Amount = 25 }
        };

        _launchingClientMock.Setup(c => c.GetTransactionsByDateAsync(dataTeste))
            .Returns(() => Task.FromResult((IEnumerable<LaunchingDto>)transacoes));

        DailyBalance? saldoAdicionado = null;
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<DailyBalance>()))
            .Callback<DailyBalance>(saldo => saldoAdicionado = saldo)
            .Returns(Task.CompletedTask);

        // Act
        var resultado = await _service.AddAsync(dataTeste);

        // Assert
        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Data);
        Assert.Equal(300, resultado.Data.TotalCredit);
        Assert.Equal(75, resultado.Data.TotalDebit);
        Assert.Equal(225, resultado.Data.Balance);

        _repositoryMock.Verify(r => r.AddAsync(It.Is<DailyBalance>(b =>
            b.TotalCredit == 300 &&
            b.TotalDebit == 75 &&
            b.Balance == 225 &&
            b.ReferenceDate == dataTeste
        )), Times.Once);
    }

    [Fact]
    public async Task GetByReferenceDateAsync_RetornaNotFoundQuandoNaoHaSaldo()
    {
        // Arrange
        DateOnly dataTeste = DateOnly.FromDateTime(DateTime.Today);
        _repositoryMock.Setup(r => r.GetByReferenceDateAsync(dataTeste))
            .ReturnsAsync((DailyBalance?)null);

        // Act
        var resultado = await _service.GetByReferenceDateAsync(dataTeste);

        // Assert
        Assert.False(resultado.IsSuccess);
        Assert.Null(resultado.Data);
        Assert.Equal("Nenhum saldo encontrado para a data informada.", resultado.Message);
    }

    [Fact]
    public async Task GetByReferenceDateAsync_RetornaSaldoQuandoExiste()
    {
        // Arrange
        DateOnly dataTeste = DateOnly.FromDateTime(DateTime.Today);
        var saldoExistente = new DailyBalance
        {
            Id = Guid.NewGuid(),
            ReferenceDate = dataTeste,
            TotalCredit = 150,
            TotalDebit = 60,
            Balance = 90,
            GeneratedAt = DateTime.Now
        };

        _repositoryMock.Setup(r => r.GetByReferenceDateAsync(dataTeste))
            .ReturnsAsync(saldoExistente);

        // Act
        var resultado = await _service.GetByReferenceDateAsync(dataTeste);

        // Assert
        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Data);
        Assert.Equal(saldoExistente, resultado.Data);
    }

    //public class LaunchingDto
    //{
    //    public required string Type { get; set; }
    //    public decimal Amount { get; set; }
    //}
}
