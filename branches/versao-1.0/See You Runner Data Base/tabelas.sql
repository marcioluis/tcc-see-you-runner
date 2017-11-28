-- Geração de Modelo físico
-- Sql ANSI 2003 - brModelo.


CREATE TABLE permissoes (
id_permissao BIGINT IDENTITY(1, 1),
escrita NCHAR(1) CONSTRAINT default_nao_escrita DEFAULT 'N',
leitura_escrita NCHAR(1) CONSTRAINT default_n_ler_escreve DEFAULT 'N',
leitura NCHAR(1) CONSTRAINT default_sim_ler DEFAULT 'S'
)

CREATE TABLE funcoes (
id_funcao INT IDENTITY(1, 1),
funcao NVARCHAR(80),
data_criacao DATETIME CONSTRAINT default_date_funcoes DEFAULT GETDATE(),
descricao NVARCHAR(250)
)

CREATE TABLE conquistas (
id_conquista BIGINT IDENTITY(1, 1),
data_conquista DATETIME CONSTRAINT default_date_conquista DEFAULT GETDATE(),
descricao NVARCHAR(255),
conquista NVARCHAR(150)
)

CREATE TABLE usuarios (
id_usuario BIGINT IDENTITY(1, 1),
data_cadastro DATETIME CONSTRAINT default_date_usuarios DEFAULT GETDATE(),
data_nascimento DATETIME,
sexo NCHAR(1),
altura DECIMAL(3,2),
sobrenome NVARCHAR(80),
ativo NCHAR(1) CONSTRAINT default_sim_ativo DEFAULT 'S',
senha NVARCHAR(MAX),
login NVARCHAR(80),
nome NVARCHAR(80)
)

CREATE TABLE percursos (
id_percurso BIGINT IDENTITY(1, 1),
id_usuario BIGINT,
data_percurso DATETIME CONSTRAINT default_date_pm DEFAULT GETDATE(),
percurso NVARCHAR(150),
descricao NVARCHAR(2048),
compartilhado NCHAR(1) CONSTRAINT default_nao_compartilhar DEFAULT 'N',
caloria FLOAT,
distancia FLOAT,
altitude_min FLOAT,
altitude_med FLOAT,
altitude_max FLOAT,
vo2max FLOAT,
velocidade_med FLOAT,
velocidade_max FLOAT,
imagem VARBINARY(MAX)
)

CREATE TABLE imagens (
id_imagem BIGINT IDENTITY(1, 1),
id_usuario BIGINT,
data_criacao DATETIME CONSTRAINT default_date_imagens DEFAULT GETDATE(),
imagem VARBINARY(MAX),
nome_imagem NVARCHAR(150),
mime NVARCHAR(100)
)

CREATE TABLE pontos (
id_ponto BIGINT IDENTITY(1, 1),
id_percurso BIGINT,
data_ponto DATETIME CONSTRAINT default_date_pontos DEFAULT GETDATE(),
latitude NVARCHAR(80),
longitude NVARCHAR(80)
--,
--geografia GEOGRAPHY,
--geografia_wkt AS geografia.STAsText()
)

CREATE TABLE comentarios (
id_comentarios BIGINT IDENTITY(1, 1),
id_usuario BIGINT,
id_percurso BIGINT,
data_comentario DATETIME CONSTRAINT default_date_comentarios DEFAULT GETDATE(),
comentario NVARCHAR(MAX)
)

CREATE TABLE usuarios_conquistas (
id_usuarios_conquistas BIGINT IDENTITY(1, 1),
id_conquista BIGINT,
id_usuario BIGINT
)

CREATE TABLE usuarios_grupos (
id_usuarios_grupos BIGINT IDENTITY(1, 1),
id_grupo BIGINT,
id_usuario BIGINT
)

CREATE TABLE funcoes_usuarios (
id_funcoes_usuarios BIGINT IDENTITY(1, 1),
id_usuario BIGINT,
id_funcao INT
)

CREATE TABLE grupos (
id_grupo BIGINT IDENTITY(1, 1),
descricao NVARCHAR(2048),
data_criacao DATETIME CONSTRAINT default_date_grupos DEFAULT GETDATE(),
grupo NVARCHAR(150)
)

