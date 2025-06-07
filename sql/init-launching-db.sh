#!/bin/bash

# ---------------------------------------------------------------------------------------------
# init-launching-db.sh
# Inicializa o banco de dados do microserviço LaunchingService no container PostgreSQL
#
# Autor: Flavio dos Santos
# Ambiente: WSL2 + Ubuntu 22.04 LTS + Docker + Docker Compose V2
#----------------------------------------------------------------------------------------------
# 💡 Observação:
# Os scripts .sql devem estar localizados na pasta:
#   C:\workarea\projects\Challenge.SolutionArchitecture\sql
# ou, no WSL2:
#   /mnt/c/workarea/projects/Challenge.SolutionArchitecture/sql
#
# Para acessar via terminal WSL:
#   cd /mnt/c/workarea/projects/Challenge.SolutionArchitecture/sql
#
# Pré-requisitos:
# - O container PostgreSQL deve estar rodando via docker compose
# - Nome do serviço no docker-compose.yaml: postgresql
# - Os arquivos LaunchingServiceDB.sql e LaunchingService.sql devem estar nesta pasta
#
# Como usar:
# 1. Dar permissão:
#    chmod +x init-launching-db.sh
# 2. Executar:
#    ./init-launching-db.sh
# ------------------------------------------------------------------------

# Configurações do banco de dados
CONTAINER_NAME="postgresql"
DATABASE_NAME="launching_service_db"
USER="postgres"
PASSWORD="your_password"  # Ajuste conforme seu docker-compose

# Verifica se o banco já existe dentro do container
echo "🔍 Verificando se o banco de dados '$DATABASE_NAME' existe..."
EXISTING_DB=$(docker exec -e PGPASSWORD=$PASSWORD -i $CONTAINER_NAME \
  psql -U $USER -tAc "SELECT 1 FROM pg_database WHERE datname='$DATABASE_NAME'")

if [ "$EXISTING_DB" != "1" ]; then
    echo "📌 Banco de dados '$DATABASE_NAME' não encontrado. Criando..."
    docker exec -e PGPASSWORD=$PASSWORD -i $CONTAINER_NAME \
      psql -U $USER < LaunchingServiceDB.sql
    if [ $? -eq 0 ]; then
        echo "✅ Banco de dados '$DATABASE_NAME' criado com sucesso!"
    else
        echo "❌ Erro ao criar o banco de dados!"
        exit 1
    fi
else
    echo "✅ O banco de dados '$DATABASE_NAME' já existe."
fi

# Scripts SQL para criação de estruturas dentro do banco
SQL_SCRIPTS=(
  "LaunchingService.sql"
)

echo "🚀 Iniciando execução dos scripts SQL para '$DATABASE_NAME'..."

for SCRIPT in "${SQL_SCRIPTS[@]}"; do
    echo "📂 Executando $SCRIPT..."
    docker exec -e PGPASSWORD=$PASSWORD -i $CONTAINER_NAME \
      psql -U $USER -d $DATABASE_NAME < $SCRIPT
    if [ $? -eq 0 ]; then
        echo "✅ $SCRIPT executado com sucesso!"
    else
        echo "❌ Erro ao executar $SCRIPT"
        exit 1
    fi
done

echo "🎉 Banco de dados '$DATABASE_NAME' configurado com sucesso!"
