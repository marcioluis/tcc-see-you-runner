use seeyourunner
go

alter table pontos
add altitude float null
go

alter table pontos
drop column altitude_max, altitude_min, altitude_med
go

alter table percursos
add constraint default_nome_percurso default 'Percurso de ' + convert(nvarchar(max), getdate(), 13) for percurso
go