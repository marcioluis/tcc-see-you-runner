use tst_seeyourunner
go
alter table syr_owner.pontos
add caloria float null
go
alter table syr_owner.pontos
add distancia float null
go
alter table syr_owner.pontos
add altitude_min float null
go
alter table syr_owner.pontos
add altitude_med float null
go
alter table syr_owner.pontos
add altitude_max float null
go
alter table syr_owner.pontos
add vo2max float null
alter table syr_owner.pontos
add velocidade_med float null
go
alter table syr_owner.pontos
add velocidade_max float null
go
alter table syr_owner.pontos
drop column geografia
go
alter table syr_owner.pontos
drop column geografia_wkt
go