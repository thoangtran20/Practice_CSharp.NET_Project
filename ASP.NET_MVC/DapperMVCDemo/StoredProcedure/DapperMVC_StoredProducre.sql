USE DapperMVC

/* CREATE OPERATION */
CREATE OR ALTER PROCEDURE sp_add_person(
    @name NVARCHAR(100),
    @email NVARCHAR(100),
    @address NVARCHAR(200)
)
AS
BEGIN
    INSERT INTO dbo.Person (FullName, Email, [Address])
    VALUES (@name, @email, @address)
END
GO	

/* READ OPERATION */
CREATE OR ALTER PROCEDURE sp_get_Allperson
AS
BEGIN
    SELECT * FROM dbo.Person
END
GO	

/* UPDATE OPERATION */
CREATE OR ALTER PROCEDURE sp_update_person(
    @id INT,
    @name NVARCHAR(100),
    @email NVARCHAR(100),
    @address NVARCHAR(200)
)
AS
BEGIN
    UPDATE dbo.Person
    SET FullName = @name, Email = @email, [Address] = @address
    WHERE Id = @id
END
GO

/* DELETE OPERATION */
CREATE OR ALTER PROCEDURE sp_delete_person(@id INT)
AS
BEGIN
    DELETE FROM dbo.Person WHERE Id = @id
END
GO	