/*Procedimientos de almacenado para la tabla articulo.*/
/*LISTAR*/
create proc articulo_listar
as
select  a.idarticulo as ID, a.idcategoria, c.nombre as Categoria,
a.codigo as Codigo, a.nombre as Nombre, a.precio_venta as Precio_Venta,
a.stock as Stock, a.descripcion as Descripcion, a.imagen as Imagen, a.estado as Estado
from articulo a inner join categoria c on a.idcategoria=c.idcategoria
order by a.idarticulo desc
exec articulo_listar
go
/*--------------------------------------------------------------*/
/*Buscar*/
create proc articulo_bsucar
@valor varchar(50)
as
select  a.idarticulo as ID, a.idcategoria, c.nombre as Categoria,
a.codigo as Codigo, a.nombre as Nombre, a.precio_venta as Precio_Venta,
a.stock as Stock, a.descripcion as Descripcion, a.imagen as Imagen, a.estado as Estado
from articulo a inner join categoria c on a.idarticulo=c.idcategoria
where a.nombre like '%' +@valor+ '%' or a.descripcion like '%' +@valor+ '%'
order by a.idarticulo asc
go
exec articulo_bsucar
/*--------------------------------------------------------------*/
/*Insertar*/
create proc articulo_insertar
@idcategoria integer,
@codigo varchar(50),
@nombre varchar(100),
@precio_venta decimal(11,2),
@stock integer,
@descripcion varchar(255),
@imagen varchar(20)
as
insert into articulo(idcategoria,codigo,nombre,precio_venta,stock,descripcion,imagen)
values(@idcategoria,@codigo,@nombre,@precio_venta,@stock,@descripcion,@imagen)
go
/*----------------------------------------*/
/*Actualizar*/
create proc articulo_actualizar
@idarticulo integer,
@idcategoria integer,
@codigo varchar(50),
@nombre varchar(100),
@precio_venta decimal(11,2),
@stock integer,
@descripcion varchar(255),
@imagen varchar(20)
as
update  articulo set idcategoria=@idcategoria,codigo=@codigo,nombre=@nombre,precio_venta=@precio_venta,stock=@stock,descripcion=@descripcion,imagen=@imagen
where idarticulo =  @idarticulo
go
/*----------------------------------------*/
/*Eliminar*/
create proc articulo_eliminar
@idarticulo integer
as
delete from articulo
where idarticulo = @idarticulo
go

/*----------------------------------------*/
/*Activar*/
create proc articulo_activar
@idarticulo integer
as
update articulo set estado = 1
where idarticulo = @idarticulo
go

/*----------------------------------------*/
/*Desactivar*/
create proc articulo_desactivar
@idarticulo integer
as
update articulo set estado = 0
where idarticulo = @idarticulo
go


/*----------------------------------------*/
/*Existe*/

/*Este procedimiento se encarga si un articulo existe a partir de su nombre, en caso de que no exista nos retorna 0 o 1*/
create proc articulo_existe
@valor varchar(100),
@existe bit output
as
/*Estructura condicional que verifica si la categoria existe, se utiliza ltrim & rtrim para limpiar los espacios de la cariable
	valor*/
	if exists(select nombre from articulo where nombre = ltrim(rtrim(@valor)))
		begin
			set @existe = 1
		end
	else
		begin
			set @existe = 0
		end