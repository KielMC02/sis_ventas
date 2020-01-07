/*PROCEDIMIENTOS DE ALMACENADO PARA LA ENTIDAD CATEGORIA*/
--Procedimientos Listar

/*Este procedimiento se encarga de listar todas las categorias y las organiza de las mas reciente
a la mas antigua.*/
create proc categoria_listar
as
select idcategoria as ID,nombre as Nombre,descripcion as Descripcion,estado as Estado
from categoria
order by idcategoria desc
exec categoria_listar
go

--Procedimientos Buscar
/*Este Procedimiento se encarga de filtrar las categorias por un estring que inserta el usuario del sistema, el la variable que recibe almacena 
el string se llama valor y tiene un maximo de 50 caracteres, utilizamos la clausula (Like '%' + '%') para indicar que muestre todos los registro donde
coincida el el String*/
create proc categoria_buscar
@valor varchar(50)
as
select idcategoria as ID,nombre as Nombre,descripcion as Descripcion,estado as Estado
from categoria 
where nombre like '%' + @valor + '%' or descripcion like '%' + @valor + '%'
order by nombre desc
exec categoria_listar
go

--Procedimientos Insertar
/*Este procedimiento me permite insertar registros en la tabla categoria, se declaran dos variables las cuales deben ser del mismo tipo y tamano 
que los campos para los cuales van a ser utilizadas*/
create proc categoria_insertar
@nombre varchar(50),
@descripcion varchar(255)
as
insert into categoria (nombre,descripcion) values (@nombre,@descripcion)
go

--Procedimientos Actualizar
/*Este procedimiento actualiza los registros de las categorias que correspondan con el ID que el usuairo nos envia desde la aplicacion*/
create proc categoria_actualizar
@idcategoria int,
@nombre varchar(50),
@descripcion varchar(255)
as
update categoria set nombre=@nombre,descripcion=@descripcion
where idcategoria = @idcategoria
go
--Procedimientos Eliminar
/*Este procedimiento se encarga de eliminar  la categoria que corresponde al ID insertado*/
create proc categoria_eliminar
@idcategoria int
as
delete from categoria
where idcategoria=@idcategoria
go

--Procedimientos Desactivar
/*Procedimiento de almacenada para desactivar categorias segun el ID que le pasen*/
create proc categoria_desactivar
@idcategoria int
as
update categoria set estado=0
where idcategoria = @idcategoria
go
--Procedimientos Activar
/*Este procedimiento activa las categorias segun el id suministrado*/
create proc categoria_activar
@idcategoria int
as
update categoria set estado=1
where idcategoria = @idcategoria
go


/*Este procedimiento se encarga si una categoria existe a partir de su nombre, en xaso de que no exista nos retorna 0 o 1*/
create proc categoria_existe
@valor varchar(100),
@existe bit output
as
/*Estructura condicional que verifica si la categoria existe, se utiliza ltrim & rtrim para limpiar los espacios de la cariable
	valor*/
	if exists(select nombre from categoria where nombre = ltrim(rtrim(@valor)))
		begin
			set @existe = 1
		end
	else
		begin
			set @existe = 0
		end