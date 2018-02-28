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





DROP TABLE IF EXISTS programacion;
	CREATE TABLE `programacion` (
	  `ProgramacionId` int(11) NOT NULL AUTO_INCREMENT,
	  `PersonaId` int(11) NOT NULL,
	  `ConsultorioId` int(11) NOT NULL,
	  `FechaLimite` date NOT NULL,
	  `Estado` bit(1) NOT NULL,
	  `Repite` bit(1) NOT NULL,
	  `SemanaInicio`  int(11) NOT NULL,
	  `SemanaFin`  int(11) NOT NULL,
	  `Lunes` varchar(45) NOT NULL,
	  `Martes` varchar(45) NOT NULL,
	  `Miercoles` varchar(45) NOT NULL,
	  `Jueves` varchar(45) NOT NULL,
	  `Viernes` varchar(45) NOT NULL,
	  `Sabado` varchar(45) NOT NULL,
	  `Domingo` varchar(45) NOT NULL,
	  PRIMARY KEY (`ProgramacionId`),
	  KEY `PersonaId` (`PersonaId`),
	  KEY `ConsultorioId` (`ConsultorioId`),
	  CONSTRAINT `programacion_ibfk_1` FOREIGN KEY (`PersonaId`) REFERENCES `persona` (`PersonaId`) ON DELETE NO ACTION ON UPDATE CASCADE,
	  CONSTRAINT `programacion_ibfk_2` FOREIGN KEY (`ConsultorioId`) REFERENCES `consultorio` (`ConsultorioId`) ON DELETE NO ACTION ON UPDATE CASCADE
	) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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

