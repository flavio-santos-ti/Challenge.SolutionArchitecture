#!/bin/bash

# ------------------------------------------------------------------------
# up-all.sh
# Sobe o container PostgreSQL via Docker Compose V2
#
# Autor: Flavio dos Santos
# Ambiente: WSL2 + Ubuntu 22.04 LTS + Docker + Docker Compose V2
#-------------------------------------------------------------------------
# Em caso de erro no arquivo: sudo apt install dos2unix
#   Ex: dos2unix nome_do_arquivo
#       dos2unix *.sh
#-------------------------------------------------------------------------
# Como usar:
# 1. Dar permissão:
#    chmod +x up-all.sh
# 2. Executar:
#    ./up-all.sh
#-------------------------------------------------------------------------

echo "🔧 Criando rede Docker challenge-net (caso ainda não exista)..."
docker network create challenge-net 2>/dev/null || echo "ℹ️ Rede já existe."
echo "✅ Rede challenge-net pronta."

echo "🚀 Subindo PostgreSQL..."
(cd postgresql && ./up-docker-postgres.sh)
echo "✅ PostgreSQL está em execução."

echo "🚀 Subindo API Gateway..."
(cd api-gateway && docker compose build --no-cache && ./up.sh)
echo "✅ API Gateway está em execução."

echo "🚀 Subindo LaunchingService..."
(cd api-launching && docker compose build --no-cache && ./up.sh)
echo "✅ LaunchingService está em execução."

echo "🚀 Subindo ConsolidationService..."
(cd api-consolidation && docker compose build --no-cache && ./up.sh)
echo "✅ ConsolidationService está em execução."

echo "🏁 Todos os containers foram iniciados com sucesso!"

echo "🟦 Executando inicialização do banco LaunchingService..."
chmod +x ../sql/init-launching-db.sh
../sql/init-launching-db.sh

echo "🟦 Executando inicialização do banco ConsolidationService..."
chmod +x ../sql/init-consolidation-db.sh
../sql/init-consolidation-db.sh

echo "✅ Todos os containers foram iniciados e os bancos foram inicializados com sucesso."
