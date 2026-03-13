CREATE DATABASE estoque_roupa;
USE estoque_roupa;

CREATE TABLE funcionario(
id_funcionario CHAR(7) PRIMARY KEY,
nome VARCHAR(100)
);

CREATE TABLE peca(
id_peca INT AUTO_INCREMENT PRIMARY KEY,
tipopeca ENUM('Camiseta', 'Calça', 'Calçado'),
tamanhopeca int
);

CREATE TABLE pedido(
id_pedido INT AUTO_INCREMENT PRIMARY KEY,
data DATE,
id_funcionario CHAR(7) NOT NULL,
status ENUM('Novo', 'EmProcesso', 'Finalizado'),
CONSTRAINT FK_id_funcionario FOREIGN KEY (id_funcionario)
  REFERENCES funcionario(id_funcionario)
);

CREATE TABLE retirada(
id_retirada INT AUTO_INCREMENT PRIMARY KEY,
id_pedido INT NOT NULL,
data DATE,
CONSTRAINT FK_id_pedido FOREIGN KEY (id_pedido)
REFERENCES pedido(id_pedido)
);

CREATE TABLE lote(
id_lote INT AUTO_INCREMENT PRIMARY KEY,
id_peca INT NOT NULL,
data DATE,
quantidade INT,
CONSTRAINT FK_id_peca FOREIGN KEY (id_peca)
REFERENCES peca(id_peca)
);

CREATE TABLE peca_x_pedido(
id_peca_x_pedido INT AUTO_INCREMENT PRIMARY KEY,
id_peca INT NOT NULL,
id_pedido INT NOT NULL,
quantidade INT,
CONSTRAINT FK_id_peca_pedido FOREIGN KEY (id_peca)
REFERENCES peca(id_peca),
CONSTRAINT FK_id_pedido_peca FOREIGN KEY (id_pedido)
REFERENCES pedido(id_pedido)
);



