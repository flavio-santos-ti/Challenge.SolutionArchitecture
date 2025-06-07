-- Criação do banco de dados para o Serviço de Consolidação de Saldos Diários
CREATE DATABASE consolidation_service_db
WITH
    OWNER = postgres -- Proprietário do banco (ajuste conforme necessário)
    ENCODING = 'UTF8' -- Codificação padrão para suportar caracteres acentuados
    LC_COLLATE = 'pt_BR.UTF-8' -- Ordem de classificação de strings (locale)
    LC_CTYPE = 'pt_BR.UTF-8' -- Tipo de caracteres (locale)
    TABLESPACE = pg_default -- Tablespace padrão do PostgreSQL
    CONNECTION LIMIT = -1; -- Sem limite de conexões simultâneas
