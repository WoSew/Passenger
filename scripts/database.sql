CREATE DATABASE Passenger2

USE Passenger2

CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(200) NOT NULL,
    Salt NVARCHAR(200) NOT NULL,
    Username NVARCHAR(100) NOT NULL,
    Fullname NVARCHAR(100),
    Role NVARCHAR(10) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL, 
)

SELECT * FROM Users;

DELETE FROM Users;

DROP DATABASE Passenger