using PayFlow.Domain.Entities;

namespace PayFlow.Domain.Interfaces
{
    /// <summary>
    /// Define o contrato básico para provedores de pagamento integrados ao sistema PayFlow.
    /// 
    /// Esta interface padroniza as operações necessárias para que qualquer provedor externo
    /// possa ser utilizado na aplicação, independentemente do formato da API externa.
    /// 
    /// Cada implementação deve ser responsável por:
    /// - Montar o payload apropriado para o provedor.
    /// - Enviar a requisição de pagamento.
    /// - Interpretar a resposta retornada.
    /// - Calcular a taxa aplicada para cada transação.
    /// </summary>
    public interface IPaymentProvider
    {
        /// <summary>
        /// Nome oficial do provedor.
        /// 
        /// Utilizado para identificar qual implementação foi acionada durante o processamento
        /// da transação (ex.: "FastPay", "SecurePay").
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Processa a solicitação de pagamento conforme as regras e formato específico do provedor.
        /// 
        /// Este método deve:
        /// - Converter o <see cref="PaymentRequest"/> em um payload adequado para a API externa.
        /// - Enviar a requisição ao provedor.
        /// - Interpretar e padronizar a resposta em um <see cref="PaymentResult"/>.
        /// </summary>
        /// <param name="request">
        /// Objeto contendo as informações essenciais da transação,
        /// como valor, moeda e dados necessários para o provedor.
        /// </param>
        /// <returns>
        /// Um <see cref="PaymentResult"/> representando o resultado do pagamento,
        /// contendo status e ID externo retornado pelo provedor.
        /// </returns>
        Task<PaymentResult> ProcessAsync(PaymentRequest request);

        /// <summary>
        /// Calcula o valor da taxa aplicada pelo provedor para uma transação.
        /// 
        /// Cada provedor possui sua própria fórmula de cálculo, por exemplo:
        /// - FastPay: 3,49% do valor.
        /// - SecurePay: 2,99% + R$ 0,40 fixos.
        /// </summary>
        /// <param name="amount">Valor bruto da transação.</param>
        /// <returns>Valor da taxa calculada.</returns>
        decimal CalculateFee(decimal amount);
    }
}
