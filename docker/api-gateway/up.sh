#!/bin/bash

echo "🔄 Subindo container do API Gateway..."
docker compose -f docker-compose.yaml up -d
echo "✅ Container do API Gateway iniciado com sucesso."
