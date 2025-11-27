using Microsoft.AspNetCore.Mvc.Testing;
using PayFlow.Application.DTOs;
using System.Net.Http.Json;
using Xunit;

public class PaymentsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PaymentsIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ProcessPayment_DeveProcessarEPossuirValoresCalculados()
    {
        // Arrange
        var request = new PaymentRequestDto
        {
            Amount = 120.50m,
            Currency = "BRL"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/payments", request);

        // Assert HTTP
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<PaymentResultDto>();

        Assert.NotNull(result);
        Assert.Equal("SecurePay", result.Provider);
        Assert.Equal(120.50m, result.GrossAmount);

        // A taxa esperada deve ser calculada pelo provider real
        Assert.True(result.Fee > 0);
        Assert.Equal(result.GrossAmount - result.Fee, result.NetAmount);

        Assert.False(string.IsNullOrEmpty(result.ExternalId));
        Assert.True(result.Status == "approved" || result.Status == "success");
    }

    [Fact]
    public async Task ProcessPayment_ComValorMenorQue100_DeveUsarFastPay()
    {
        var request = new PaymentRequestDto
        {
            Amount = 50m,
            Currency = "BRL"
        };

        var response = await _client.PostAsJsonAsync("/api/payments", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<PaymentResultDto>();

        Assert.NotNull(result);
        Assert.Equal("FastPay", result!.Provider);
    }
}
