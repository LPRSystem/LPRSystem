CREATE PROCEDURE [api].[uspDeleteLocation]
(@locationid  bigint)

AS

BEGIN

update [data].[Location] 
       set  IsActive = 0
       WHERE LocationId = @locationid
END
