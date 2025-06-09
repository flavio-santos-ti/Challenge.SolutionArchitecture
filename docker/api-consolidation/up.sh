#!/bin/bash

echo "🔄 Subindo container do ConsolidationService..."
docker compose -f docker-compose.yaml up -d
echo "✅ Container do ConsolidationService iniciado com sucesso."
