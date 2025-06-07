#!/bin/bash

# -----------------------------------------------------------------------------
# init-consolidation-db.sh
# Inicializa o banco consolidation_service_db e sua tabela daily_balances
#
# Autor: Flavio dos Santos
# Ambiente: WSL2 + Ubuntu 22.04 LTS + Docker + Docker Compose V2
#------------------------------------------------------------------------------
# 💡 Diretório dos arquivos SQL:
# No Windows:
#   C:\workarea\projects\Challenge.SolutionArchitecture\sql
# No WSL2:
#   /mnt/c/workarea/projects/Challenge.SolutionArchitecture/sql
#
# Acesse com:
#   cd /mnt/c/workarea/projects/Challenge.SolutionArchitecture/sql
#
# Pré-requisitos:
# - Container PostgreSQL rodando com nome: postgresql
# - Senha de acesso definida corretamente abaixo
#
# Como usar:
#   chmod +x init-consolidation-db.sh
#   ./init-consolidation-db.sh
# -----------------------------------------------------------------------------

CONTAINER_NAME="postgresql"
DATABASE_NAME="consolidation_service_db"
USER="postgres"
PASSWORD="your_password"  # ajuste conforme seu docker-compose

echo "🔍 Verificando existência do banco '$DATABASE_NAME'..."
EXISTS=$(docker exec -e PGPASSWORD=$PASSWORD -i $CONTAINER_NAME \
  psql -U $USER -tAc "SELECT 1 FROM pg_database WHERE datname = '$DATABASE_NAME'")

if [ "$EXISTS" != "1" ]; then
    echo "📌 Criando banco '$DATABASE_NAME'..."
    docker exec -e PGPASSWORD=$PASSWORD -i $CONTAINER_NAME \
      psql -U $USER < ConsolidationServiceDB.sql
    if [ $? -eq 0 ]; then
        echo "✅ Banco criado com sucesso."
    else
        echo "❌ Falha na criação do banco."
        exit 1
    fi
else
    echo "✅ Banco '$DATABASE_NAME' já existe."
fi

# Scripts de estrutura do banco
SQL_SCRIPTS=(
  "ConsolidationService.sql"
)

echo "🚀 Executando scripts no banco '$DATABASE_NAME'..."

for SCRIPT in "${SQL_SCRIPTS[@]}"; do
    echo "📂 Executando $SCRIPT..."
    docker exec -e PGPASSWORD=$PASSWORD -i $CONTAINER_NAME \
      psql -U $USER -d $DATABASE_NAME < $SCRIPT
    if [ $? -eq 0 ]; then
        echo "✅ $SCRIPT executado com sucesso."
    else
        echo "❌ Erro ao executar $SCRIPT"
        exit 1
    fi
done

echo "🎉 Banco '$DATABASE_NAME' configurado com sucesso!"
