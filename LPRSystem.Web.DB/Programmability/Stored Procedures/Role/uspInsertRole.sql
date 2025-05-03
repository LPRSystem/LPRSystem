CREATE PROCEDURE [api].[uspInsertRole]
	(@name varchar(50),
	@code varchar(50),
	@createdBy bigint,
	@createdOn datetimeoffset,
	@modifiedBy bigint,
	@modifiedOn datetimeoffset,
	@isActive bit)
AS

BEGIN

INSERT INTO [lookup].[role]
                     (Name,
					  Code,
					  CreatedBy,
					  CreatedOn,
					  ModifiedBy,
					  ModifiedOn,
					  IsActive)
					  values
					  (@name,
					  @code,
					  @createdBy,
					  @createdOn,
					  @modifiedBy,
					  @modifiedOn,
					  @isActive)

     EXEC  [api].[uspGetRoles]
	END