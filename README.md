# Challenge Solution Architecture

![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-blueviolet?style=flat&logo=dotnet) 
![Docker](https://img.shields.io/badge/Docker-Container_Platform-blue?logo=docker) 
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-17-blue?logo=postgresql) 
[![NuGet ApiResponse](https://img.shields.io/nuget/v/Flavio.Santos.NetCore.ApiResponse?label=NuGet%20ApiResponse&logo=nuget)](https://www.nuget.org/packages/Flavio.Santos.NetCore.ApiResponse/)
[![Nuget ApiResponse Downloads](https://img.shields.io/nuget/dt/Flavio.Santos.NetCore.ApiResponse?label=Downloads%20ApiResponse&logo=nuget)](https://www.nuget.org/packages/Flavio.Santos.NetCore.ApiResponse/)

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

## 📦 O Que Está Neste Repositório?

Este repositório contém:

- Código-fonte completo de **três microserviços**:
  - `LaunchingService` – responsável pelos lançamentos financeiros
  - `ConsolidationService` – responsável por consolidar os saldos diários
  - `ApiGateway` – roteamento centralizado via **YARP**
- Scripts `.sh` para subir e derrubar toda a stack localmente
- Dockerfile e docker-compose separados por microserviço
- Banco de dados PostgreSQL com criação automatizada via SQL script
- Documentação em PDF com **diagramas C4**, **requisitos**, **decisões arquiteturais** e **considerações de escalabilidade**

---

## 📄 Documentação Técnica

A documentação completa está no arquivo PDF abaixo:

📘 [`Challenge-SolutionArchitecture.pdf`](./pdf/Challenge-SolutionArchitecture.pdf)

Ela abrange:

- Descrição do problema e solução
- Decisões arquiteturais estratégicas
- Diagrama de containers e interações
- Visão de implantação
- Considerações sobre escalabilidade, cache e observabilidade

---

## 🚀 Executando o Projeto

> **Pré-requisitos:**
> - Docker instalado e funcional **dentro do WSL2**
> - Ubuntu 22.04 LTS como distribuição do WSL2
> - Git instalado no WSL2

### 🧭 Posicionamento no Terminal

Todos os comandos devem ser executados **de dentro do terminal do WSL2** (Ubuntu 22.04), com o terminal posicionado no diretório de execução do projeto.  
Por exemplo:

```bash
cd /mnt/c/workarea/projects/Challenge.SolutionArchitecture/docker
```

---

### 📦 1. Clonando o Repositório

```bash
git clone https://github.com/flavio-santos-ti/Challenge.SolutionArchitecture.git
```

### 📁 2. Posicionando-se no Diretório do Projeto

Todos os comandos devem ser executados de dentro do terminal do WSL2 (Ubuntu 22.04), com o terminal posicionado no diretório de execução do projeto.
Por exemplo:

```bash
cd /mnt/c/workarea/projects/Challenge.SolutionArchitecture/docker
```

### 🔐 3. Permissões de Execução

Antes de executar pela primeira vez, é necessário conceder permissão de execução para os scripts .sh:

```bash
chmod +x up-all.sh
```

```bash
chmod +x down-all.sh
```

### ⬆️ 4. Subindo os Containers (PostgreSQL + APIs)

Execute:

```bash
./up-all.sh
```

Esse script irá:

- Criar a rede challenge-net
- Subir o banco PostgreSQL
- Inicializar os bancos e tabelas (init-*.sh)
- Subir os microserviços (api-gateway, api-launching, api-consolidation)

### ⬇️ 5. Parando e Removendo Tudo

```bash
./down-all.sh
```

---

## 📬 Testando as APIs (Exemplos via Postman)

Após a execução, as APIs estarão disponíveis via rede local (bridge `challenge-net`).  
⚠️ **Substitua `172.19.121.141` pelo IP do seu WSL2**, que pode ser obtido com:

```bash
ip addr | grep eth0
```

Exemplos de chamadas:

#### ➕ Criar Transação

```http
POST http://172.19.121.141:5000/api/Transactions
Content-Type: application/json
```
```json
{
  "occurredAt": "2025-06-08",
  "amount": 112.56,
  "type": "debit",
  "description": "Conta de luz"
}
```

#### 📄 Consultar Transações por Data

```http
GET http://172.19.121.141:5000/api/Transactions/?date=2025-06-08
```

#### ➕ Gerar Saldo Diário

```http
POST http://localhost:5000/api/DailyBalances
Content-Type: application/json
```

```json
{
  "referenceDate": "2025-06-09"
}
```

#### 📄 Consultar Saldo Diário

```http
GET http://localhost:5000/api/DailyBalances/2025-06-08
```

