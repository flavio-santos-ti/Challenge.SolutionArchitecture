#!/bin/bash

# ------------------------------------------------------------------------
# up-postgres.sh
# Sobe o container PostgreSQL via Docker Compose V2
#
# Autor: Flavio dos Santos
# Ambiente: WSL2 + Ubuntu 22.04 LTS + Docker + Docker Compose V2
#-------------------------------------------------------------------------
# 💡 Dica prática:
# Certifique-se de que os arquivos .sql e o docker-compose.yaml
# estejam localizados em uma pasta montada sob /mnt para que o 
# Docker possa acessar os volumes corretamente no WSL2.
#
# Exemplo prático:
# No Windows:
#   C:\workarea\projects\Challenge.SolutionArchitecture\docker\postgresql
#
# No WSL2 (Ubuntu):
#   /mnt/c/workarea/projects/Challenge.SolutionArchitecture/docker/postgresql
#
# Acesse via terminal WSL:
#   cd /mnt/c/workarea/projects/Challenge.SolutionArchitecture/docker/postgresql
#
# Requisitos:
# - Docker instalado no Windows
# - Docker Compose V2 disponível no WSL2 (use: docker compose ...)
#-----------------------------------------------------------------------------------------
# Como usar:
# 1. Dar permissão:
#    chmod +x up-docker-postgres.sh
# 2. Executar:
#    ./up-docker-postgres.sh
# ----------------------------------------------------------------------------------------

echo "🔄 Subindo o container PostgreSQL..."

docker compose -f docker-compose.yaml up -d

echo "✅ PostgreSQL iniciado com sucesso."
