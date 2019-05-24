SET FOREIGN_KEY_CHECKS=0;

DROP TABLE IF EXISTS medico;
CREATE TABLE medico (
  MedicoId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  PersonaId INT(11) NOT NULL,
  FOREIGN KEY(PersonaId) REFERENCES Persona(PersonaId) on DELETE no action on UPDATE CASCADE,  
  EspecialidadId INT(11) NOT NULL,
  FOREIGN KEY(EspecialidadId) REFERENCES Especialidad(EspecialidadId) on DELETE no action on UPDATE CASCADE,
  NumeroColegio VARCHAR(60) ,
  FechaColegiacion DATE ,
  TituloProfesional VARCHAR(120) ,
  Universidad VARCHAR(120) ,
  Estado bit(1) NOT NULL
  );

INSERT INTO `medico` (`PersonaId`, `EspecialidadId`, `NumeroColegio`, `FechaColegiacion`, `TituloProfesional`, `Universidad`,Estado) 
VALUES ('1', 1, '00912SDSD1212', '2017-11-13', 'Licenciado en Ginecologia', 'Universidad Nacional de San Cristobal ',1);

DROP TABLE IF EXISTS Especialidad;
CREATE TABLE `Especialidad` ( 
`EspecialidadId` INT(8) NOT NULL AUTO_INCREMENT , 
`Denominacion` VARCHAR(255) NOT NULL , 
PRIMARY KEY (`EspecialidadId`));

INSERT INTO `Especialidad` (`EspecialidadId`, `Denominacion`) VALUES (NULL, 'Gerencia de Servicios de Salud Reproductiva.'), (NULL, 'Atención Primaria de la Salud.'),(NULL, 'Emergencia y Alto Riesgo Obstétrico');

DROP TABLE IF EXISTS consultorio;
CREATE TABLE `consultorio` (
  `ConsultorioId` INT(11) NOT NULL AUTO_INCREMENT,
  `Denominacion` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`ConsultorioId`));

