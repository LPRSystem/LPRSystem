CREATE PROCEDURE [api].[uspGetParkingPrice]

WITH RECOMPILE

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

