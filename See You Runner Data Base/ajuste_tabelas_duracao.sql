use tst_seeyourunner
go

alter table syr_owner.pontos
add duracao int null
go

alter table syr_owner.percursos
add duracao nvarchar(8) null
go