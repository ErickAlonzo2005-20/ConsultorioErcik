CREATE DATABASE Consultorio_Denta_ERICK;
GO

USE Consultorio_Denta_ERICK;
GO

CREATE TABLE Paciente (
    PacienteID INT PRIMARY KEY IDENTITY(1,1), 
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    Telefono NVARCHAR(20)
);

CREATE TABLE Dentista (
    DentistaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Especialidad NVARCHAR(50) NOT NULL
);


CREATE TABLE Motivo (
    MotivoID INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(100) NOT NULL,
    Costo DECIMAL(10, 2) NOT NULL
);


CREATE TABLE Cita (
    CitaID INT PRIMARY KEY IDENTITY(1,1),
    PacienteID INT NOT NULL,
    DentistaID INT NOT NULL,
    MotivoID INT NOT NULL,
    Fecha DATETIME NOT NULL, 
    DuracionMinutos INT NOT NULL, 
  
    CONSTRAINT FK_Cita_Paciente FOREIGN KEY (PacienteID) REFERENCES Paciente(PacienteID),
    CONSTRAINT FK_Cita_Dentista FOREIGN KEY (DentistaID) REFERENCES Dentista(DentistaID),
    CONSTRAINT FK_Cita_Motivo FOREIGN KEY (MotivoID) REFERENCES Motivo(MotivoID)
);
GO

INSERT INTO Paciente (Nombre, Apellido, Telefono) VALUES ('Juan', 'Perez', '555-0100');
INSERT INTO Dentista (Nombre, Especialidad) VALUES ('Dra. Ana', 'Ortodoncia');
INSERT INTO Motivo (Descripcion, Costo) VALUES ('Limpieza General', 500.00);
GO