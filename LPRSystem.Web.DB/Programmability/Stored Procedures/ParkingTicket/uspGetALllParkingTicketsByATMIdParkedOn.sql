CREATE PROCEDURE [api].[uspGetAllParkingTicketsByATMIdParkedOn]
    @ATMId BIGINT,
    @ParkedOn DateTimeOffSet
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [ParkingTicketId],
        [ATMId],
        [ParkingTicketCode],
        [ParkingTicketRefrence],
        [ParkedOn],
        [ParkingDurationFrom],
        [ParkingDurationTo],
        [TotalDuration],
        [ParkingPriceId],
        [VehicleNumber],
        [PhoneNumber],
        [IsExtended],
        [ExtendedOn],
        [ExtendedDurationFrom],
        [ExtendedDurationTo],
        [ActualAmount],
        [ExtendedAmount],
        [TotalAmount],
        [Status],
        [CreatedBy],
        [CreatedOn],
        [ModifiedBy],
        [ModifiedOn],
        [IsActive]
    FROM 
        [data].[ParkingTicket]
    WHERE 
        [ATMId] = @ATMId
        AND CAST([ParkedOn] AS DATE) = @ParkedOn
        AND [IsActive] = 1;
END;