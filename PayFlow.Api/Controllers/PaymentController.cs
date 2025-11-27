using Microsoft.AspNetCore.Mvc;
using PayFlow.Application.DTOs;
using PayFlow.Application.UseCases;

namespace PayFlow.Api.Controllers
{
    /// <summary>
    /// Controlador responsável por expor os endpoints de processamento
    /// de pagamentos. Atua como ponto de entrada da API e direciona
    /// as requisições para o caso de uso apropriado.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IProcessPaymentUseCase _useCase;

        /// <summary>
        /// Construtor do controlador, recebendo via injeção de dependência
        /// o caso de uso responsável por processar pagamentos.
        /// </summary>
        /// <param name="useCase">Caso de uso de processamento de pagamento.</param>
        public PaymentsController(IProcessPaymentUseCase useCase)
        {
            _useCase = useCase;
        }

        /// <summary>
        /// Endpoint responsável por processar uma solicitação de pagamento.
        /// Recebe os dados da requisição, executa o caso de uso e devolve
        /// como resposta o resultado final do processamento.
        /// </summary>
        /// <param name="request">Dados enviados pelo cliente contendo o valor e a moeda.</param>
        /// <returns>Objeto contendo o resultado do processamento: status, valores e provedor utilizado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PaymentResultDto), 200)]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDto request)
        {
            var result = await _useCase.ExecuteAsync(request);
            return Ok(result);
        }
    }
}
