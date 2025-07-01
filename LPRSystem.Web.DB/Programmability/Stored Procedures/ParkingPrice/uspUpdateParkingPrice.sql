CREATE PROCEDURE [api].[uspUpdateParkingPrice]
(
 @parkingPriceId         bigint,
 @duration               varchar(max),
 @price                  decimal(10,2),
 @modifiedBy             bigint,
 @modifiedOn             datetimeoffset,
 @isActive               bit
 )

 WITH RECOMPILE

 AS

 BEGIN

 UPDATE [data].[ParkingPrice] 
		SET
		   Duration   = @duration,
		   Price      = @price,
		   ModifiedBy = @modifiedBy,
		   ModifiedOn = @modifiedOn,
		   IsActive  = @isActive
		 WHERE ParkingPriceId = @parkingPriceId
  END
