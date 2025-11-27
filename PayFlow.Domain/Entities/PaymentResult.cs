namespace PayFlow.Domain.Entities;

public class PaymentResult
{
    /// <summary>
    /// Identificador retornado pelo provedor externo de pagamento.
    /// Pode representar o ID da transação, operação ou pedido gerado
    /// pelo gateway utilizado.
    /// </summary>
    public string ExternalId { get; set; } = "";

    /// <summary>
    /// Status da operação de pagamento retornado pelo provedor.
    /// Exemplos comuns: "approved", "success", "pending", "failed".
    /// </summary>
    public string Status { get; set; } = "";

    /// <summary>
    /// Indica se a operação foi concluída com sucesso.
    /// O pagamento é considerado bem-sucedido quando o status é
    /// "approved" ou "success".
    /// </summary>
    public bool Success => Status == "approved" || Status == "success";
}
