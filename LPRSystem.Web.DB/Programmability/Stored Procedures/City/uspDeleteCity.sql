CREATE PROCEDURE [api].[uspDeleteCity]
	(@cityid  bigint)

AS

BEGIN

update [data].[City] 
       set  IsActive = 0
       WHERE CityId = @cityid
END