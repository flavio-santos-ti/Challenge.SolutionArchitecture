#!/bin/bash

echo "🔄 Subindo container do LaunchingService..."
docker compose -f docker-compose.yaml up -d
echo "✅ Container do LaunchingService iniciado com sucesso."
