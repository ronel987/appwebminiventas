CREATE DATABASE BDMiniVentas
GO

USE BDMiniVentas
GO

-- Tables

-- Table: venta
CREATE TABLE venta (
    Id				INT NOT NULL IDENTITY(1, 1),
    IdEmpleado		CHAR(8) NOT NULL,
    Total			DECIMAL(10,2) NOT NULL,
    FechaRegistro	DATETIME NOT NULL DEFAULT getdate(),
    Estado			BIT NOT NULL DEFAULT 1,
    CONSTRAINT venta_pk PRIMARY KEY(Id)
)
GO

-- Table: venta_detalle
CREATE TABLE venta_detalle (
    Id				INT NOT NULL IDENTITY(1, 1),
    IdVenta			INT NOT NULL,
    IdProducto		INT NOT NULL,
    PrecioUnidad	DECIMAL(10,2) NOT NULL,
    Cantidad		INT NOT NULL,
    SubTotal		DECIMAL(10,2) NOT NULL,
    CONSTRAINT venta_detalle_pk PRIMARY KEY(Id)
)
GO

-- Table: producto
CREATE TABLE producto (
    Id					INT NOT NULL IDENTITY(1, 1),
    Nombre				VARCHAR(50) NOT NULL,
    Marca				VARCHAR(50) NOT NULL,
    Descripcion			VARCHAR(255) NULL,
    Precio				DECIMAL(10,2) NOT NULL,
    Stock				INT NOT NULL,
    FechaRegistro		DATETIME NOT NULL DEFAULT getdate(),
    Estado				BIT NOT NULL DEFAULT 1,
    CONSTRAINT producto_pk PRIMARY KEY(Id)
)
GO

-- Table: categoria
CREATE TABLE categoria (
    Id				INT NOT NULL IDENTITY(1, 1),
    Nombre			VARCHAR(50) NOT NULL,
    FechaRegistro	DATETIME NOT NULL DEFAULT getdate(),
    CONSTRAINT categoria_pk PRIMARY KEY(Id)
)
GO

--Table: producto_categoria
CREATE TABLE producto_categoria (
    Id				INT NOT NULL IDENTITY(1, 1),
    IdProducto		INT NOT NULL,
    IdCategoria		INT NOT NULL,
    CONSTRAINT producto_categoria_pk PRIMARY KEY(Id)
)
GO

-- Table: empleado
CREATE TABLE empleado (
    Id				CHAR(8) NOT NULL,
    Nombre			VARCHAR(50) NOT NULL,
    Apellido		VARCHAR(50) NOT NULL,
    Alias			VARCHAR(12) NOT NULL,
    Clave			VARCHAR(20) NOT NULL,
    Telefono		VARCHAR(20) NULL,
    Correo			VARCHAR(50) NULL,
    FechaRegistro	DATETIME NOT NULL DEFAULT getdate(),
    Estado			BIT NOT NULL DEFAULT 1,
    CONSTRAINT empleado_pk PRIMARY KEY(Id)
)
GO

-- Foreing Keys

-- Reference: FK2(table: venta)
ALTER TABLE venta ADD CONSTRAINT FK2
    FOREIGN KEY(IdEmpleado)
    REFERENCES empleado(Id)
	GO

-- Reference: FK3(table: venta_detalle)
ALTER TABLE venta_detalle ADD CONSTRAINT FK3
    FOREIGN KEY(IdVenta)
    REFERENCES venta(Id)
	GO

-- Reference: FK4(table: venta_detalle)
ALTER TABLE venta_detalle ADD CONSTRAINT FK4
    FOREIGN KEY(IdProducto)
    REFERENCES producto(Id)
	GO

-- Reference: FK5(table: producto_categoria)
ALTER TABLE producto_categoria ADD CONSTRAINT FK5
    FOREIGN KEY(IdProducto)
    REFERENCES producto(Id)
	GO

-- Reference: FK6(table: producto_categoria)
ALTER TABLE producto_categoria ADD CONSTRAINT FK6
    FOREIGN KEY(IdCategoria)
    REFERENCES categoria(Id)
	GO

-- End of file.