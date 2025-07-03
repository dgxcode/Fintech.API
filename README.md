Fintech API - Manual de Utilização

Visão Geral
Esta é uma API REST para gerenciar uma fintech, construída em .NET 8 com PostgreSQL, Docker e boas práticas de Clean Architecture, SOLID e testes automatizados. O projeto está configurado para ser facilmente executado via docker-compose, garantindo que qualquer desenvolvedor possa clonar e subir o ambiente de forma rápida.

---

Tecnologias
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL (via Docker)
- PostgreSQL 16
- Docker / Docker Compose
- Swagger
- User-Secrets (.NET)
- DotNetEnv (.env) para segredos
- Clean Architecture (camadas separadas)
- SOLID
- autenticação JWT
- MediatR
- Dapper
- xUnit

---

Configuração do ambiente

Pré-requisitos
- .NET 8 SDK
- Docker
- Git
- Visual Studio 2022 ou VS Code (opcional)

Clonando o projeto
git clone https://github.com/dgxcode/Fintech.API
cd Fintech.API

Subindo o banco PostgreSQL via Docker
docker-compose up -d

---

Configurando variáveis de ambiente
NUNCA coloque segredos diretamente no appsettings.json.

Crie um arquivo chamado .env na raiz do projeto Fintech.API:
JWT_SECRET=sua-chave-super-segura

Para gerar a chave de forma segura:
- Linux/macOS: openssl rand -base64 32
- Windows PowerShell: [Convert]::ToBase64String((1..32 | ForEach-Object {Get-Random -Max 256}))

---

Configurando User-Secrets (Visual Studio)
dotnet user-secrets init --project ./Fintech.API
dotnet user-secrets set "JwtSettings:Secret" "sua-chave-super-segura" --project ./Fintech.API

---

Connection string já configurada no appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=fintechdb;Username=postgres;Password=post1"
}

---

Rodando a aplicação
dotnet run --project ./Fintech.API

Swagger: https://localhost:5001/swagger

---

Banco de dados
O projeto faz seeding automático ao subir, usando o FintechDbContextSeed.SeedAsync().

---

Executando os Testes
dotnet test

---

Endpoints Documentados
- Cadastro de usuários
- Login
- Gestão de carteira
- Transferências entre carteiras
- Consultas de saldo
- Registro de transações

---

Configuração de Autenticação JWT
"JwtSettings": {
  "Secret": "sua-chave-super-secreta",
  "ExpirationHours": 2,
  "Issuer": "FintechAPI",
  "Audience": "FintechAPIUsers"
}

---

Estrutura do Projeto
- Fintech.API: camada de apresentação
- Fintech.Application: regras de negócio
- Fintech.Domain: entidades de domínio
- Fintech.Infrastructure: acesso a dados
- Fintech.Tests: testes automatizados

---

Boas práticas
- O .env está no .gitignore e não deve ser versionado
- Utilize user-secrets no Visual Studio
- Para parar o banco Docker: docker-compose down
- Para limpar dados: docker volume prune

---

Contribuição
Pull requests são bem-vindos!
Abra issues para bugs ou melhorias.

---

Licença
MIT License — fique à vontade para evoluir este projeto.

---

Contato
Rodrigo de Sousa Batista
LinkedIn: https://www.linkedin.com/in/rodrigo-dgxcode/
GitHub: https://github.com/dgxcode
