-- Create trigger, that will be work after INSERT, UPDATE and DELETE operation in Review table
CREATE TRIGGER UpdateAudioBookRating
ON Review
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @AudioBookId INT;
    DECLARE @NewRating DECIMAL(3, 1);

    -- If INSERT or UPDATE Review
    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        -- Get AudioBook Id
        SELECT @AudioBookId = AudioBookId FROM inserted;

        -- Calculate new rating value
        SELECT @NewRating = ISNULL(CAST(AVG(CAST(Rating AS DECIMAL(3, 1))) AS DECIMAL(3, 1)), 0)
		FROM Review WHERE AudioBookId = @AudioBookId;
        
        -- Update Rating in AudioBook table
        UPDATE AudioBook
        SET Rating = @NewRating
        WHERE Id = @AudioBookId;
    END
    -- If DELETE Review
    ELSE IF EXISTS (SELECT 1 FROM deleted)
    BEGIN
        -- Get AudioBook Id
        SELECT @AudioBookId = AudioBookId FROM deleted;

        -- Calculate new rating value
        SELECT @NewRating = ISNULL(CAST(AVG(CAST(Rating AS DECIMAL(3, 1))) AS DECIMAL(3, 1)), 0)
		FROM Review WHERE AudioBookId = @AudioBookId;
        
        -- Update Rating in AudioBook table
        UPDATE AudioBook
        SET Rating = @NewRating
        WHERE Id = @AudioBookId;
    END
END;
