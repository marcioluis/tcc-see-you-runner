/*
Recuperar os dados criptografados:
select * from tabela where senha = HASHBYTES('MD5','senha123')
*/

/********************
Precisa trocar o nome do banco de dados de tst_seeyourunner para algo existente
********************/
SET DATEFORMAT dmy;
GO

PRINT 'inserindo dados dos usuários'
BEGIN TRANSACTION dados_usuarios;
GO

SET IDENTITY_INSERT tst_seeyourunner.syr_owner.usuarios ON;
GO

INSERT INTO tst_seeyourunner.syr_owner.usuarios 
(id_usuario, nome, login, senha, sobrenome, altura, sexo, data_nascimento) 
VALUES 
(1, 'Marcio Luis', 'marcio@email.com', HASHBYTES('MD5', 'marcio123') , 'S. Arrosi', 1.77, UPPER('m'), CONVERT(datetime, '19/07/1989', 103)  );
GO
INSERT INTO tst_seeyourunner.syr_owner.usuarios 
(id_usuario, nome, login, senha, sobrenome, altura, sexo, data_nascimento) 
VALUES 
(2, 'Marcos Michel', 'marcos@email.com', HASHBYTES('MD5', 'marcos123') , 'N. Borba', 1.77, UPPER('m'), CONVERT(datetime, '19/07/1989', 103) );
GO
INSERT INTO tst_seeyourunner.syr_owner.usuarios 
(id_usuario, nome, login, senha, sobrenome, altura, sexo, data_nascimento) 
VALUES 
(3, 'Leonard', 'nimoy@enterprise.com', HASHBYTES('MD5', 'nimoy123') , 'Nimoy', 1.80, UPPER('m'), CONVERT(datetime, '01/05/1964', 103));
GO
INSERT INTO tst_seeyourunner.syr_owner.usuarios 
(id_usuario, nome, login, senha, sobrenome, altura, sexo, data_nascimento) 
VALUES 
(4, 'Natalia', 'natygatinha@email.com', HASHBYTES('MD5', 'gatinha123') , 'Gatinha', 1.55, UPPER('f'), CONVERT(datetime, '22/07/1990', 103));
GO
INSERT INTO tst_seeyourunner.syr_owner.usuarios 
(id_usuario, nome, login, senha, sobrenome, altura, sexo, data_nascimento) 
VALUES 
(5, 'Gabriela', 'gabriela_santos@yahuu.com.br', HASHBYTES('MD5', 'santos123') , 'Santos', 1.49, UPPER('f'), CONVERT(datetime, '29/01/1974', 103));
GO
INSERT INTO tst_seeyourunner.syr_owner.usuarios 
(id_usuario, nome, login, senha, sobrenome, altura, sexo, data_nascimento) 
VALUES 
(6, 'Pamela H.', 'pamh@email.com', HASHBYTES('MD5', 'silva123') , 'Silva', 1.62, UPPER('f'), CONVERT(datetime, '30/11/1985', 103));
GO

SET IDENTITY_INSERT tst_seeyourunner.syr_owner.usuarios OFF;
GO

COMMIT TRANSACTION dados_usuarios;
GO


/*
Inserindo percursos para os usuários
*/
PRINT 'Inserindo percursos para os usuários'
BEGIN TRANSACTION dados_percursos;
GO

SET IDENTITY_INSERT tst_seeyourunner.syr_owner.percursos ON;
GO

--percursos de marcio
INSERT INTO tst_seeyourunner.syr_owner.percursos 
(id_percurso, id_usuario, percurso, descricao, caloria, distancia, altitude_min, altitude_max, altitude_med, vo2max, velocidade_med, velocidade_max, imagem) 
VALUES 
(1, 1, 'Percurso na Nilo Peçanha', 'meu percurso de testes na Nilo Peçanha', 524.0, 5324.0, 1, 5, 10, 50, 15.3, 23.4, NULL);
GO
INSERT INTO tst_seeyourunner.syr_owner.percursos
(id_percurso, id_usuario, percurso, descricao, caloria, distancia, altitude_min, altitude_max, altitude_med, vo2max, velocidade_med, velocidade_max, imagem) 
VALUES 
(2, 1, 'Corrida na Azenha', 'meu percurso de testes na Azenha', 124.0, 3024.0, 5, 5, 5, 25, 10, 15, NULL);
GO
INSERT INTO tst_seeyourunner.syr_owner.percursos
(id_percurso, id_usuario, percurso, descricao, caloria, distancia, altitude_min, altitude_max, altitude_med, vo2max, velocidade_med, velocidade_max, imagem) 
VALUES 
(3, 1, 'Maratona de Porto Alegre', 'Maratona do aniversário de Porto Alegre', 1321.0, 40301.0, 5, 10, 7.6, 55, 13, 21, NULL);
GO

SET IDENTITY_INSERT tst_seeyourunner.syr_owner.percursos OFF;
GO

COMMIT TRANSACTION dados_percursos;
GO

/*
Inserindo pontos para um percurso
*/
PRINT 'Inserindo pontos para um percurso'
BEGIN TRANSACTION dados_pontos;
GO

--pontos dos percursos 1
INSERT INTO tst_seeyourunner.syr_owner.pontos (id_percurso, latitude, longitude) 
VALUES (1, '47.64054', '-120.12934');
GO
INSERT INTO tst_seeyourunner.syr_owner.pontos (id_percurso, latitude, longitude) 
VALUES (1, '47.64054', '-120.13');
GO
INSERT INTO tst_seeyourunner.syr_owner.pontos (id_percurso, latitude, longitude) 
VALUES (1, '47.64054', '-120.14');
GO

--pontos percurso 2
INSERT INTO tst_seeyourunner.syr_owner.pontos (id_percurso, latitude, longitude) 
VALUES (2, '47.64054', '-120.12934');
GO
INSERT INTO tst_seeyourunner.syr_owner.pontos (id_percurso, latitude, longitude) 
VALUES (2, '47.65', '-120.12934');
GO
INSERT INTO tst_seeyourunner.syr_owner.pontos (id_percurso, latitude, longitude) 
VALUES (2, '47.65054', '-120.12934');
GO

--pontos percurso 3
INSERT INTO tst_seeyourunner.syr_owner.pontos (id_percurso, latitude, longitude) 
VALUES (3, '47.64054', '-120.12934');
GO
INSERT INTO tst_seeyourunner.syr_owner.pontos (id_percurso, latitude, longitude) 
VALUES (3, '47.65', '-120.12934');
GO
INSERT INTO tst_seeyourunner.syr_owner.pontos (id_percurso, latitude, longitude) 
VALUES (3, '47.65054', '-120.12934');
GO

COMMIT TRANSACTION dados_pontos;
GO
select * from tst_seeyourunner.syr_owner.pontos

--inserindo funcoes
PRINT 'inserindo funcoes'

BEGIN TRANSACTION funcoes;
GO

SET IDENTITY_INSERT tst_seeyourunner.syr_owner.funcoes ON;
GO

INSERT INTO tst_seeyourunner.syr_owner.funcoes (id_funcao, funcao, descricao) 
VALUES (1, upper('corredor'), 'Função de usuário normal do serviço, dito corredor.');
GO

SET IDENTITY_INSERT tst_seeyourunner.syr_owner.funcoes OFF;
GO

--inserindo em funcoes_usuarios
PRINT 'inserindo em funcoes_usuarios'
GO

INSERT INTO tst_seeyourunner.syr_owner.funcoes_usuarios (id_funcao, id_usuario) 
VALUES (1, 3);
GO

COMMIT TRANSACTION funcoes;
GO