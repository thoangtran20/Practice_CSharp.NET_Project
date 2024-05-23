USE HotelDB
GO	

-- Create Room Type
CREATE OR ALTER PROCEDURE spCreateRoomType
	@TypeName NVARCHAR(50),
	@AccessibilityFeatures NVARCHAR(255),
	@Description NVARCHAR(255),
	@CreatedBy NVARCHAR(100),
	@NewRoomTypeID INT OUTPUT,
	@StatusCode INT OUTPUT,
	@Message NVARCHAR(255) OUTPUT
AS 
BEGIN
    SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION
			IF NOT EXISTS (SELECT 1 FROM dbo.RoomTypes WHERE TypeName = @TypeName)
			BEGIN
			    INSERT INTO RoomTypes (TypeName, AccessibilityFeatures, Description, CreatedBy, CreatedDate)
                VALUES (@TypeName, @AccessibilityFeatures, @Description, @CreatedBy, GETDATE())

				SET @NewRoomTypeID = SCOPE_IDENTITY()
				SET @StatusCode = 0 -- Success
				SET @Message = 'Room type created successfully.'
			END
			ELSE
			BEGIN
			    SET @StatusCode = 1 -- Failure due to duplicate name
				SET @Message = 'Room type name already exists.'
			END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @StatusCode = ERROR_NUMBER() -- SQL Server error number
		SET @Message = ERROR_MESSAGE() 
	END CATCH
END
GO	

-- Update Room Type
CREATE OR ALTER PROCEDURE spUpdateRoomType
	@RoomTypeID INT,
	@TypeName NVARCHAR(50),
	@AccessibilityFeatures NVARCHAR(255),
	@Description NVARCHAR(255),
	@ModifiedBy NVARCHAR(100),
	@StatusCode INT OUTPUT,
	@Message NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	BEGIN TRY
	    BEGIN TRANSACTION
			-- Check if the updated type name already exists in another record 
			IF NOT EXISTS (SELECT 1 FROM dbo.RoomTypes WHERE TypeName = @TypeName AND 
				RoomTypeID <> @RoomTypeID)
			BEGIN
			    IF EXISTS (SELECT 1 FROM dbo.RoomTypes WHERE RoomTypeID = @RoomTypeID)
				BEGIN
					UPDATE dbo.RoomTypes
					SET TypeName = @TypeName,
						AccessibilityFeatures = @AccessibilityFeatures,
						Description = @Description,
						ModifiedBy= @ModifiedBy,
						ModifiedDate = GETDATE()
					WHERE RoomTypeID = @RoomTypeID

					SET @StatusCode = 0 -- Success
					SET @Message = 'Room type updated successfully.'
				END
				ELSE 
				BEGIN
				    SET @StatusCode = 2 -- Failure due to not found
					SET @Message = 'Room type not found.'
				END
			END
			ELSE 
			BEGIN
			    SET @StatusCode = 1 -- Failure due to duplicate name
				SET @Message = 'Another room type with the same name already exists.'
			END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @StatusCode = ERROR_NUMBER() -- SQL Server error number
		SET @Message = ERROR_MESSAGE()
	END CATCH
END

-- Delete Room Type By Id
CREATE OR ALTER PROCEDURE spDeleteRoomType 
	@RoomTypeID INT,
	@StatusCode INT OUTPUT,
	@Message NVARCHAR(255) OUTPUT
AS	
BEGIN
    SET NOCOUNT ON;
	BEGIN TRY	
	    BEGIN TRANSACTION
			-- Check for existing rooms linked to this room type
			IF NOT EXISTS (SELECT 1 FROM dbo.Rooms WHERE RoomTypeID = @RoomTypeID)
			BEGIN
				IF EXISTS (SELECT 1 FROM dbo.RoomTypes WHERE RoomTypeID	= @RoomTypeID)
				BEGIN
					DELETE FROM dbo.RoomTypes WHERE RoomTypeID = @RoomTypeID
					SET @StatusCode = 0 -- Success
					SET @Message = 'Room type deleted successfully.'
				END
				ELSE
				BEGIN
				    SET @StatusCode = 2 -- Failure duo to not found
					SET @Message = 'Room type not found.'
				END
			END
			ELSE
			BEGIN
			    SET @StatusCode = 1 -- Failure due to dependency
				SET @Message = 'Cannot delete room type as it is being referenced by one of more rooms.'
			END
	    COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @StatusCode = ERROR_NUMBER() -- SQL Server error number
		SET @Message = ERROR_MESSAGE()
	END CATCH
END

-- Get Room Type By Id
CREATE OR ALTER PROCEDURE spGetRoomTypeById 
	@RoomTypeID INT
AS	
BEGIN
    SET NOCOUNT ON;
	SELECT RoomTypeID, TypeName, AccessibilityFeatures, Description, IsActive FROM dbo.RoomTypes 
	WHERE RoomTypeID = @RoomTypeID
END
GO	

-- Get All Room Type
CREATE OR ALTER PROCEDURE spGetAllRoomTypes
	@IsActive BIT = NULL -- Optional parameter to filter by IsActive status
AS
BEGIN
    SET NOCOUNT ON;
	-- Select users based on active status
	IF @IsActive IS NULL
	BEGIN
	    SELECT RoomTypeID, TypeName, AccessibilityFeatures, Description, IsActive FROM dbo.RoomTypes
	END
	ELSE
	BEGIN
	    SELECT RoomTypeID, TypeName, AccessibilityFeatures, Description, IsActive FROM dbo.RoomTypes
		WHERE IsActive = @IsActive;
	END
END

-- Activate/Deactivate RoomType
CREATE OR ALTER PROCEDURE spToggleRoomTypeActive
	@RoomTypeID INT,
	@IsActive BIT,
	@StatusCode INT OUTPUT,
	@Message NVARCHAR(255) OUTPUT
AS	
BEGIN
    SET NOCOUNT ON;
	BEGIN TRY	
	    -- Check user existence
		IF NOT EXISTS (SELECT 1 FROM dbo.RoomTypes WHERE RoomTypeID = @RoomTypeID)
		BEGIN
		     SET @StatusCode = 1 -- Failure due to not found
             SET @Message = 'Room type not found.'
		END

		 -- Update IsActive status
        BEGIN TRANSACTION
             UPDATE RoomTypes SET IsActive = @IsActive WHERE RoomTypeID = @RoomTypeID;
                SET @StatusCode = 0 -- Success
             SET @Message = 'Room type activated/deactivated successfully.'
        COMMIT TRANSACTION
	END TRY
	-- Handle exceptions
	BEGIN CATCH 
		ROLLBACK TRANSACTION
		SET @StatusCode = ERROR_NUMBER() -- SQL Server error number
		SET @Message = ERROR_MESSAGE()
	END CATCH
END
