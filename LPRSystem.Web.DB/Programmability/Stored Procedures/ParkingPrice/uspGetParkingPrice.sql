CREATE PROCEDURE [api].[uspGetParkingPrice]

WITH RECOMPILE

AS

BEGIN

SELECT
      [ParkingPriceId],
      [Duration] + ' - ' + '$' + CAST([Price] AS VARCHAR) AS DurationPrice,
      [Price],
      [CreatedBy],
      [CreatedOn],
      [ModifiedBy],
      [ModifiedOn],
      [IsActive]   
      FROM [data].[ParkingPrice]
      WHERE IsActive = 1
END

