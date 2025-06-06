CREATE PROCEDURE [api].[uspGetParkingSlots]

WITH RECOMPILE

AS 
   BEGIN
     
     SELECT 
      prks.ParkingSlotId,
      prks.ParkingPlaceId,
      prkc.ParkingPlaceName,
      prkc.ParkingPlaceCode,
      prks.ParkingSlotCode,
      prks.ATMId,
      atm.ATMCode,
      prks.CreatedBy,
      prks.CreatedOn,
      prks.ModifiedBy,
      prks.ModifiedOn,
      prks.IsActive
      FROM [data].[ParkingSlot] prks
      left join [data].[ParkingPlace] prkc on prks. ParkingPlaceId = prkc.ParkingPlaceId
      left join [data].[ATMMachine] atm on prks.ATMId=atm.ATMId
END