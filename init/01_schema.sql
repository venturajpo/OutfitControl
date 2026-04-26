CREATE DATABASE estoque_roupa;
USE estoque_roupa;

CREATE TABLE funcionario(
id_funcionario INT AUTO_INCREMENT PRIMARY KEY,
matricula CHAR(7) NOT NULL,
nome VARCHAR(100) NOT NULL
);
ALTER TABLE funcionario ADD UNIQUE INDEX idx_matricula_funcionario (matricula);

CREATE TABLE peca(
id_peca INT AUTO_INCREMENT PRIMARY KEY,
tipopeca ENUM('Camiseta', 'Calca', 'Calcado') NOT NULL , -- Manter os enums usando caracteres ASCII padrão para maior compatibilidade
tamanhopeca varchar(4) NOT NULL
);
ALTER TABLE peca ADD UNIQUE INDEX idx_peca_tipo_tamanho (tipopeca, tamanhopeca); -- Cria um índice único entre tipo e tamanho de peça

CREATE TABLE pedido(
id_pedido INT AUTO_INCREMENT PRIMARY KEY,
data DATE NOT NULL,
id_funcionario INT NOT NULL,
status ENUM('Novo', 'EmProcesso', 'AguardandoRetirada', 'Finalizado') NOT NULL, -- Manter os enums usando caracteres ASCII padrão para maior compatibilidade
CONSTRAINT FK_id_funcionario FOREIGN KEY (id_funcionario)
  REFERENCES funcionario(id_funcionario)
);

CREATE TABLE retirada(
id_retirada INT AUTO_INCREMENT PRIMARY KEY,
id_pedido INT NOT NULL,
data DATE NOT NULL,
CONSTRAINT FK_id_pedido FOREIGN KEY (id_pedido)
REFERENCES pedido(id_pedido)
);

CREATE TABLE lote(
id_lote INT AUTO_INCREMENT PRIMARY KEY,
id_peca INT NOT NULL,
data DATE NOT NULL ,
quantidade INT NOT NULL,
CONSTRAINT FK_id_peca FOREIGN KEY (id_peca)
REFERENCES peca(id_peca)
);

CREATE TABLE peca_x_pedido(
id_peca_x_pedido INT AUTO_INCREMENT PRIMARY KEY,
id_peca INT NOT NULL,
id_pedido INT NOT NULL,
quantidade INT NOT NULL,
CONSTRAINT FK_id_peca_pedido FOREIGN KEY (id_peca)
REFERENCES peca(id_peca),
CONSTRAINT FK_id_pedido_peca FOREIGN KEY (id_pedido)
REFERENCES pedido(id_pedido)
);

CREATE VIEW estoque AS
SELECT combined.id as id_peca, SUM(combined.qtd) as quantidade
FROM (SELECT entrada.id_peca as id, entrada.quantidade as qtd
FROM lote as entrada
UNION ALL
SELECT saida.id_peca as id,
-saida.quantidade as qtd
FROM peca_x_pedido as saida
JOIN pedido ON saida.id_pedido = pedido.id_pedido
WHERE pedido.status IN ('Finalizado', 'AguardandoRetirada')) as combined
GROUP BY id_peca