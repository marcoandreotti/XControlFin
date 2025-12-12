# 🚀 XControlFin

Sistema backend desenvolvido em **.NET 10 C#**, com foco em **gestão financeira**, escalabilidade e modularização.  
O projeto adota práticas modernas de arquitetura e boas práticas de desenvolvimento para garantir **alta performance** e **baixa complexidade de dependências**.

---

## 🧠 Visão Geral

- **DDD (Domain-Driven Design)** → Modelagem orientada ao domínio  
- **CQRS (Command Query Responsibility Segregation)** → Separação clara entre leitura e escrita  
- **Clean Code & SOLID** → Código limpo, sustentável e extensível  
- **Execução via Docker** → Fácil orquestração e portabilidade  
- **Design modular e desacoplado** → Independência entre camadas e baixo acoplamento  

---

## 🔧 Stack Técnica

- .NET 10 C#  
- InMemory Dispatchers (facilitador CQRS)  
- FluentValidation (com behaviors customizados)  
- EF Core + PostgreSQL  
- Autenticação JWT  
- Docker Compose para orquestração (PostgreSQL, Redis etc.)  

---

## 🧱 Estrutura do Projeto
```
📆 Projeto XCONTROLFIN  
├── docker-compose.dcproj
├── docker-compose.yml                          # Orquestração de containers (PostgreSQL, Redis etc.)  
├── scripts/                                    # Scripts de inicialização do banco  
│   └── init-auth-sql.sql  
├── src/  
│   ├── xcontrolfin.Api/                        # Camada de apresentação (Web API)  
│   │   ├── Controllers/                        # Controllers por feature  
│   │   ├── Extensions/                         # Extensões para middlewares, autenticação etc.  
│   │   ├── Middleware/                         # Middlewares customizados  
│   │   └── Resources/                          # Arquivos de internacionalização (resx)  
│   ├── xcontrolfin.Application/                # Casos de uso (commands/queries), validadores e respostas  
│   │   ├── Commons/                            # Contratos, handlers genéricos, DTOs de resposta  
│   │   ├── Exceptions/                         # Exceções da camada de aplicação  
│   │   └── Features/                           # Funcionalidades organizadas por contexto de negócio  
│   ├── xcontrolfin.Crosscutting.Common/        # Funcionalidades transversais  
│   │   ├── Localization/                       # Serviços de localização e cultura  
│   │   ├── Logging/                            # Integração com logs  
│   │   ├── Security/                           # Autenticação, JWT, usuário atual  
│   │   └── Validation/                         # Infra de validação genérica  
│   ├── xcontrolfin.Crosscutting.IoC/           # Registro de dependências e módulos de injeção  
│   │   └── ModuleInitializers/                 # Inicializadores separados por responsabilidade  
│   ├── xcontrolfin.Domain/                     # Entidades e regras de negócio  
│   │   ├── Entities/                           # Entidades de domínio  
│   │   ├── Interfaces/                         # Contratos de repositórios e serviços  
│   │   ├── Models/                             # DTOs e objetos de transferência  
│   │   └── Validation/                         # Validadores de domínio  
│   ├── xcontrolfin.Infrastructure/             # Implementações técnicas  
│   │   ├── Caching/                            # Cache com Redis  
│   │   ├── Data/                               # Contexto EF Core, mapeamentos e conversores  
│   │   ├── Exceptions/                         # Exceções específicas da infraestrutura  
│   │   ├── Logging/                            # Decoradores e extensões de logging  
│   │   ├── Repositories/                       # Implementação de repositórios  
│   │   └── Services/                           # Serviços de autenticação e auxiliares  
│   ├── xcontrolfin.Shared/                     # Utilitários e dispatchers  
│   │   ├── Commands/                           # Dispatcher de comandos e auxiliares  
│   │   └── Queries/                            # Dispatcher de queries  
│   ├── xcontrolfin.Shared.Abstractions/        # Contratos e interfaces base (DDD/CQRS)  
│   │   ├── Behaviors/                          # Comportamentos como validação  
│   │   ├── Commands/                           # Interfaces para comandos e handlers  
│   │   └── Queries/                            # Interfaces para queries e handlers    
└── README
```

---

## 📊 Módulo Financeiro

### Entidades principais
- **UserEntity** → Usuários do sistema  
- **CostCenterEntity** → Centros de custo  
- **FinancialInstitutionEntity** → Instituições financeiras  
- **FinancialReleaseEntity** → Lançamentos financeiros realizados  
- **FinancialPlanningEntity** → Planejamentos financeiros recorrentes  

### Funcionalidade-chave
Consulta de lançamentos **realizados** e **planejados**, filtrados por:
- `FinancialInstitutionId`  
- Intervalo de `PaymentDate` (StartDate e EndDate)  

---

## 🎯 Atrativos do Projeto

- ✅ **Escalabilidade**: arquitetura preparada para crescer sem comprometer performance  
- ✅ **Testabilidade**: separação clara de responsabilidades e baixo acoplamento  
- ✅ **Flexibilidade**: fácil extensão de módulos e funcionalidades  
- ✅ **Infra moderna**: execução simplificada via Docker  
- ✅ **Segurança**: autenticação JWT integrada  

---

## 🐳 Execução Local

```
# Subir containers
docker-compose up -d
```

[Manual de acesso ao banco](docs/ACESSO_BANCO.md)


---

## 📈 O que vem pela frente:

* :heavy_check_mark: Finalizar analise de informações e estrutura da dos dados
* :heavy_check_mark: Criar repositório proprio
* :heavy_check_mark: Finalizar implementação da autenticação jwt
* :heavy_check_mark: OpenAPI/Scalar para documentação viva, com execuções e regras de autenticação/autorização
* :heavy_check_mark: Construir/segregar responsabilidades das demais entidades, configurar relacionamentos, comportamentos
* Implementar classes genericas para Aplications, Infrastructure e Domain (Repositórios, Services, Handlers, Validators ...)
* Implementar validações customizadas com FluentValidation
* Implementar logging estruturado com Serilog (Ajustar middleware com as principais exceções)
* Implmentar paginação nas consultas
* Analisar a implementação de autorizações baseadas em políticas (Policy-based Authorization)
* Analisar a implementação de controle de cache com Redis
* Analisar a implementação de globalização, tradução dos componentes da Api (validações, exceções, msgs ...)
* Implementar testes unitários e de integração
* OpenTelemetry para observabilidade
* RabbitMQ ou Kafka para eventos assíncronos
* Kubernetes (k8s) para orquestração
* Utilização do (k6.io) para testes de massa de dados, confiabilidade e desempenho
