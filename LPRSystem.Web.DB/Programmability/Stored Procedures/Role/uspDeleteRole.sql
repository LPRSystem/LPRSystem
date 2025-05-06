CREATE PROCEDURE [api].[uspDeleteRole]
	(@id bigint)

AS

BEGIN

update [lookup].[Role]
       set IsActive=0 
	   where Id= @id

	    EXEC  [api].[uspGetRoles]
END