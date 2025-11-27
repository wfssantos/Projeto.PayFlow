namespace PayFlow.Domain.Entities
{
    /// <summary>
    /// Representa a entidade principal de um pagamento dentro do domínio.
    /// Contém informações resultantes do processamento, como valores e 
    /// identificadores internos e externos. É utilizada para registrar e 
    /// acompanhar o estado de uma transação de pagamento.
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Identificador interno do pagamento no sistema.
        /// Utilizado para persistência e rastreamento interno.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador retornado pelo provedor externo de pagamento.
        /// Permite correlacionar a transação local com a transação processada
        /// pelo serviço terceiro.
        /// </summary>
        public string ExternalId { get; set; } = "";

        /// <summary>
        /// Status atual do pagamento (ex.: "pending", "success", "approved", "failed").
        /// Pode variar conforme o provedor utilizado e conforme o fluxo de processamento.
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary>
        /// Nome do provedor responsável pelo processamento da transação.
        /// Ex.: "SecurePay", "PixProvider", "CreditCardProvider".
        /// </summary>
        public string Provider { get; set; } = "";

        /// <summary>
        /// Valor bruto enviado para processamento antes da aplicação de taxas.
        /// </summary>
        public decimal GrossAmount { get; set; }

        /// <summary>
        /// Valor cobrado de taxa pelo provedor durante o processamento.
        /// O cálculo pode variar conforme a implementação de cada provedor.
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Valor líquido recebido após o desconto das taxas.
        /// Calculado automaticamente como GrossAmount - Fee.
        /// </summary>
        public decimal NetAmount => GrossAmount - Fee;
    }
}
