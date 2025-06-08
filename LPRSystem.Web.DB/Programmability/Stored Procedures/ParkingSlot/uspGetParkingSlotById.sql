CREATE PROCEDURE [api].[uspGetParkingSlotById]
(
  @ParkingSlotId bigint
)

WITH RECOMPILE

AS 
   BEGIN
     
     SELECT 
      prks.ParkingSlotId,
      prks.ParkingPlaceId,
      prks.ParkingSlotCode,
      prks.ATMId,
      prks.CreatedBy,
      prks.CreatedOn,
      prks.ModifiedBy,
      prks.ModifiedOn,
      prks.IsActive
      FROM [data].[ParkingSlot] prks
      where 
      prks.ParkingSlotId = @ParkingSlotId;
END