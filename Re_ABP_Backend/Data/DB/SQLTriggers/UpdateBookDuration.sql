CREATE TRIGGER UpdateBookDuration
ON BookAudioFile
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Update And Insert
    DECLARE @AudioBookIds TABLE (Id INT);
    
    INSERT INTO @AudioBookIds (Id)
    SELECT DISTINCT AudioBookId FROM inserted;

    -- Delete
    IF NOT EXISTS (SELECT 1 FROM @AudioBookIds)
    BEGIN
        INSERT INTO @AudioBookIds (Id)
        SELECT DISTINCT AudioBookId FROM deleted;
    END

    -- Calculate Update BookDuration
    UPDATE ab
    SET ab.BookDuration = COALESCE((
        SELECT SUM(baf.Duration)
        FROM BookAudioFile baf
        WHERE baf.AudioBookId = ab.Id
    ), 0)
    FROM AudioBook ab
    WHERE ab.Id IN (SELECT Id FROM @AudioBookIds);
END;
