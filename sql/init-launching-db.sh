#!/bin/bash

# Diretório base relativo ao script
BASE_DIR="$(dirname "$0")"

# Configurações do banco de dados
CONTAINER_NAME="postgresql"
DATABASE_NAME="launching_service_db"
USER="postgres"
PASSWORD="your_password"  # Ajuste conforme seu docker-compose

# Verifica se o banco já existe dentro do container
echo "🔍 Verificando se o banco de dados '$DATABASE_NAME' existe..."
EXISTING_DB=$(docker exec -e PGPASSWORD=$PASSWORD -i $CONTAINER_NAME \
  psql -U $USER -tAc "SELECT 1 FROM pg_database WHERE datname='$DATABASE_NAME'")

if [[ "$EXISTING_DB" != "1" ]]; then
  echo "📦 Banco de dados '$DATABASE_NAME' não encontrado. Criando..."
  docker exec -e PGPASSWORD=$PASSWORD -i $CONTAINER_NAME \
    psql -U $USER < "$BASE_DIR/LaunchingServiceDB.sql"
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
  "$BASE_DIR/LaunchingService.sql"
)

echo "🚀 Iniciando execução dos scripts SQL para '$DATABASE_NAME'..."

for SCRIPT in "${SQL_SCRIPTS[@]}"; do
  echo "📄 Executando $SCRIPT..."
  docker exec -e PGPASSWORD=$PASSWORD -i $CONTAINER_NAME \
    psql -U $USER -d $DATABASE_NAME < "$SCRIPT"
  if [ $? -eq 0 ]; then
    echo "✅ $SCRIPT executado com sucesso!"
  else
    echo "❌ Erro ao executar $SCRIPT"
    exit 1
  fi
done

echo "🏁 Banco de dados '$DATABASE_NAME' configurado com sucesso!"
