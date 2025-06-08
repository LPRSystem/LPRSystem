CREATE PROCEDURE [api].[uspUpdateParkingSlot]
(
    @parkingSlotId BIGINT,
    @parkingPlaceId BIGINT,
    @parkingSlotCode VARCHAR(50),
    @atmId BIGINT,
    @modifiedBy BIGINT,
    @modifiedOn DATETIMEOFFSET,
    @isActive BIT
)


WITH RECOMPILE

AS

BEGIN

    UPDATE [data].[ParkingSlot]
    SET
        ParkingPlaceId = @parkingPlaceId,
        ParkingSlotCode = @parkingSlotCode,
        ATMId = @atmId,
        ModifiedBy = @modifiedBy,
        ModifiedOn = @modifiedOn,
        IsActive = @isActive
    WHERE ParkingSlotId = @parkingSlotId
END