CREATE DATABASE TestSP_DB

USE TestSP_DB
GO

CREATE TABLE EmpTest (
	Emp_ID INT NOT NULL PRIMARY KEY,
	Emp_Name NVARCHAR(50),
	City NVARCHAR(50),
	Age FLOAT,
	Sex NVARCHAR(20),
	JoiningDate DATETIME,
	Contact NVARCHAR(50)
);

--ALTER TABLE dbo.EmpTest
--ALTER COLUMN Emp_ID INT NOT NULL

--DROP TABLE dbo.EmpTest

INSERT INTO EmpTest (Emp_ID, Emp_Name, City, Age, Sex, JoiningDate, Contact)
VALUES (1012, 'John Doe', 'New York', 32.5, 'Male', '2023-01-01', '123-456-7890'),
       (1034, 'Jane Smith', 'Los Angeles', 28, 'Female', '2022-06-15', '987-654-3210'),
       (0092, 'Michael Chen', 'Chicago', 41, 'Male', '2020-12-24', '555-123-4567');

