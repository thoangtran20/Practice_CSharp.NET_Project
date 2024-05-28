-- Description: Fetches amenities based on their active status.
-- If @IsActive is provided, it returns amenities filtered by the active status.

USE HotelDB
GO		

CREATE OR ALTER PROCEDURE spFetchAmenities
	@IsActive BIT = NULL,
	@Status BIT OUTPUT,
	@Message NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	BEGIN TRY	
	    -- Retrieve all amenities or filter by active status based on the input parameter.
		IF @IsActive IS	NULL
			SELECT * FROM dbo.Amenities
		ELSE	
			SELECT * FROM dbo.Amenities WHERE IsActive = @IsActive;
		-- Return success status and message.
		SET @Status = 1 --Success
		SET @Message = 'Data retrieved successfully.';
	END TRY
	BEGIN CATCH
		-- Handle errors and return failure status.
		SET @Status = 0; -- Failure
		SET @Message = ERROR_MESSAGE();
	END CATCH;
END;
GO	

-- Description: Fetches a specific amenity based on its ID.
-- Returns the details of the amenity if it exists.
CREATE OR ALTER PROCEDURE spFetchAmenityByID 
	@AmenityID INT
AS
BEGIN
    SET NOCOUNT ON;
	SELECT AmenityID, Name, Description, IsActive FROM dbo.Amenities
	WHERE AmenityID = @AmenityID;
END;
GO	

-- Description: Inserts a new amenity into the Amenities table.
-- Prevents duplicates based on the amenity name.
CREATE OR ALTER PROCEDURE spAddAmenity
	@Name NVARCHAR(100),
	@Description NVARCHAR(255),
	@CreatedBy NVARCHAR(100),
	@AmenityID INT OUTPUT,
	@Status BIT OUTPUT,
	@Message NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION
		    -- Check if an amenity with the same name already exists to avoid duplication.
			IF EXISTS (SELECT 1 FROM dbo.Amenities WHERE Name = @Name)
			BEGIN
			    SET @Status = 0;
				SET @Message = 'Amenity already exists.';
			END
			ELSE	
			BEGIN
			    -- Insert the new amenity record.
				INSERT INTO Amenities (Name, Description, CreatedBy, CreatedDate, IsActive)
				VALUES (@Name, @Description, @CreatedBy, GETDATE(), 1);
				-- Retrieve the ID of the newly inserted amenity.
				SET @AmenityID = SCOPE_IDENTITY();
				SET @Status = 1;
				SET @Message = 'Amenity added successfully.';
			END
		COMMIT TRANSACTION
    END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		SET @Status = 0;
		SET @Message = ERROR_MESSAGE();
	END CATCH;
END;
GO

-- Description: Updates an existing amenity's details in the Amenities table.
-- Checks if the amenity exists before attempting an update.
CREATE OR ALTER PROCEDURE spUpdateAmenity
	@AmenityID INT,
	@Name NVARCHAR(100),
	@Description NVARCHAR(255),
	@IsActive BIT,
	@ModifiedBy NVARCHAR(100),
	@Status BIT OUTPUT,
	@Message NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION
			-- Check if the amenity exists before updating.
			IF NOT EXISTS (SELECT 1 FROM Amenities WHERE AmenityID = @AmenityID)
			BEGIN
				SET @Status = 0;
				SET @Message = 'Amenity does not exist.';
				ROLLBACK TRANSACTION;
				RETURN;
			END
			-- Check for name uniqueness excluding the current amenity.
			IF EXISTS (SELECT 1 FROM Amenities WHERE Name = @Name AND AmenityID <> @AmenityID)
			BEGIN
				SET @Status = 0;
				SET @Message = 'The name already exists for another amenity.';
				ROLLBACK TRANSACTION;
				RETURN;
			END
			-- Update the amenity details.
			UPDATE Amenities
			SET Name = @Name, Description = @Description, IsActive = @IsActive, ModifiedBy = @ModifiedBy, ModifiedDate = GETDATE()
			WHERE AmenityID = @AmenityID;
			-- Check if the update was successful
			IF @@ROWCOUNT = 0
			BEGIN
				SET @Status = 0;
				SET @Message = 'No records updated.';
				ROLLBACK TRANSACTION;
			END
			ELSE
			BEGIN
				SET @Status = 1;
				SET @Message = 'Amenity updated successfully.';
				COMMIT TRANSACTION;
			END
	END TRY
	BEGIN CATCH
	-- Handle exceptions and roll back the transaction if an error occurs.
		ROLLBACK TRANSACTION;
		SET @Status = 0;
		SET @Message = ERROR_MESSAGE();
	END CATCH;
END;
GO

-- Description: Soft deletes an amenity by setting its IsActive flag to 0.
-- Checks if the amenity exists before marking it as inactive.
CREATE OR ALTER PROCEDURE spDeleteAmenity
	@AmenityID INT,
	@Status BIT OUTPUT,
	@Message NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
	BEGIN TRY	
	    BEGIN TRANSACTION
	        -- Check if the amenity exists before attempting to delete.
			IF NOT EXISTS (SELECT 1 FROM dbo.Amenities WHERE AmenityID = @AmenityID)
				BEGIN
				    SET @Status = 0;
					SET @Message = 'Amenity does not exist.';
				END
			ELSE	
				BEGIN
				    -- Update the IsActive flag to 0 to soft delete the amenity.
					UPDATE Amenities
					SET IsActive = 0
					WHERE AmenityID = @AmenityID;
					SET @Status = 1;
					SET @Message = 'Amenity deleted successfully.';
				END
	    COMMIT TRANSACTION;
	END TRY	
	BEGIN CATCH
	-- Roll back the transaction if an error occurs.
	ROLLBACK TRANSACTION;
	SET @Status = 0;
	SET @Message = ERROR_MESSAGE();
	END CATCH;
