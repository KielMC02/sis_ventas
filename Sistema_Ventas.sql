use dbsistema
go
/*Tabla Categoria No. 1*/
create table categoria (
	idcategoria integer identity(1,1) primary key,
	nombre varchar(50) not null unique,
	descripcion varchar(255) null,
	estado bit default(1)
);
/*----------Insertar Registros----------*/
insert into categoria(nombre,descripcion) values('Dispositivos de Compputos','Dispositivos relacionados con los ordenadores')
select * from categoria
/*---------------------------------*/

/*----------------Tabla Articulo-----------------*/
create table articulo
(
	idarticulo integer identity(1,1) primary key,
	idcategoria integer not null,
	codigo varchar(50) null,
	nombre varchar(100) not null,
	precio_venta decimal(11,2) not null,
	stock integer not null,
	descripcion varchar(255) null,
	imagen varchar(20) null,
	estado bit default(1),
	FOREIGN KEY (idcategoria) REFERENCES categoria(idcategoria)
)
/*----------Insertar Registros----------*/
/*---------------------------------*/

/*----------------Tabla Articulo-----------------*/
create table persona
(
	idpersona integer primary key identity(1,1),
	tipo_persona varchar(20) not null,
	nombre varchar(100) not null,
	tipo_documento varchar(20) null,
	num_documento varchar (20) null,
	direccion varchar(70) null,
	telefono varchar(20) null,
	email varchar(50) null
)
/*----------Insertar Registros----------*/
/*---------------------------------*/

/*----------------Tabla Rol-----------------*/
create table rol
(
	idrol integer primary key identity(1,1),
	nombre varchar(30) not null,
	descripion varchar (255) null,
	estado bit default(1)
)
/*----------Insertar Registros----------*/
/*---------------------------------*/

/*----------------Tabla Usuario-----------------*/
create table usuario
(
	idusuario int primary key identity,
	idrol integer not null,
	nombre varchar(100) not null,
	tipo_documento varchar(20) null,
	num_documento varchar(20) null,
	direccion varchar(70) null,
	telefono varchar(20)null,
	email varchar(50) not null,
	clave varbinary(max) not null,
	estado bit default(1),
	FOREIGN KEY (idrol) REFERENCES rol (idrol)
);
go
/*----------------Tabla Ingreso-----------------*/
create table ingreso
(
	idingreso integer primary key identity(1,1),
	idproveedor integer not null,
	idusuario integer not null,
	tipo_comprobante varchar(7) not null,
	serie_comprobante varchar(7) null,
	num_comprobante varchar(10) not null,
	fecha datetime not null,
	impuesto decimal (4,2) not null,
	total decimal (11,2)not null,
	estado varchar(20)not null,
	FOREIGN KEY (idproveedor) REFERENCES persona (idpersona),
	FOREIGN KEY (idusuario) REFERENCES usuario (idusuario)
);
go
/*----------------Tabla detalle Ingreso-----------------*/
create table detalle_ingreso
(
	iddetalle_ingreso integer primary key identity(1,1),
	idingreso integer not null,
	idarticulo integer not null,
	cantidad integer not null,
	precio decimal(11,2) not null
	FOREIGN KEY (idingreso)References ingreso(idingreso) ON DELETE CASCADE,
	FOREIGN KEY (idarticulo) REFERENCES articulo(idarticulo)
);
go
/*----------------Tabla Ventas-----------------*/
create table venta
(
	idventa integer primary key identity(1,1),
	idcliente integer not null,
	idusuario integer not null,
	tipo_comprobante varchar(10) not null,
	serie_comprobante varchar(7) null,
	num_comprobante varchar(10) not null,
	fecha datetime not null, 
	impuesto decimal(4,2) not null,
	total decimal (11,2) not null,
	estado varchar(20) not null,
	FOREIGN KEY (idcliente) REFERENCES persona (idpersona),
	FOREIGN KEY (idusuario) REFERENCES usuario (idusuario)
);
go

/*----------------Tabla Detalle de Ventas-----------------*/
create table detalle_venta
(
	iddetalle_venta integer primary key identity(1,1),
	idventa integer not null,
	idarticulo integer not null,
	cantidad integer not null,
	precio decimal(11,2) not null,
	descuento decimal(11,2) not null,
	FOREIGN KEY (idventa) REFERENCES venta(idventa)ON DELETE CASCADE,
	FOREIGN KEY(idarticulo) REFERENCES articulo(idarticulo)
);
go