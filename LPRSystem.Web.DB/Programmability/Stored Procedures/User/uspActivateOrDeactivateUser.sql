CREATE PROCEDURE [api].[uspActivateOrDeactivateUser]
	(@userId bigint,@isActive bit,@modifiedBy bigint)
AS
BEGIN
	
	update [data].[User]
	    set IsActive = @isActive,
		    ModifiedBy = @modifiedBy,
			ModifiedOn = GETDATE()
		where Id = @userId
END
