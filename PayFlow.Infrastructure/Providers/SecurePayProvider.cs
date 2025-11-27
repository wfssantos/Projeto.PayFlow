using PayFlow.Domain.Entities;
using PayFlow.Domain.Interfaces;

namespace PayFlow.Infrastructure.Providers
{
    /// <summary>
    /// Implementação do provedor de pagamentos SecurePay.
    ///
    /// Este provedor é responsável por:
    /// - Construir o payload no formato exigido pela SecurePay.
    /// - Processar a requisição de pagamento simulada.
    /// - Calcular a taxa associada ao provedor SecurePay.
    /// - Retornar um <see cref="PaymentResult"/> padronizado pelo domínio.
    ///
    /// Observação:
    /// A chamada HTTP real não está implementada neste exemplo;
    /// o método utiliza <see cref="Task.Delay(int)"/> apenas para simular latência.
    /// </summary>
    public class SecurePayProvider : IPaymentProvider
    {
        /// <summary>
        /// Nome oficial do provedor.
        /// Utilizado pelo sistema PayFlow para identificar qual provedor foi acionado.
        /// </summary>
        public string Name => "SecurePay";

        /// <summary>
        /// Calcula a taxa de processamento da SecurePay.
        ///
        /// Regra:
        /// - 2,99% sobre o valor da transação
        /// - + R$ 0,40 fixo
        /// </summary>
        /// <param name="amount">Valor bruto da transação.</param>
        /// <returns>Valor da taxa calculada, arredondada para 2 casas decimais.</returns>
        public decimal CalculateFee(decimal amount)
            => Math.Round(amount * 0.0299m + 0.40m, 2);

        /// <summary>
        /// Processa o pagamento enviando o payload no formato esperado pelo provedor SecurePay.
        ///
        /// Exemplo de payload que seria enviado:
        /// {
        ///     "amount_cents": 12050,
        ///     "currency_code": "BRL",
        ///     "client_reference": "ORD-20251022"
        /// }
        ///
        /// Este método retorna um <see cref="PaymentResult"/> representando a resposta
        /// padronizada da SecurePay para o PayFlow.
        /// </summary>
        /// <param name="request">Dados da requisição de pagamento enviados pelo domínio.</param>
        /// <returns>Objeto <see cref="PaymentResult"/> contendo o ID externo e o status retornado pelo provedor.</returns>
        public async Task<PaymentResult> ProcessAsync(PaymentRequest request)
        {
            var payload = new
            {
                amount_cents = (int)(request.Amount * 100),
                currency_code = request.Currency,
                client_reference = "ORD-20251022"
            };

            // Simulação de chamada HTTP externa
            await Task.Delay(50);

            return new PaymentResult
            {
                ExternalId = "SP-19283",
                Status = "success"
            };
        }
    }
}
