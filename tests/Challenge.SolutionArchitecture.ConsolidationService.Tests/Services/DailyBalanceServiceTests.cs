using Challenge.SolutionArchitecture.ConsolidationService.Models;
using Challenge.SolutionArchitecture.ConsolidationService.Repositories;
using Challenge.SolutionArchitecture.ConsolidationService.Services;
using Moq;

namespace Challenge.SolutionArchitecture.ConsolidationService.Tests.Services;

public class DailyBalanceServiceTests
{
    [Fact(DisplayName = "Registrar saldo com crédito maior que débito")]
    public async Task RegistrarAsync_LancamentoCredor_DeveRegistrarCorretamente()
    {
        // Arrange
        var mockRepo = new Mock<IDailyBalanceRepository>();
        var service = new DailyBalanceService(mockRepo.Object);

        var entrada = new DailyBalance
        {
            ReferenceDate = new DateTime(2024, 6, 3),
            TotalCredit = 300,
            TotalDebit = 100,
            Balance = 200
        };

        // Act
        var resultado = await service.RegisterAsync(entrada);

        // Assert
        Assert.NotEqual(Guid.Empty, resultado.Id);
        Assert.Equal(200, resultado.Balance);
        Assert.Equal(300, resultado.TotalCredit);
        Assert.Equal(100, resultado.TotalDebit);

        mockRepo.Verify(r => r.AddAsync(It.IsAny<DailyBalance>()), Times.Once);
    }

    [Fact(DisplayName = "Registrar saldo com débito maior que crédito")]
    public async Task RegistrarAsync_LancamentoDevedor_DeveRegistrarCorretamente()
    {
        // Arrange
        var mockRepo = new Mock<IDailyBalanceRepository>();
        var service = new DailyBalanceService(mockRepo.Object);

        var entrada = new DailyBalance
        {
            ReferenceDate = new DateTime(2024, 6, 3),
            TotalCredit = 100,
            TotalDebit = 150,
            Balance = -50
        };

        // Act
        var resultado = await service.RegisterAsync(entrada);

        // Assert
        Assert.NotEqual(Guid.Empty, resultado.Id);
        Assert.Equal(-50, resultado.Balance);
        Assert.Equal(100, resultado.TotalCredit);
        Assert.Equal(150, resultado.TotalDebit);

        mockRepo.Verify(r => r.AddAsync(It.IsAny<DailyBalance>()), Times.Once);
    }
}
