namespace PayFlow.Domain.Entities
{
    /// <summary>
    /// Representa uma solicitação de pagamento enviada ao provedor.
    /// Contém os dados essenciais necessários para iniciar o
    /// processamento de uma transação financeira.
    /// </summary>
    public class PaymentRequest
    {
        /// <summary>
        /// Valor monetário da transação que será enviado para processamento.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Código da moeda utilizada no pagamento.
        /// O padrão é "BRL" (Real Brasileiro), podendo ser ajustado conforme
        /// a necessidade do provedor ou suporte internacional.
        /// </summary>
        public string Currency { get; set; } = "BRL";
    }
}
