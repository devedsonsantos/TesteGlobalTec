CREATE DATABASE Desafio2;

USE Desafio2;

CREATE TABLE Pessoas(
	Codigo BIGINT PRIMARY KEY IDENTITY,
	Nome VARCHAR(110) NOT NULL,
	CpfCnpj VARCHAR(14) NOT NULL
);

INSERT INTO Pessoas(Nome, CpfCnpj) VALUES('Luiza Antonella Cardoso', '71353511421');
INSERT INTO Pessoas(Nome, CpfCnpj) VALUES('Cristiane Sabrina Camila Moraes', '63984498357');
INSERT INTO Pessoas(Nome, CpfCnpj) VALUES('Aurora Juliana Laís Fogaça', '82650247380');

CREATE TABLE ContasAPagar(
	Numero BIGINT PRIMARY KEY IDENTITY,
	CodigoFornecedor BIGINT NOT NULL,
	DataVencimento DATE NOT NULL,
	DataProrrogacao DATE,
	Valor DECIMAL(18,6) NOT NULL,
	Acrescimo DECIMAL(18,6),
	Desconto DECIMAL(18,6)
);

INSERT INTO ContasAPagar(CodigoFornecedor, DataVencimento, Valor) VALUES(1, '2021-09-12', 235.45);
INSERT INTO ContasAPagar(CodigoFornecedor, DataVencimento, Valor, Acrescimo) VALUES(2, '2021-10-20', 635.49, 50.01);
INSERT INTO ContasAPagar(CodigoFornecedor, DataVencimento, Valor, Acrescimo, Desconto) VALUES(3, '2021-10-20', 800.99, 150.01, 20);
INSERT INTO ContasAPagar(CodigoFornecedor, DataVencimento, Valor) VALUES(1, '2021-09-12', 500);

CREATE TABLE ContasPagas(
	Numero BIGINT PRIMARY KEY,
	CodigoFornecedor BIGINT NOT NULL,
	DataVencimento DATE NOT NULL,
	DataPagamento DATE,
	Valor DECIMAL(18,6) NOT NULL,
	Acrescimo DECIMAL(18,6),
	Desconto DECIMAL(18,6)
);

INSERT INTO ContasPagas(Numero, CodigoFornecedor, DataVencimento, DataPagamento,Valor) VALUES(4, 1, '2021-09-12', GETDATE() ,500);
INSERT INTO ContasPagas(Numero, CodigoFornecedor, DataVencimento, DataPagamento,Valor, Acrescimo) VALUES(2, 2, '2021-10-20', GETDATE() ,635.49, 50.01);
INSERT INTO ContasPagas(Numero, CodigoFornecedor, DataVencimento, DataPagamento,Valor, Acrescimo, Desconto) VALUES(3, 3, '2021-10-20', GETDATE() ,800.99, 150.01, 20);

SELECT ISNULL(cp.Numero, cap.Numero) AS "Número do Processo"
    , p.Nome AS "Nome do fornecedor"
    ,ISNULL(cp.DataVencimento, cap.DataVencimento) AS "Data de vencimento"
    ,cp.DataPagamento AS "Data de pagamento"
    ,IIF(cp.Valor IS NULL, (cap.Valor + ISNULL(cap.Acrescimo, 0) - ISNULL(cap.Desconto, 0)), (cp.Valor + ISNULL(cap.Acrescimo, 0) - ISNULL(cap.Desconto, 0))) AS "Valor líquido"
    ,IIF(cp.Numero IS NULL, 'Em aberto', 'Liquidado') AS "Situação"
FROM Pessoas p
LEFT JOIN ContasAPagar cap
    ON p.Codigo = cap.CodigoFornecedor
LEFT JOIN ContasPagas cp
    ON cap.Numero = cp.Numero;