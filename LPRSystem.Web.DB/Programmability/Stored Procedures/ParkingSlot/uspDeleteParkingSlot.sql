CREATE PROCEDURE [api].[uspDeleteParkingSlot]
(
    @parkingSlotId BIGINT
)

WITH RECOMPILE

AS

BEGIN

    DELETE FROM [data].[ParkingSlot]
    WHERE [ParkingSlotId] = @parkingSlotId;

END


