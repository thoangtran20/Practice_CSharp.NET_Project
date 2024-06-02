CREATE DATABASE CustomerDB;
GO;

USE CustomerDB;
CREATE TABLE Customer (
	CustomerID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	FirstName VARCHAR(50),
	LastName VARCHAR(50),
	City VARCHAR(50),
	Address VARCHAR(255)
)