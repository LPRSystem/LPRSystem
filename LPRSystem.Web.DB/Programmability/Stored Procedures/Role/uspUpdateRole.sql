CREATE PROCEDURE [api].[uspUpdateRole]
	(@id bigint,
	@name varchar(50),
	@code varchar(50),
	@createdBy bigint,
	@createdOn datetimeoffset,
	@modifiedBy bigint,
	@modifiedOn datetimeoffset,
	@isActive bit)
AS
	BEGIN
update [lookup].[role] set 
           name      = @name,
		   code      = @code,
		   CreatedBy = @createdBy,
		   CreatedOn = @createdOn,
		   ModifiedBy = @modifiedBy,
		   ModifiedOn = @modifiedOn,
		   IsActive = @isActive
	Where Id=@id

	EXEC  [api].[uspGetRoles]
END
