CREATE DATABASE DentalDB;
GO
USE DentalDB;
GO

-- Tabla Paciente
CREATE TABLE Paciente (
    PacienteID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(80),
    Telefono NVARCHAR(20),
    Email NVARCHAR(120)
);

-- Tabla Dentista
CREATE TABLE Dentista (
    DentistaID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(80),
    Especialidad NVARCHAR(100)
);

-- Tabla Motivo
CREATE TABLE Motivo (
    MotivoID INT IDENTITY PRIMARY KEY,
    Descripcion NVARCHAR(200)
);

-- Tabla Cita
CREATE TABLE Cita (
    CitaID INT IDENTITY PRIMARY KEY,
    PacienteID INT NOT NULL,
    DentistaID INT NOT NULL,
    MotivoID INT NOT NULL,
    Fecha DATE NOT NULL,
    Hora TIME NOT NULL,
    Duracion INT NOT NULL, -- minutos
    CONSTRAINT FK_Cita_Paciente FOREIGN KEY(PacienteID) REFERENCES Paciente(PacienteID),
    CONSTRAINT FK_Cita_Dentista FOREIGN KEY(DentistaID) REFERENCES Dentista(DentistaID),
    CONSTRAINT FK_Cita_Motivo FOREIGN KEY(MotivoID) REFERENCES Motivo(MotivoID)
    );