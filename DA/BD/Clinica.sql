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
CREATE TABLE paciente (
  PacienteId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  PersonaId INT(11) NOT NULL,
  FOREIGN KEY(PersonaId) REFERENCES Persona(PersonaId) on DELETE no action on UPDATE CASCADE,  
  Alergia VARCHAR(500) ,
  Estado bit(1) NOT NULL
  );


	--Creacion de tabla paciente
	DROP TABLE IF EXISTS paciente;
	CREATE TABLE `clinica`.`paciente`(
    `PacienteId` INT(11) NOT NULL AUTO_INCREMENT,
    `PersonaId` INT(11) NOT NULL,
	`NumeroHistoria` VARCHAR(45) NOT NULL,
    `Alergia` VARCHAR(255) NOT NULL,
    `AntecedentePersonal` VARCHAR(255) NOT NULL,
    `AntecedenteFamiliar` VARCHAR(255) NOT NULL,
    PRIMARY KEY(`PacienteId`),
    CONSTRAINT `PersonaId` FOREIGN KEY(`PersonaId`) REFERENCES `clinica`.`persona`(`PersonaId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE = InnoDB;


DROP TABLE IF EXISTS atencion;
CREATE TABLE atencion (
  AtencionId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  PacienteId INT(11) NOT NULL,
  FOREIGN KEY(PacienteId) REFERENCES Paciente(PacienteId) on DELETE no action on UPDATE CASCADE,  
  MedicoId INT(11) NOT NULL,
  FOREIGN KEY(MedicoId) REFERENCES Medico(MedicoId) on DELETE no action on UPDATE CASCADE,
  FechaColegiacion DATE NOT NULL,
  TituloProfesional VARCHAR(120),
  Universidad VARCHAR(120) ,
  Estado bit(1) NOT NULL
  );

  DROP TABLE IF EXISTS programacion;
  CREATE TABLE programacion (
  ProgramacionId int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  MedicoId int(11) NOT NULL,
  FOREIGN KEY(MedicoId) REFERENCES Medico(MedicoId) ON DELETE NO ACTION ON UPDATE NO ACTION,
  FechaInicio date NOT NULL,
  FechaLimite date DEFAULT NULL,
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

