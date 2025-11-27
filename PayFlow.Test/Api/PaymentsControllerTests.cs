using Microsoft.AspNetCore.Mvc;
using Moq;
using PayFlow.Api.Controllers;
using PayFlow.Application.DTOs;
using PayFlow.Application.UseCases;
using Xunit;

public class PaymentsControllerTests
{
    [Fact]
    public async Task ProcessPayment_DeveRetornarOkComResultado()
    {
        // Arrange
        var fakeRequest = new PaymentRequestDto { Amount = 120.50m, Currency = "BRL" };

        var fakeResponse = new PaymentResultDto
        {
            Id = 1,
            ExternalId = "XPTO",
            Status = "approved",
            Provider = "SecurePay",
            GrossAmount = 120.50m,
            Fee = 4.01m
        };

        var useCaseMock = new Mock<IProcessPaymentUseCase>();
        useCaseMock
            .Setup(u => u.ExecuteAsync(fakeRequest))
            .ReturnsAsync(fakeResponse);

        var controller = new PaymentsController(useCaseMock.Object);

        // Act
        var result = await controller.ProcessPayment(fakeRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDto = Assert.IsType<PaymentResultDto>(okResult.Value);

        Assert.Equal(fakeResponse.ExternalId, returnedDto.ExternalId);
        Assert.Equal(fakeResponse.Provider, returnedDto.Provider);
        Assert.Equal(fakeResponse.Status, returnedDto.Status);
        Assert.Equal(fakeResponse.Fee, returnedDto.Fee);
    }
}
