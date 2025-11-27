namespace PayFlow.Application.DTOs
{
    /// <summary>
    /// DTO responsável por transportar o resultado final do processamento 
    /// de um pagamento para camadas externas, como a API.
    /// Este objeto agrega informações calculadas a partir da operação,
    /// incluindo valores financeiros, status e dados do provedor utilizado.
    /// </summary>
    public class PaymentResultDto
    {
        /// <summary>
        /// Identificador interno do pagamento, gerado pela aplicação.
        /// Usado para controle e rastreamento da transação no sistema.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador retornado pelo provedor externo, permitindo 
        /// correlacionar a transação local com a execução no serviço terceiro.
        /// </summary>
        public string ExternalId { get; set; } = "";

        /// <summary>
        /// Status final da operação.
        /// Exemplos: "approved", "failed", "success".
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary>
        /// Nome do provedor responsável pelo processamento do pagamento.
        /// Ex.: "FastPay", "SecurePay".
        /// </summary>
        public string Provider { get; set; } = "";

        /// <summary>
        /// Valor bruto enviado para processamento antes da aplicação das taxas.
        /// </summary>
        public decimal GrossAmount { get; set; }

        /// <summary>
        /// Valor da taxa cobrada pelo provedor durante o processamento.
        /// O cálculo depende das regras específicas de cada integração.
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Valor líquido recebido após o desconto das taxas.
        /// Calculado automaticamente como GrossAmount - Fee.
        /// </summary>
        public decimal NetAmount => GrossAmount - Fee;
    }
}
