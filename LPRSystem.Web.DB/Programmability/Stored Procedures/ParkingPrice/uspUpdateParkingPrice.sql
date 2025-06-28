﻿CREATE PROCEDURE [api].[uspUpdateParkingPrice]
(
 @parkingPriceId         bigint,
 @duration               varchar(max),
 @price                  decimal(10,2),
 @modifiedBy             bigint,
 @modifiedOn             datetimeoffset,
 @isActive               bit)

 AS

 BEGIN

 update [data].[ParkingPrice] 
		set
		   Duration   = @duration,
		   Price      = @price,
		   ModifiedBy = @modifiedBy,
		   ModifiedOn = @modifiedOn,
		   IsActive  = @isActive
		 Where ParkingPriceId = @parkingPriceId
  END
