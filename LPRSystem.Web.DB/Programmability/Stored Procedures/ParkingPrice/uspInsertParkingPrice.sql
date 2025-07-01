CREATE PROCEDURE [api].[uspInsertParkingPrice]
	(
		 @duartion varchar(max),
		 @price  decimal(10,2),
		 @createdBy bigint,
		 @createdOn datetimeoffset,
		 @modifiedBy bigint,
		 @modifiedOn datetimeoffset,
		 @isActive bit
)

WITH RECOMPILE

AS

BEGIN
        INSERT INTO [data].[ParkingPrice]
		(
		[Duration],
		[Price],
		[CreatedBy],
		[CreatedOn],
		[ModifiedBy],
		[ModifiedOn],
		[IsActive]
		) 
		Values
		(
		@duartion,
		@price,
		@createdBy,
		@createdOn,
		@modifiedBy,
		@modifiedOn,
		@isActive)
END
