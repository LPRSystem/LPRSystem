CREATE PROCEDURE [api].[uspChangePassword]
	(
		@userId bigint, 
		@passwordhash nvarchar(max),
		@passwordsalt nvarchar(max), 
		@modifiedBy bigint,
		@lastPasswordChangedOn datetimeoffset
	)
AS
BEGIN
	update [data].[User] 
	   set PasswordHash = @passwordhash,
		   PasswordSalt = @passwordsalt,
		   ModifiedBy   = @modifiedBy,
		   ModifiedOn   = getdate(),
		   LastPasswordChangedOn = @lastPasswordChangedOn
		   where Id = @userId
	  
END
