using PayFlow.Application.DTOs;

namespace PayFlow.Application.UseCases;

public interface IProcessPaymentUseCase
{
    Task<PaymentResultDto> ExecuteAsync(PaymentRequestDto request);
}
