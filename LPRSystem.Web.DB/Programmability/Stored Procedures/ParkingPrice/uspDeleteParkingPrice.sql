CREATE PROCEDURE [api].[uspDeleteParkingPrice]
(@parkingPriceId  bigint)

AS

BEGIN

update [data].[ParkingPrice] 
       set  IsActive = 0
       WHERE ParkingPriceId = @parkingPriceId
END
