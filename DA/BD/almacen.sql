SET FOREIGN_KEY_CHECKS=0;

DROP TABLE IF EXISTS Marca;
CREATE TABLE Marca (
  MarcaId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Denominacion varchar(100) NOT NULL
);
DROP TABLE IF EXISTS Modelo;
CREATE TABLE Modelo (
  ModeloId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  MarcaId int(11) NOT NULL,
  FOREIGN KEY(MarcaId) REFERENCES Marca(MarcaId) on DELETE no action on UPDATE CASCADE,
  Denominacion varchar(100) NOT NULL
);
DROP TABLE IF EXISTS TipoArticulo;
CREATE TABLE TipoArticulo (
  TipoArticuloId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Denominacion varchar(100) NOT NULL
);
DROP TABLE IF EXISTS Articulo;
CREATE TABLE Articulo (
  ArticuloId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  TipoArticuloId int(11) NOT NULL,
  FOREIGN KEY(TipoArticuloId) REFERENCES TipoArticulo(TipoArticuloId) on DELETE no action on UPDATE CASCADE,
  ModeloId int(11) NOT NULL,
  FOREIGN KEY(ModeloId) REFERENCES Modelo(ModeloId) on DELETE no action on UPDATE CASCADE,
  Codigo varchar(100) NOT NULL,
  Denominacion varchar(100) NOT NULL,
  Precio decimal(15,2) not NULL default 0,
  Stock int(11) not null,
  IndServicio bit(1) not null,
  Estado bit(1) not null
);


