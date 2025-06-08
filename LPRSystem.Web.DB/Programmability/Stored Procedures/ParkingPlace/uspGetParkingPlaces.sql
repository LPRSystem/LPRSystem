CREATE PROCEDURE [api].[uspGetParkingPlaces]
WITH RECOMPILE
AS
  BEGIN
            SELECT 
            ParkingPlaceId,
            ParkingPlaceName,
            ParkingPlaceCode,
            ParkingPlaceType,
            CreatedOn,
            CreatedBy,
            ModifiedOn,
            ModifiedBy,
            IsActive
            FROM  [data].[ParkingPlace]

  END