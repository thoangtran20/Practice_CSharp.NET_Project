USE TestSP_DB
GO


CREATE OR ALTER PROCEDURE InsertEmp_SP
	@EmpID INT,
	@EmpName NVARCHAR(50),
	@City NVARCHAR(50),
	@Sex NVARCHAR(20),
	@Age FLOAT,
	@JoiningDate DATETIME,
	@Contact NVARCHAR(50)
AS 
BEGIN
	INSERT INTO dbo.EmpTest (Emp_ID, Emp_Name, City, Age, Sex, JoiningDate, Contact)
	VALUES(@EmpID, @EmpName, @City, @Age, @Sex, @JoiningDate, @Contact) 
END;
GO

CREATE OR ALTER PROCEDURE ListEmp_SP
AS	
BEGIN
    SELECT * FROM dbo.EmpTest
END;
GO

CREATE OR ALTER PROCEDURE UpdateEmp_SP
	@EmpID INT,
	@EmpName NVARCHAR(50),
	@City NVARCHAR(50),
	@Sex NVARCHAR(20),
	@Age FLOAT,
	@JoiningDate DATETIME,
	@Contact NVARCHAR(50)
AS 
BEGIN
	UPDATE dbo.EmpTest 
	SET Emp_Name = @EmpName, City = @City, Age = @Age, Sex = @Sex, 
		JoiningDate = @JoiningDate, Contact = @Contact
	WHERE Emp_ID = @EmpID
END;
GO


CREATE OR ALTER PROCEDURE DeleteEmp_SP
	@EmpID INT
AS 
BEGIN
	DELETE dbo.EmpTest 
	WHERE Emp_ID = @EmpID
END;
GO

