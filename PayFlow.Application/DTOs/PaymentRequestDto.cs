namespace PayFlow.Application.DTOs
{
    /// <summary>
    /// DTO utilizado para representar os dados necessários para iniciar
    /// o processamento de um pagamento. Este objeto é recebido pelas camadas
    /// superiores, como a API, e repassado para o caso de uso.
    /// </summary>
    public class PaymentRequestDto
    {
        /// <summary>
        /// Valor bruto da transação que será processada.
        /// Este valor será utilizado para determinar o provedor adequado
        /// e calcular as taxas correspondentes.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Código da moeda utilizada na transação.
        /// Por padrão, utiliza "BRL", mas pode ser ampliado para suportar 
        /// múltiplas moedas.
        /// </summary>
        public string Currency { get; set; } = "BRL";
    }
}
