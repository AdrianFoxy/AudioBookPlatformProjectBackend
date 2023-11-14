CREATE TRIGGER UpdateAudioBookRating
ON Review
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Common table expression to calculate average ratings
    WITH AvgRatingCTE AS (
        SELECT
            r.AudioBookId,
            ROUND(AVG(CAST(r.Rating AS DECIMAL(4, 1))), 1) AS AvgRating
        FROM
            Review r
        WHERE
            EXISTS (SELECT 1 FROM inserted i WHERE i.AudioBookId = r.AudioBookId)
            OR EXISTS (SELECT 1 FROM deleted d WHERE d.AudioBookId = r.AudioBookId)
        GROUP BY
            r.AudioBookId
    )
    
    -- Update AudioBook ratings based on the calculated averages
    UPDATE a
    SET Rating = ISNULL(c.AvgRating, 0)
    FROM AudioBook a
    LEFT JOIN AvgRatingCTE c ON a.Id = c.AudioBookId
    WHERE EXISTS (SELECT 1 FROM inserted i WHERE i.AudioBookId = a.Id)
       OR EXISTS (SELECT 1 FROM deleted d WHERE d.AudioBookId = a.Id);
END;
