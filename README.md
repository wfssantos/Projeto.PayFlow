📘 PayFlow – Arquitetura, Camadas e Fluxo da Aplicação

O PayFlow é um projeto construído seguindo princípios de Clean Architecture, SOLID e DDD (Domain-Driven Design).
O objetivo é fornecer uma estrutura clara, desacoplada e extensível para o processamento de pagamentos.

🏛️ Arquitetura Geral

A solução está organizada em quatro camadas principais:

PayFlow.Api        → Camada de apresentação (controllers)
PayFlow.Application → Casos de uso + DTOs
PayFlow.Domain      → Regras de negócio + Entidades + Interfaces
PayFlow.Infrastructure → Provedores externos e implementações


Cada camada possui responsabilidades bem definidas e comunicação direcionada:

API → Application → Domain

Infrastructure implementa Domain, mas nunca é chamada diretamente pela API.

📂 1. PayFlow.Domain (Domínio)

É o coração do sistema, contendo:

Entidades

Interfaces

Regras de negócio

Contratos de provedores

Exemplos:

Entidades

Payment

PaymentRequest

Interfaces

IPaymentProvider

A camada Domain não conhece nenhuma tecnologia externa.
Não sabe o que é banco, API, controller, nada.
⚠️ Isso garante independência total.

📂 2. PayFlow.Application (Aplicação)

Contém:

Casos de Uso (Use Cases)

DTOs

Regras de orquestração (mas não de domínio)

Exemplo principal:

Use Case

ProcessPaymentUseCase

Responsabilidades:

Validar dados iniciais

Escolher o provedor adequado

Chamar o provedor

Calcular valores de retorno

Mapear resultado para DTO

DTOs

PaymentRequestDto

PaymentResultDto

ProviderResultDto

A camada Application depende apenas de Domain.

📂 3. PayFlow.Infrastructure (Infraestrutura)

Onde ficam:

Implementações concretas de IPaymentProvider

Acesso a APIs externas

Persistência (caso exista)

Integrações reais

Exemplos:

FastPayProvider

SecurePayProvider

Essa camada implementa as interfaces definidas no domínio, mantendo baixo acoplamento.

📂 4. PayFlow.Api (Apresentação – Web API)

Exposição de endpoints HTTP.

Controllers

PaymentsController

Funções:

Receber request JSON

Model binding e validação

Invocar o caso de uso

Devolver PaymentResultDto

A camada API não conhece detalhes de provedores nem lógica de negócios.

🔁 Fluxo de Processamento de Pagamento
[Cliente] → POST /api/payments
     ↓
[PaymentsController]
     ↓
[ProcessPaymentUseCase]
     ↓ seleciona provedor
[IPaymentProvider (FastPay/SecurePay)]
     ↓ envia requisição externa
[PaymentResultDto]
     ↓
[API retorna resultado ao cliente]

🧱 Princípios Utilizados

✔ Clean Architecture
✔ SOLID
✔ DDD (entidades + interfaces)
✔ Injeção de dependência
✔ Segregação por camadas
✔ Facilidade para adicionar novos provedores

➕ Como adicionar um novo provedor de pagamento

Criar uma classe que implemente IPaymentProvider

Implementar:

Name

ProcessAsync

CalculateFee

Registrar no DI (Program.cs)

Ajustar lógica de seleção no ProcessPaymentUseCase

Exemplo:

builder.Services.AddScoped<IPaymentProvider, NewAwesomePayProvider>();

🧪 Testes

O projeto permite testes em dois níveis:

✔ Unitários

Testam o controller isolado usando Moq

Testam o caso de uso usando providers mockados

✔ Integrados

Usando WebApplicationFactory

Validam chamadas reais da API

🐳 Docker (Opcional)

Arquivo Dockerfile para rodar a API em container:

docker build -t payflow-api .
docker run -p 8080:80 payflow-api

🚀 Objetivo do Projeto

Criar um exemplo sólido e limpo de arquitetura moderna em .NET, com foco em:

Escalabilidade

Facilidade de manutenção

Desacoplamento total

Clareza de responsabilidades

Ideal para estudos, entrevistas e projetos reais.
