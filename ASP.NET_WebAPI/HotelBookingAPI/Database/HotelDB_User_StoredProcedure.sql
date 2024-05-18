USE HotelDB
GO	

-- Add a New User
CREATE PROCEDURE spAddUser 
	@Email NVARCHAR(100),
	@PasswordHash NVARCHAR(255),
    @CreatedBy NVARCHAR(100),
    @UserID INT OUTPUT,
    @ErrorMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Check if email or password is null
        IF @Email IS NULL OR @PasswordHash IS NULL
        BEGIN
            SET @ErrorMessage = 'Email and Password cannot be null.';
            SET @UserID = -1;
            RETURN;
        END

		-- Check if email already exists in the system
		IF EXISTS (SELECT 1 FROM dbo.Users WHERE Email = @Email) 
		BEGIN 
            SET @ErrorMessage = 'A user with the given email already exists.';
			SET @UserID = -1;
			RETURN;
		END

		-- Default role ID for new users
		DECLARE @DefaultRoleID INT = 2; -- Assuming 'Guest' role ID is 2
		
		BEGIN TRANSACTION
			INSERT INTO Users (RoleID, Email, PasswordHash, CreatedBy, CreatedDate)
            VALUES (@DefaultRoleID, @Email, @PasswordHash, @CreatedBy, GETDATE());

			SET @UserID = SCOPE_IDENTITY();
			SET @ErrorMessage = NULL;
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		-- Handle exceptions
		ROLLBACK TRANSACTION
		SET @ErrorMessage = ERROR_MESSAGE();
		SET @UserID = -1;
	END CATCH
END;
GO	

-- Assign a Role to User
CREATE PROCEDURE spAssignUserRole 
	@UserID INT,
	@RoleID INT,
    @ErrorMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	BEGIN TRY
	    -- Check if the user exists
		IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE UserID = @UserId) 
		BEGIN
			SET @ErrorMessage = 'User not found.';
			RETURN;
		END

		-- Check if the role exists
		IF NOT EXISTS (SELECT 1 FROM dbo.UserRoles WHERE RoleID = @RoleID)
		BEGIN
			SET @ErrorMessage = 'Role not found.';
			RETURN;
		END
	END TRY
	BEGIN CATCH
		-- Handle exceptions
		ROLLBACK TRANSACTION
		SET @ErrorMessage = ERROR_MESSAGE();
	END CATCH
END;
GO	

-- List All Users
CREATE PROCEDURE spListAllUsers
	@IsActive BIT = NULL -- Optional parameter to filter by IsActive status
AS
BEGIN
    SET NOCOUNT ON;

	-- Select users based on active status
	IF @IsActive IS NULL
	BEGIN
		SELECT UserID, Email, RoleID, IsActive, LastLogin, CreatedBy, CreatedDate FROM dbo.Users;
	END
	ELSE
	BEGIN
	    SELECT UserID, Email, RoleID, IsActive, LastLogin, CreatedBy, CreatedDate FROM dbo.Users
		WHERE IsActive = @IsActive;
	END
END

-- Get User by ID
CREATE PROCEDURE spGetUserByID
	@UserID INT,
	@ErrorMessage NVARCHAR(255) OUTPUT
AS	
BEGIN	
	SET NOCOUNT ON;

	-- Check if the user exists
	IF NOT EXISTS (SELECT 1 FROM Users WHERE UserID = @UserID)
	BEGIN
	    SET @ErrorMessage = 'User not found.';
		RETURN;
	END

	-- Retrieve user details
    SELECT UserID, Email, RoleID, IsActive, LastLogin, CreatedBy, CreatedDate FROM Users WHERE UserID = @UserID;
    SET @ErrorMessage = NULL;
END;
GO	

-- Update User Information
CREATE PROCEDURE spUpdateUserInformation 
	@UserID INT,
	@Email NVARCHAR(100),
	@Password NVARCHAR(100),
	@ModifiedBy NVARCHAR(100),
	@ErrorMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	BEGIN TRY
		-- Check user existence
		IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE UserID = @UserID)
		BEGIN
		    SET @ErrorMessage = 'User not found.';
			RETURN;
		END

        -- Check email uniqueness except for the current user
		IF EXISTS (SELECT 1 FROM dbo.Users WHERE Email = @Email AND UserID <> @UserID)
		BEGIN
		    SET @ErrorMessage = 'Email already used by another user.';
			RETURN;
		END

		-- Update user details
		BEGIN TRANSACTION
			UPDATE dbo.Users
			SET Email = @Email, PasswordHash = @Password, ModifiedBy = @ModifiedBy, ModifiedDate = GETDATE()
			WHERE UserID = @UserID;
		COMMIT TRANSACTION

		SET @ErrorMessage = NULL;
	END TRY
	-- Handle exceptions
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @ErrorMessage = ERROR_MESSAGE();
	END CATCH
END;
GO	

-- Activate/Deactivate User
-- This can also be used for deleting a User
CREATE PROCEDURE spToggleUserActive
	@UserID INT,
	@IsActive BIT,
	@ErrorMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	BEGIN TRY
		-- Check user existence
		IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE UserID = @UserID)
		BEGIN
		    SET @ErrorMessage = 'User not found.';
			RETURN;
		END

		-- Update IsActive status
		BEGIN TRANSACTION
			UPDATE dbo.Users SET IsActive = @IsActive WHERE UserID = @UserID;
		COMMIT TRANSACTION

		SET @ErrorMessage = NULL;
	END TRY
	-- Handle exceptions
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @ErrorMessage = ERROR_MESSAGE();
	END CATCH
END;
GO	

--Login a User
CREATE PROCEDURE spLoginUser
	@Email NVARCHAR(100),
	@PasswordHash NVARCHAR(255),
	@UserID INT OUTPUT,
	@ErrorMessage NVARCHAR(255) OUTPUT
AS
BEGIN
	-- Attempt to retrieve the user based on email and password hash
    SELECT @UserID = UserID FROM dbo.Users WHERE Email = @Email AND	PasswordHash = @PasswordHash;

	-- Check if user ID was set (means credentials are correct)
	IF @UserID IS NOT NULL
	BEGIN
	    -- Check if the user is active 
		IF EXISTS (SELECT 1 FROM dbo.Users WHERE UserID = @UserID AND IsActive = 1)
		BEGIN
		    -- Update the last login time
			UPDATE dbo.Users SET LastLogin = GETDATE() WHERE UserID = @UserID;
			SET @ErrorMessage = NULL; -- Clear any previous error messages
		END
		ELSE	
		BEGIN
		    SET @ErrorMessage = 'User account is not active.';
			SET @UserID = NULL; -- Reset the UserID as login should not be considered successful
		END
	END
	ELSE
	BEGIN
	    SET @ErrorMessage = 'Invalid Credentiasl.';
	END
END