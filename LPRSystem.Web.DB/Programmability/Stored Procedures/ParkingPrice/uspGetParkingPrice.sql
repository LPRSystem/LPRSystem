CREATE PROCEDURE [api].[uspGetParkingPrice]
AS
BEGIN

SELECT
      [ParkingPriceId],
	  [Duration],
	  [Price],
	  [CreatedBy],
	  [CreatedOn],
	  [ModifiedBy],
	  [ModifiedOn],
	  [IsActive]   
	  FROM [data].[ParkingPrice]
	  
END

