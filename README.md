📄 Fintech API - Manual de Utilização
📌 Visão Geral
Esta é uma API REST para gerenciar uma fintech, construída em .NET 8 com PostgreSQL, Docker e boas práticas de Clean Architecture, SOLID e testes automatizados. O projeto está configurado para ser facilmente executado via docker-compose, garantindo que qualquer desenvolvedor possa clonar e subir o ambiente de forma rápida.

🚀 Tecnologias Utilizadas
.NET 8

PostgreSQL 16
Docker / Docker Compose
EF Core
Swagger
xUnit
Clean Architecture
SOLID principles
MediatR
Dapper

⚙️ Configuração do Banco de Dados (Docker)
O serviço de banco de dados já está configurado no arquivo docker-compose.yml:

services:
  postgres:
    image: postgres:16
    container_name: fintech_postgres
    restart: always
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: post1
      POSTGRES_DB: fintechdb
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    networks:
      - fintech_network

networks:
  fintech_network:
    driver: bridge

volumes:
  postgres_data:
Para subir o banco de dados, basta rodar no terminal: docker-compose up -d

⚙️ Configuração da Aplicação
No arquivo appsettings.json já existe a string de conexão apontando para o PostgreSQL no Docker:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=fintechdb;Username=postgres;Password=post1"
}

📝 Como rodar a aplicação

1️⃣ Clone o repositório: git clone https://github.com/seuusuario/nomedorepo.git

2️⃣ Restaure os pacotes: dotnet restore
3️⃣ Execute as migrations (se necessário): dotnet ef database update
4️⃣ Inicie a aplicação: dotnet run --project Fintech.API

🧪 Executando os Testes
Os testes unitários e de integração estão no projeto Fintech.Tests. Execute com: dotnet test

🛠️ Endpoints Documentados
A API já possui o Swagger configurado:

Acesse http://localhost:5000/swagger (ou a porta configurada)

Visualize todos os endpoints:

 -> Cadastro de usuários
 -> Login
 -> Gestão de carteira
 -> Transferências entre carteiras
 -> Consultas de saldo
 -> Registro de transações

🔐 Configuração de Autenticação JWT
As credenciais JWT estão configuradas no appsettings.json:

"JwtSettings": {
  "Secret": "sua-chave-super-secreta",
  "ExpirationHours": 2,
  "Issuer": "FintechAPI",
  "Audience": "FintechAPIUsers"
}

📦 Estrutura do Projeto
 -> Fintech.API - camada de apresentação (controllers, middlewares)
 -> Fintech.Application - regras de negócio, validações, serviços
 -> Fintech.Domain - entidades de domínio
 -> Fintech.Infrastructure - acesso a dados (EF Core, Dapper)
 -> Fintech.Tests - testes automatizados

📄 Boas Práticas Utilizadas
✅ Clean Architecture
✅ SOLID
✅ CQRS com MediatR
✅ Banco de dados desacoplado via Docker
✅ Testes automatizados
✅ Versionamento via Git
✅ Documentação Swagger

🧩 Requisitos para rodar

 -> Docker instalado
 -> .NET 8 SDK
 -> Git

🫱 Contato
Rodrigo Digorilla
https://github.com/dgxcode
dgxcode@gmail.com