END;
GO	
-- Creating a User-Defined Table Type for Bulk Insert
CREATE TYPE AmenityInsertType AS TABLE (
	Name NVARCHAR(100),
	Description NVARCHAR(255),
	CreatedBy NVARCHAR(100)
);
GO

-- Description: Performs a bulk insert of amenities into the Amenities table.
-- Ensures that no duplicate names are inserted.
CREATE OR ALTER PROCEDURE spBulkInsertAmenities
	@Amenities AmenityInsertType READONLY,
	@Status BIT OUTPUT,
	@Message NVARCHAR(255) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION
			-- Check for duplicate names within the insert dataset.
			IF EXISTS (
				SELECT 1
				FROM @Amenities
				GROUP BY Name
				HAVING COUNT(*) > 1
			)
			BEGIN
				SET @Status = 0;
				SET @Message = 'Duplicate names found within the new data.';
				ROLLBACK TRANSACTION;
				RETURN;
			END
			-- Check for existing names in the Amenities table that might conflict with the new data.
			IF EXISTS (
				SELECT 1
				FROM @Amenities a
				WHERE EXISTS (
					SELECT 1 FROM Amenities WHERE Name = a.Name
				)
			)
			BEGIN
				SET @Status = 0;
				SET @Message = 'One or more names conflict with existing records.';
				ROLLBACK TRANSACTION;
				RETURN;
			END
			-- Insert new amenities ensuring there are no duplicates by name.
			INSERT INTO Amenities (Name, Description, CreatedBy, CreatedDate, IsActive)
			SELECT Name, Description, CreatedBy, GETDATE(), 1
			FROM @Amenities;
			-- Check if any records were actually inserted.
			IF @@ROWCOUNT = 0
				BEGIN
					SET @Status = 0;
					SET @Message = 'No records inserted. Please check the input data.';
					ROLLBACK TRANSACTION;
				END
			ELSE
				BEGIN
					SET @Status = 1;
					SET @Message = 'Bulk insert completed successfully.';
					COMMIT TRANSACTION;
				END
	END TRY
	BEGIN CATCH
		-- Handle any errors that occur during the transaction.
		ROLLBACK TRANSACTION;
		SET @Status = 0;
		SET @Message = ERROR_MESSAGE();
	END CATCH;
END;
GO

-- Creating User-Defined Table Type for Bulk Update
CREATE TYPE AmenityUpdateType AS TABLE (
	AmenityID INT,
	Name NVARCHAR(100),
	Description NVARCHAR(255),
	IsActive BIT
);
GO

-- Description: Updates multiple amenities in the Amenities table using a provided list.
-- Applies updates to the Name, Description, and IsActive status.
CREATE OR ALTER PROCEDURE spBulkUpdateAmenities
	@AmenityUpdates AmenityUpdateType READONLY,
	@Status BIT OUTPUT,
	@Message NVARCHAR(255) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION
			-- Check for duplicate names within the insert dataset.
			IF EXISTS (
				SELECT 1
				FROM @AmenityUpdates u
				GROUP BY u.Name
				HAVING COUNT(*) > 1
			)
			BEGIN
				SET @Status = 0;
				SET @Message = 'Duplicate names found within the update data.';
				ROLLBACK TRANSACTION;
				RETURN;
			END
			-- Check for existing names in the Amenities table that might conflict with the new data.
			IF EXISTS (
				SELECT 1
				FROM @AmenityUpdates u 
				JOIN dbo.Amenities a ON u.Name = a.Name AND u.AmenityID != a.AmenityID
			)
			BEGIN
				SET @Status = 0;
				SET @Message = 'One or more names conflict with existing records.';
				ROLLBACK TRANSACTION;
				RETURN;
			END
			-- Update amenities based on the provided data.
			UPDATE a
			SET a.Name = u.Name,
			a.Description = u.Description,
			a.IsActive = u.IsActive
			FROM Amenities a
			INNER JOIN @AmenityUpdates u ON a.AmenityID = u.AmenityID;
			-- Check if any records were actually updated.
			IF @@ROWCOUNT = 0
				BEGIN
					SET @Status = 0;
					SET @Message = 'No records updated. Please check the input data.';
				END
			ELSE
				BEGIN
					SET @Status = 1;
					SET @Message = 'Bulk update completed successfully.';
				END
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		-- Roll back the transaction and handle the error
		ROLLBACK TRANSACTION;
		SET @Status = 0;
		SET @Message = ERROR_MESSAGE();
	END CATCH;
END;
GO

-- Creating a User-Defined Table Type for Bulk Active and InActive
CREATE TYPE AmenityStatusType AS TABLE (
	AmenityID INT,
	IsActive BIT
);
GO

-- Description: Updates the active status of multiple amenities in the Amenities table.
-- Takes a list of amenity IDs and their new IsActive status.
CREATE OR ALTER PROCEDURE spBulkUpdateAmenityStatus
	@AmenityStatuses AmenityStatusType READONLY,
	@Status BIT OUTPUT,
	@Message NVARCHAR(255) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION
			-- Update the IsActive status for amenities based on the provided AmenityID.
			UPDATE a
			SET a.IsActive = s.IsActive
			FROM Amenities a
			INNER JOIN @AmenityStatuses s ON a.AmenityID = s.AmenityID;
			-- Check if any records were actually updated.
			SET @Status = 1; -- Success
			SET @Message = 'Bulk status update completed successfully.';
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		-- Roll back the transaction if an error occurs.
		ROLLBACK TRANSACTION;
		SET @Status = 0; -- Failure
		SET @Message = ERROR_MESSAGE();
	END CATCH;
END;
GO