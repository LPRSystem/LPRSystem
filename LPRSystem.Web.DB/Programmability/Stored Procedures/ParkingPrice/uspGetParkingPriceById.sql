CREATE PROCEDURE [api].[uspGetParkingPriceById]
	(
 @parkingPriceId  bigint
)
WITH RECOMPILE

AS 

BEGIN

SELECT

     [ParkingPriceId]
    ,[Duration]
    ,[Price]
    ,[CreatedBy]
    ,[CreatedOn]
    ,[ModifiedBy]
    ,[ModifiedOn]
    ,[IsActive]

    FROM [data].[ParkingPrice]

    WHERE ParkingPriceId=@parkingPriceId

    END
