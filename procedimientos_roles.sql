--Insertamos roles--
Insert into rol(nombre) values('Administrador');
Insert into rol(nombre) values('Vendedor');
Insert into rol(nombre) values('Alamcenero');

---------------------------------
create proc Rol_Listar
as
select idrol, nombre from rol 
where estado=1
go

exec Rol_Listar