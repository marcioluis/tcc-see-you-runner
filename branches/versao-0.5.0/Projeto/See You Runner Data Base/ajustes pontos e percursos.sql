/**
 Alteraçoes feiras dia 24/10 tabelas percurso e pontos
**/
 
use tst_seeyourunner
go

-- Aletraçoes na tabela pontos
alter table syr_owner.pontos
drop column velocidade_med, velocidade_max
go

alter table syr_owner.pontos
add velocidade float
go

sp_Rename 'syr_owner.pontos.vo2max','Pace','COLUMN'
go

alter table syr_owner.pontos
add isMetrico nvarchar(1) default 's'
go

-- Alter percursos
alter table syr_owner.percursos
add isMetrico nvarchar(1) default 's'
go

sp_Rename 'syr_owner.percursos.vo2max','Pace','COLUMN'
go


