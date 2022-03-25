USE Task1;

BEGIN
DECLARE @number_equipment INT = 100
DECLARE @id INT
--проверяем есть ли в таблице уже какие-то данные
Select @id = ISNULL(MAX(id) + 1,1) FROM Equipment
SET @number_equipment = @number_equipment + @id
while @id < @number_equipment
	BEGIN
		--PRINT 'equipment' + Convert(nvarchar,@id)
		INSERT INTO Equipment VALUES (CONCAT('equipment',@id))
		SET @id = @id + 1
	END
END;

GO 

BEGIN

DECLARE @number_coordinates INT = 200
DECLARE @id INT
DECLARE @id_equipment INT
DECLARE @latitude INT, @longitude INT
DECLARE @latitude_type NVARCHAR(5), @longitude_type NVARCHAR(5)

--проверяем есть ли в таблице уже какие-то данные
Select @id = ISNULL(MAX(id) + 1,1) FROM Location
SET @number_coordinates = @number_coordinates + @id
while @id < @number_coordinates
	BEGIN
	    SET @id_equipment = (SELECT TOP 1 id FROM Equipment ORDER BY NEWID())
		SET @latitude = CAST((90 - (RAND() * 180)) as int)
		SET @longitude = CAST((180 - (RAND() * 360)) as int)
		/*Print CONVERT(nvarchar,@id) + ' ' +
				CONVERT(nvarchar,@id_equipment) +  ' ' +
				CONVERT(nvarchar,@latitude) +  ' ' +
				CONVERT(nvarchar,@longitude)*/
		INSERT INTO Location VALUES (@id_equipment,@latitude,@longitude)
		SET @id = @id + 1
	END
END;