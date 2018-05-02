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
	`NumeroHistoria` VARCHAR(45) NOT NULL,
    `Alergia` VARCHAR(255) NOT NULL,
    `AntecedentePersonal` VARCHAR(255) NOT NULL,
    `AntecedenteFamiliar` VARCHAR(255) NOT NULL,
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
  Estado bit(1) DEFAULT NULL,
  NumeroAtencion int(11) NOT NULL,
  HoraProbable time NOT NULL,
  FechaAtencion DATE NOT NULL
);

 DROP TABLE IF EXISTS tablaconfiguracion;
  CREATE TABLE tablaconfiguracion (
  TablaconfiguracionId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
	TablaId int(11) NOT NULL,
	Item int(11) NOT NULL,
	Denominacion VARCHAR(255) NOT NULL
);

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
   UsuarioMId int(11) not null,
  FOREIGN KEY(UsuarioMId) REFERENCES Usuario(UsuarioId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  IndPago bit(1) not null
);


 DROP TABLE IF EXISTS topico;
  CREATE TABLE topico (
  TopicoId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
	AtencionId int(11) NOT NULL,
  FOREIGN KEY(AtencionId) REFERENCES Atencion(AtencionId) ON DELETE NO ACTION ON UPDATE NO ACTION,
	Item int(11) NOT NULL,
	Denominacion VARCHAR(255) NOT NULL,
	Valor VARCHAR(255) NOT NULL
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

INSERT INTO atencion VALUES (1, NULL, 1, 1,1,'2018-05-02',2,'2018-05-02','2018-05-02',5,5,'p');
INSERT INTO atencion VALUES (2, NULL, 1, 1,1,'2018-05-02',2,'2018-05-02','2018-05-02',5,5,'p');
INSERT INTO atencion VALUES (3, NULL, 1, 1,1,'2018-05-02',2,'2018-05-02','2018-05-02',5,5,'p');


INSERT INTO tablaconfiguracion VALUES (1, 1,1,'P.A.');
INSERT INTO tablaconfiguracion VALUES (2, 1,2,'Temperatura');
INSERT INTO tablaconfiguracion VALUES (3, 1,3,'Pulso');
INSERT INTO tablaconfiguracion VALUES (4, 1,4,'Peso');
INSERT INTO tablaconfiguracion VALUES (5, 1,5,'Respiración');

INSERT INTO tablaconfiguracion VALUES (6, 2,1,'Menarquia');
INSERT INTO tablaconfiguracion VALUES (7, 2,2,'Inicio Relacion Sexuales');
INSERT INTO tablaconfiguracion VALUES (8, 2,3,'FUM');
INSERT INTO tablaconfiguracion VALUES (9, 2,4,'Gestante');
INSERT INTO tablaconfiguracion VALUES (10, 2,5,'FUP');
INSERT INTO tablaconfiguracion VALUES (11, 2,6,'G');
INSERT INTO tablaconfiguracion VALUES (12, 2,7,'P');

