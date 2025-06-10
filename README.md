# Challenge Solution Architecture

![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-blueviolet?style=flat&logo=dotnet) 
![Docker](https://img.shields.io/badge/Docker-Container_Platform-blue?logo=docker) 
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-17-blue?logo=postgresql) 
[![NuGet ApiResponse](https://img.shields.io/nuget/v/Flavio.Santos.NetCore.ApiResponse?label=NuGet%20ApiResponse&logo=nuget)](https://www.nuget.org/packages/Flavio.Santos.NetCore.ApiResponse/)

## 📌 Sobre

Desafio técnico de arquitetura de software proposto com foco em microserviços, mensageria, persistência e automação de infraestrutura.

---

## 🛠️ Requisitos de Ambiente

Para executar esta solução corretamente, é necessário:

-  **WSL2 com Ubuntu 22.04 LTS**
-  **Docker e Docker Compose instalados no WSL**
-  **.NET 8** SDK (já incluso nas imagens base)
-  **PostgreSQL** client (opcional, para acesso local ao banco via terminal)

---

## 📦 O Que Está Neste Repositório

Este repositório contém:

- Código-fonte completo de **três microserviços**:
  - `LaunchingService` – responsável pelos lançamentos financeiros
  - `ConsolidationService` – responsável por consolidar os saldos diários
  - `ApiGateway` – roteamento centralizado via **YARP**
- Scripts `.sh` para subir e derrubar toda a stack localmente
- Dockerfile e docker-compose separados por microserviço
- Banco de dados PostgreSQL com criação automatizada via SQL script
- Documentação em PDF com **diagramas C4**, **requisitos**, **decisões arquiteturais** e **considerações de escalabilidade**
