CREATE PROCEDURE [api].[uspSavePaymentMethod]
(
		 @name varchar(max),
		 @code varchar(max),
		 @createdby bigint,
		 @createdon datetimeoffset,
		 @modifiedby bigint,
		 @modifiedon datetimeoffset,
		 @isactive bit
)
AS

BEGIN
        INSERT INTO [data].[PaymentMethod](Name,Code,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive) Values (@name,@code,@createdby,@createdon,@modifiedby,@modifiedon,@isactive)
END
