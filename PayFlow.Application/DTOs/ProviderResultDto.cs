namespace PayFlow.Application.DTOs
{
    /// <summary>
    /// DTO utilizado para representar o resultado retornado por um provedor 
    /// de pagamento após o processamento de uma transação.
    /// Contém informações essenciais para identificar a operação e avaliar 
    /// seu sucesso.
    /// </summary>
    public class ProviderResultDto
    {
        /// <summary>
        /// Identificador da transação no provedor externo.
        /// Serve para correlacionar o pagamento interno com o processamento 
        /// realizado por um serviço terceirizado.
        /// </summary>
        public string ExternalId { get; set; } = "";

        /// <summary>
        /// Status da operação retornado pelo provedor.
        /// Exemplos comuns: "approved", "success", "failed".
        /// </summary>
        public string Status { get; set; } = "";

        /// <summary>
        /// Indica se o resultado deve ser considerado como bem-sucedido.
        /// A operação é tratada como sucesso quando o status é 
        /// "approved" ou "success".
        /// </summary>
        public bool Success => Status == "approved" || Status == "success";
    }
}
