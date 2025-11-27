using PayFlow.Domain.Entities;
using PayFlow.Domain.Interfaces;

namespace PayFlow.Infrastructure.Providers
{
    /// <summary>
    /// Implementação do provedor de pagamentos FastPay.
    /// 
    /// Este provedor é responsável por:
    /// - Montar o payload no formato exigido pela FastPay.
    /// - Enviar a requisição de pagamento ao serviço externo.
    /// - Calcular a taxa específica da FastPay.
    /// - Retornar um <see cref="PaymentResult"/> contendo a resposta padronizada.
    /// 
    /// Observação:
    /// A integração real com HTTP não foi implementada neste exemplo.
    /// A chamada é simulada com <see cref="Task.Delay(int)"/>.
    /// </summary>
    public class FastPayProvider : IPaymentProvider
    {
        /// <summary>
        /// Nome oficial do provedor.
        /// Utilizado para identificação interna no PayFlow.
        /// </summary>
        public string Name => "FastPay";

        /// <summary>
        /// Calcula a taxa de processamento da FastPay.
        /// 
        /// Regra:
        /// - 3,49% sobre o valor da transação.
        /// </summary>
        /// <param name="amount">Valor bruto da transação.</param>
        /// <returns>Valor da taxa calculado e arredondado para 2 casas decimais.</returns>
        public decimal CalculateFee(decimal amount)
            => Math.Round(amount * 0.0349m, 2);

        /// <summary>
        /// Processa a requisição de pagamento enviando um payload no formato requerido pela FastPay.
        ///
        /// Exemplo de payload enviado:
        /// {
        ///     "transaction_amount": 120.50,
        ///     "currency": "BRL",
        ///     "payer": { "email": "cliente@teste.com" },
        ///     "installments": 1,
        ///     "description": "Compra via FastPay"
        /// }
        ///
        /// A resposta retornada é convertida em um <see cref="PaymentResult"/> padronizado pelo domínio.
        /// </summary>
        /// <param name="request">Dados da requisição de pagamento enviados pelo PayFlow.</param>
        /// <returns>Objeto <see cref="PaymentResult"/> contendo ID externo e status.</returns>
        public async Task<PaymentResult> ProcessAsync(PaymentRequest request)
        {
            var payload = new
            {
                transaction_amount = request.Amount,
                currency = request.Currency,
                payer = new { email = "cliente@teste.com" },
                installments = 1,
                description = "Compra via FastPay"
            };

            // Simulação de chamada HTTP externa
            await Task.Delay(50);

            return new PaymentResult
            {
                ExternalId = "FP-884512",
                Status = "approved"
            };
        }
    }
}