--pks
--ALTER TABLE permissoes ADD CONSTRAINT id_permissao_pk PRIMARY KEY(id_permissao)
ALTER TABLE funcoes ADD CONSTRAINT id_funcao_pk PRIMARY KEY(id_funcao)
ALTER TABLE conquistas ADD CONSTRAINT id_conquista_pk PRIMARY KEY(id_conquista)
ALTER TABLE usuarios ADD CONSTRAINT id_usuario_pk PRIMARY KEY(id_usuario)
ALTER TABLE percursos ADD CONSTRAINT id_percurso_pk PRIMARY KEY(id_percurso)
ALTER TABLE imagens ADD CONSTRAINT id_imagem_pk PRIMARY KEY(id_imagem)
ALTER TABLE pontos ADD CONSTRAINT id_ponto_pk PRIMARY KEY(id_ponto)
ALTER TABLE comentarios ADD CONSTRAINT id_comentarios_pk PRIMARY KEY(id_comentarios)
ALTER TABLE usuarios_conquistas ADD CONSTRAINT id_usuarios_conquistas_pk PRIMARY KEY(id_usuarios_conquistas)
ALTER TABLE usuarios_grupos ADD CONSTRAINT id_usuarios_grupos_pk PRIMARY KEY(id_usuarios_grupos)
ALTER TABLE funcoes_usuarios ADD CONSTRAINT id_funcoes_usuarios_pk PRIMARY KEY(id_funcoes_usuarios)
ALTER TABLE grupos ADD CONSTRAINT id_grupo_pk PRIMARY KEY(id_grupo)

--fks
--ALTER TABLE usuarios ADD CONSTRAINT id_usuarios_permissoes_fk FOREIGN KEY(id_permissao) REFERENCES permissoes (id_permissao)
ALTER TABLE percursos ADD CONSTRAINT id_percurso_usuarios_fk FOREIGN KEY(id_usuario) REFERENCES usuarios (id_usuario) ON DELETE CASCADE
ALTER TABLE imagens ADD CONSTRAINT id_imagem_usuarios_fk FOREIGN KEY(id_usuario) REFERENCES usuarios (id_usuario) ON DELETE CASCADE
ALTER TABLE pontos ADD CONSTRAINT id_pontos_percursos_fk FOREIGN KEY(id_percurso) REFERENCES percursos (id_percurso) ON DELETE CASCADE
ALTER TABLE comentarios ADD CONSTRAINT id_comentarios_usuarios_fk FOREIGN KEY(id_usuario) REFERENCES usuarios (id_usuario) ON DELETE CASCADE
ALTER TABLE comentarios ADD CONSTRAINT id_comentarios_percursos_fk FOREIGN KEY(id_percurso) REFERENCES percursos (id_percurso)
ALTER TABLE usuarios_conquistas ADD CONSTRAINT id_usarios_conq_conquistas_fk FOREIGN KEY(id_conquista) REFERENCES conquistas (id_conquista)
ALTER TABLE usuarios_conquistas ADD CONSTRAINT id_usarios_conq_usuarios_fk FOREIGN KEY(id_usuario) REFERENCES usuarios (id_usuario) ON DELETE CASCADE
ALTER TABLE usuarios_grupos ADD CONSTRAINT id_usarios_grupos_grupos_fk FOREIGN KEY(id_grupo) REFERENCES grupos (id_grupo) ON DELETE CASCADE
ALTER TABLE usuarios_grupos ADD CONSTRAINT id_usarios_grupos_usuarios_fk FOREIGN KEY(id_usuario) REFERENCES usuarios (id_usuario) ON DELETE SET NULL
ALTER TABLE funcoes_usuarios ADD CONSTRAINT id_funcoes_usuarios_usuarios_fk FOREIGN KEY(id_usuario) REFERENCES usuarios (id_usuario) ON DELETE CASCADE
ALTER TABLE funcoes_usuarios ADD CONSTRAINT id_funcoes_usuarios_funcoes_fk FOREIGN KEY(id_funcao) REFERENCES funcoes (id_funcao)
