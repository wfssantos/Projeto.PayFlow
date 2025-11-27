using PayFlow.Application.DTOs;
using PayFlow.Domain.Interfaces;

namespace PayFlow.Application.UseCases;


/// <summary>
/// Caso de uso responsável por processar pagamentos dentro da aplicação.
/// Ele orquestra a escolha do provedor adequado, dispara o processamento
/// e retorna um DTO com o resultado consolidado.
/// </summary>
public class ProcessPaymentUseCase : IProcessPaymentUseCase
{
    private readonly IEnumerable<IPaymentProvider> _providers;

    /// <summary>
    /// Construtor do caso de uso, recebendo via injeção de dependência 
    /// a lista de provedores de pagamento registrados no sistema.
    /// </summary>
    /// <param name="providers">
    /// Coleção de implementações de <see cref="IPaymentProvider"/>, 
    /// representando os provedores disponíveis.
    /// </param>
    public ProcessPaymentUseCase(IEnumerable<IPaymentProvider> providers)
    {
        _providers = providers;
    }

    /// <summary>
    /// Executa o fluxo de processamento de um pagamento.
    /// Seleciona o provedor adequado com base no valor, envia a requisição
    /// ao provedor e retorna os dados consolidados do pagamento.
    /// </summary>
    /// <param name="request">Objeto contendo os dados da solicitação de pagamento.</param>
    /// <returns>Um <see cref="PaymentResultDto"/> contendo os detalhes do pagamento processado.</returns>
    public async Task<PaymentResultDto> ExecuteAsync(PaymentRequestDto request)
    {
        // Seleciona automaticamente o provedor com base no valor.
        var provider = SelectProvider(request.Amount);

        // TODO: Utilizar AutoMapper ou similar para mapear DTOs para entidades e vice-versa.
        var response = await provider.ProcessAsync(
            new Domain.Entities.PaymentRequest
            {
                Amount = request.Amount,
                Currency = request.Currency
            }
        );

        // Cria a resposta do caso de uso.
        return new PaymentResultDto
        {
            Id = DateTime.UtcNow.Ticks, // Identificador fictício para exemplo.
            ExternalId = response.ExternalId,
            Status = response.Success ? "approved" : "failed",
            Provider = provider.Name,
            GrossAmount = request.Amount,
            Fee = provider.CalculateFee(request.Amount)
        };
    }

    /// <summary>
    /// Seleciona o provedor de pagamento de acordo com uma regra de negócio simples:
    /// - Valores abaixo de 100 utilizam o provedor "FastPay".
    /// - Valores iguais ou acima de 100 utilizam o provedor "SecurePay".
    /// </summary>
    /// <param name="amount">Valor bruto do pagamento.</param>
    /// <returns>O provedor de pagamento apropriado.</returns>
    private IPaymentProvider SelectProvider(decimal amount)
    {
        if (amount < 100)
            return _providers.First(p => p.Name == "FastPay");

        return _providers.First(p => p.Name == "SecurePay");
    }
}
