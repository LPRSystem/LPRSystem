CREATE PROCEDURE [api].[uspDeleteParkingPrice]
(
@parkingPriceId  bigint
)

AS

BEGIN

DELETE FROM [data].[ParkingPrice] 
       
       WHERE ParkingPriceId = @parkingPriceId
END