INSERT INTO `consultorio` (`ConsultorioId`, `denominacion`) VALUES ('0', 'Consultorio01'), ('0', 'Consultorio02');


	DROP TABLE IF EXISTS paciente;
	CREATE TABLE paciente(
    `PacienteId` INT(11) NOT NULL AUTO_INCREMENT,
    `PersonaId` INT(11) NOT NULL,
	`NumeroHistoria` VARCHAR(45) ,
	`Sangre` VARCHAR(50) ,
    `Alergia` VARCHAR(255) ,
    `AntecedentePersonal` VARCHAR(255) ,
    `AntecedenteFamiliar` VARCHAR(255) ,
    PRIMARY KEY(`PacienteId`),
    CONSTRAINT `PersonaId` FOREIGN KEY(`PersonaId`) REFERENCES `clinica`.`persona`(`PersonaId`) ON DELETE NO ACTION ON UPDATE NO ACTION
);


  DROP TABLE IF EXISTS programacion;
  CREATE TABLE programacion (
  ProgramacionId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  MedicoId int(11) NOT NULL,
  FOREIGN KEY(MedicoId) REFERENCES Medico(MedicoId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  FechaInicio date NOT NULL,
  FechaLimite date NULL,
  HoraInicio time NOT NULL,
  HoraFin time NOT NULL,
  Estado bit(1) DEFAULT NULL,
  Repite bit(1) DEFAULT NULL,
  Semanal bit(1) DEFAULT NULL
);

 DROP TABLE IF EXISTS Cita;
  CREATE TABLE Cita (
  CitaId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  PacienteId int(11) NOT NULL,
  FOREIGN KEY(PacienteId) REFERENCES Paciente(PacienteId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ProgramacionId int(11) NOT NULL,
  FOREIGN KEY(ProgramacionId) REFERENCES Programacion(ProgramacionId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ConceptoPagoId int(11) NOT NULL,
  FOREIGN KEY(ConceptoPagoId) REFERENCES ConceptoPago(ConceptoPagoId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  Estado char(1) NOT NULL,
  IndPago bit(1) NOT NULL,
  NumeroAtencion int(11) NOT NULL,
  HoraProbable time NOT NULL,
  FechaAtencion DATE NOT NULL
);


 DROP TABLE IF EXISTS TablaExamen;
  CREATE TABLE TablaExamen (
  TablaId int(11) ,
  ItemId int(11) ,
  Denominacion VARCHAR(255) NOT NULL,
  Unidad VARCHAR(50),
  Estado bit(1) NOT NULL default 1,
  IndLab bit(1) NOT NULL default 0,
  primary key(TablaId,ItemId)
);

INSERT INTO TablaExamen VALUES (1,0,'FUNCIONES BIOLOGICAS','',1,0);
INSERT INTO TablaExamen VALUES (1,1,'Peso','Kg.',1,0);
INSERT INTO TablaExamen VALUES (1,2,'Talla','cm.',1,0);
INSERT INTO TablaExamen VALUES (2,0,'FUNCIONES VITALES','',1,0);
INSERT INTO TablaExamen VALUES (2,1,'PRESION ARTERIAL','mmHg',1,0);
INSERT INTO TablaExamen VALUES (2,2,'FRECUENCIA RESPIRATORIA','/min.',1,0);
INSERT INTO TablaExamen VALUES (2,3,'FRECUENCIA CARDIACA','/min.',1,0);
INSERT INTO TablaExamen VALUES (2,4,'TEMPERATURA','°C',1,0);

INSERT INTO TablaExamen VALUES (3,0,'EXAMEN ORINA','',1,1);
INSERT INTO TablaExamen VALUES (3,1,'valor 1','nn',1,1);
INSERT INTO TablaExamen VALUES (3,2,'otro valor','nn',1,1);


 DROP TABLE IF EXISTS ATENCION;
  CREATE TABLE ATENCION (
  AtencionId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  CitaId int(11) ,
  FOREIGN KEY(CitaId) REFERENCES Cita(CitaId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  Estado char(1) DEFAULT NULL,
  PerPacienteId  int (11) not null,
  FOREIGN KEY(PerPacienteId) REFERENCES Persona(PersonaId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  PerMedicoId  int (11) not null,
  FOREIGN KEY(PerMedicoId) REFERENCES Persona(PersonaId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  Fecha date not null,
  EspecialidadId int(11) not null,
  FOREIGN KEY(EspecialidadId) REFERENCES Especialidad(EspecialidadId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  FechaCreacion datetime not null,
  FechaModificacion Datetime,
  UsuarioCId int(11) not null,
  FOREIGN KEY(UsuarioCId) REFERENCES Usuario(UsuarioId) ON DELETE NO ACTION ON UPDATE NO ACTION,
   UsuarioMId int(11) ,
  FOREIGN KEY(UsuarioMId) REFERENCES Usuario(UsuarioId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  IndPago bit(1) not null,
  Motivo text,
  Diagnostico text,
  Tratamiento text
);


 DROP TABLE IF EXISTS Examen;
  CREATE TABLE Examen (
  AtencionId int(11) NOT NULL,
  TablaId int(11) NOT NULL,
  ItemId int(11) NOT NULL,
  FOREIGN KEY(AtencionId) REFERENCES Atencion(AtencionId) ON DELETE NO ACTION ON UPDATE NO ACTION,
	Denominacion VARCHAR(255) NOT NULL,
	Valor VARCHAR(255) NOT NULL,
	primary key(AtencionId,TablaId,ItemId)
);

DROP TABLE IF EXISTS OrdenLab;
  CREATE TABLE OrdenLab (
  OrdenLabId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,  
  AtencionId int(11) NOT NULL,
  FOREIGN KEY(AtencionId) REFERENCES Atencion(AtencionId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  TablaId int(11) NOT NULL,
  Denominacion VARCHAR(255) NOT NULL,
  Estado CHAR(3) NOT NULL
);

DROP TABLE IF EXISTS ATENCIONESPECIALIDAD;
  CREATE TABLE atencionespecialidad (
  AtencionespecialidadId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
	AtencionId int(11) NOT NULL,
  FOREIGN KEY(AtencionId) REFERENCES Atencion(AtencionId) ON DELETE NO ACTION ON UPDATE NO ACTION,
	Item int(11) NOT NULL,
	Denominacion VARCHAR(255) NOT NULL,
	Valor VARCHAR(255) NOT NULL
);

INSERT INTO atencion(AtencionId, Estado, PerPacienteId, PerMedicoId, Fecha, EspecialidadId, FechaCreacion, UsuarioCId,IndPago) 
VALUES (1, 'P', 1,1,'2018-05-02',2,'2018-05-02',1,1);



