CREATE PROCEDURE [api].[uspSaveParkingPrice]
	(
		 @duartion varchar(max),
		 @price  decimal(10,2),
		 @createdby bigint,
		 @createdon datetimeoffset,
		 @modifiedby bigint,
		 @modifiedon datetimeoffset,
		 @isactive bit
)
AS

BEGIN
        INSERT INTO [data].[ParkingPrice]
		(Duration,
		Price,
		CreatedBy,
		CreatedOn,
		ModifiedBy,
		ModifiedOn,
		IsActive) 
		Values
		(@duartion ,
		@price,
		@createdby,
		@createdon,
		@modifiedby,
		@modifiedon,
		@isactive)
END
