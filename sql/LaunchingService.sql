-- Tabela de lançamentos financeiros registrados pelo comerciante
CREATE TABLE transactions (
    id UUID PRIMARY KEY,  -- Identificador único gerado pelo backend
    occurred_at TIMESTAMP WITH TIME ZONE NOT NULL,  -- Data e hora em que a transação ocorreu
    amount NUMERIC(14,2) NOT NULL,  -- Valor monetário da transação, com duas casas decimais
    type VARCHAR(10) NOT NULL CHECK (type IN ('credit', 'debit')),  -- Tipo da transação: crédito ou débito
    description VARCHAR(255),  -- Descrição opcional, livre, fornecida pelo comerciante
    created_at TIMESTAMP WITH TIME ZONE NOT NULL  -- Data e hora em que a transação foi registrada no sistema
);
