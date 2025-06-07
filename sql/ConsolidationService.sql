-- Tabela de saldos diários consolidados, utilizada pelo Serviço de Consolidação
CREATE TABLE daily_balances (
    id UUID PRIMARY KEY,  -- Identificador único gerado pelo backend
    reference_date DATE NOT NULL,  -- Data de referência do saldo
    total_credit NUMERIC(14,2) NOT NULL,  -- Total de créditos registrados no dia
    total_debit NUMERIC(14,2) NOT NULL,  -- Total de débitos registrados no dia
    balance NUMERIC(14,2) NOT NULL,  -- Resultado líquido (saldo) do dia
    generated_at TIMESTAMP WITH TIME ZONE NOT NULL  -- Momento da geração da consolidação
);
