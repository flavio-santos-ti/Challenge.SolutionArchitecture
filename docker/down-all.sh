#!/bin/bash

#-----------------------------------------------------------------------------------------
# Como usar:
# 1. Dar permissão:
#    chmod +x down-all.sh
# 2. Executar:
#    ./down-all.sh
# ----------------------------------------------------------------------------------------

echo "🛑 Encerrando e removendo containers e volumes..."

echo "🔻 Removendo container e volumes do ConsolidationService..."
(cd api-consolidation && docker compose down -v 2>/dev/null || echo "ℹ️ ConsolidationService já estava parado ou não existe.")

echo "🔻 Removendo container e volumes do LaunchingService..."
(cd api-launching && docker compose down -v 2>/dev/null || echo "ℹ️ LaunchingService já estava parado ou não existe.")

echo "🔻 Removendo container e volumes do API Gateway..."
(cd api-gateway && docker compose down -v 2>/dev/null || echo "ℹ️ API Gateway já estava parado ou não existe.")

echo "🔻 Removendo container e volumes do PostgreSQL..."
(cd postgresql && docker compose down -v 2>/dev/null || echo "ℹ️ PostgreSQL já estava parado ou não existe.")

echo "🧹 Removendo rede Docker challenge-net..."
docker network rm challenge-net 2>/dev/null && echo "✅ Rede challenge-net removida." || echo "ℹ️ Rede challenge-net já não existia."

echo "✅ Finalizado. Todos os recursos foram encerrados e removidos."
